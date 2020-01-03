﻿using UnityEngine;
using UnityEngine.AI;
using RTSCoreFramework;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
#if RTSAStarPathfinding
using Pathfinding;
#endif

namespace RPGPrototype
{
    [SelectionBase]
    public class RPGCharacter : MonoBehaviour
    {
        #region Fields
        //Init Field
        bool bInitializedAlly = false;
        //Used For Character Death
        [Header("Character Death")]
        [SerializeField] AudioClip[] damageSounds;
        [SerializeField] AudioClip[] deathSounds;
        [SerializeField] float deathVanishSeconds = 2.0f;

        const string DEATH_TRIGGER = "Death";

        AudioSource audioSource;
        // TODO consider a CharacterConfig SO
        [Header("Animator")] [SerializeField] RuntimeAnimatorController animatorController;
        [SerializeField] AnimatorOverrideController animatorOverrideController;
        [SerializeField] Avatar characterAvatar;
        [SerializeField] [Range(.1f, 1f)] float animatorForwardCap = 1f;

        [Header("Audio")]
        [SerializeField] float audioSourceSpatialBlend = 0.5f;

        [Header("Capsule Collider")]
        [SerializeField] Vector3 colliderCenter = new Vector3(0, 1.03f, 0);
        [SerializeField] float colliderRadius = 0.2f;
        [SerializeField] float colliderHeight = 2.03f;

        [Header("Movement")]
        [SerializeField] float moveSpeedMultiplier = .7f;
        [SerializeField] float animationSpeedMultiplier = 1.5f;
        [SerializeField] float movingTurnSpeed = 360;
        [SerializeField] float stationaryTurnSpeed = 180;
        [SerializeField] float moveThreshold = 1f;

        [Header("Nav Mesh Agent")]
        [SerializeField] float navMeshAgentSteeringSpeed = 1.0f;
        [SerializeField] float navMeshAgentStoppingDistance = 1.3f;


        // private instance variables for state
        float turnAmount;
        float forwardAmount;
        bool isAlive = true;

        // cached references for readability
        NavMeshAgent navMeshAgent;
        Animator animator;
        Rigidbody ridigBody;

        //Extra
        bool bUseAStarPath = false;
        private Vector3 myAnimMoveVelocity = Vector3.zero;
        #endregion

        #region MovementFields
        float myHorizontalMovement = 0f;
        float myForwardMovement = 0f;
        Vector3 myDirection = Vector3.zero;
        Vector3 MyMove = Vector3.zero;
        //float speedMultiplier
        //{
        //    get
        //    {
        //        return eventHandler.bIsFreeMoving ?
        //            1.2f * _baseSpeedMultiplier :
        //            1.0f * _baseSpeedMultiplier;
        //    }
        //}
        float _baseSpeedMultiplier = 1.0f;
        bool bWasFreeMoving = false;
        //Used to Fix issue with Command Movement 
        //Automatically being finished because Destination hasn't been set yet.
        bool bHasSetDestination = false;
        //Extra Movement
        //Vector3 m_GroundNormal = Vector3.zero;
        //bool m_IsGrounded = true;
        //float m_GroundCheckDistance = 0.1f;
        #endregion

        #region Properties
        Vector3 CamForward
        {
            get
            {
                return Vector3.Scale(myCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
            }
        }

        Camera myCamera
        {
            get
            {
                if (_myCamera == null)
                    _myCamera = Camera.main;

                return _myCamera;
            }
        }
        Camera _myCamera = null;

        RTSGameMode gamemode
        {
            get { return RTSGameMode.thisInstance; }
        }

        RTSGameMaster gamemaster
        {
            get { return RTSGameMaster.thisInstance; }
        }

        AllyEventHandlerRPG eventHandler
        {
            get
            {
                if (_eventHandler == null)
                {
                    _eventHandler = GetComponent<AllyEventHandlerRPG>();
                }
                return _eventHandler;
            }
        }
        AllyEventHandlerRPG _eventHandler = null;

        AllyMemberRPG allymember
        {
            get
            {
                if (_allymember == null)
                    _allymember = GetComponent<AllyMemberRPG>();

                return _allymember;
            }
        }
        AllyMemberRPG _allymember = null;

#if RTSAStarPathfinding
        Seeker mySeeker
        {
            get
            {
                if(_mySeeker == null)
                {
                    _mySeeker = GetComponent<Seeker>();
                }
                return _mySeeker;
            }
        }
        Seeker _mySeeker = null;

        AIPath myAIPath
        {
            get
            {
                if (_myAIPath == null)
                    _myAIPath = GetComponent<AIPath>();

                return _myAIPath;
            }
        }
        AIPath _myAIPath = null;
#endif
        #endregion

        #region UnityMessages
        // messages, then public methods, then private methods...
        //void Awake()
        //{

        //}

        private void OnEnable()
        {
            eventHandler.EventAllyDied += Kill;
            //eventHandler.EventCommandMove += HandleSetDestination;
            //eventHandler.EventToggleIsSprinting += ToggleSprint;
            //eventHandler.EventFinishedMoving += FinishMoving;
            eventHandler.InitializeAllyComponents += OnInitializeAllyComponents;
        }

        private void OnDisable()
        {
            eventHandler.EventAllyDied -= Kill;
            //eventHandler.EventCommandMove -= HandleSetDestination;
            //eventHandler.EventToggleIsSprinting -= ToggleSprint;
            //eventHandler.EventFinishedMoving -= FinishMoving;
            eventHandler.InitializeAllyComponents -= OnInitializeAllyComponents;
        }

        //void FixedUpdate()
        //{
        //    //Only Update Movement If Ally is Initialized
        //    if(bInitializedAlly == false) return;

        //    if (PlayerWantsFreeMovement())
        //    {
        //        if (bWasFreeMoving == false)
        //            bWasFreeMoving = true;

        //        MoveFreely();
        //    }
        //    else
        //    {
        //        if (bWasFreeMoving)
        //        {
        //            eventHandler.CallEventFinishedMoving();
        //            bWasFreeMoving = false;
        //        }

        //        if (bUseAStarPath == false)
        //        {
        //            MoveCharacterMain();
        //        }
        //        else
        //        {
        //            MoveCharacterFromAStarPath();
        //        }
        //    }
        //}
        
        void OnAnimatorMove()
        {
            // we implement this function to override the default root motion.
            // this allows us to modify the positional speed before it's applied.
            if (Time.deltaTime > 0)
            {
                myAnimMoveVelocity = (animator.deltaPosition * moveSpeedMultiplier) / Time.deltaTime;

                // we preserve the existing y part of the current velocity.
                myAnimMoveVelocity.y = ridigBody.velocity.y;
                ridigBody.velocity = myAnimMoveVelocity;
            }
        }
        #endregion

        #region Initialization
        private void AddRequiredComponents()
        {
            var capsuleCollider = gameObject.AddComponent<CapsuleCollider>();
            capsuleCollider.center = colliderCenter;
            capsuleCollider.radius = colliderRadius;
            capsuleCollider.height = colliderHeight;

            ridigBody = gameObject.AddComponent<Rigidbody>();
            ridigBody.constraints = RigidbodyConstraints.FreezeRotation;

            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = audioSourceSpatialBlend;

            animator = gameObject.AddComponent<Animator>();
            animator.runtimeAnimatorController = animatorController;
            animator.avatar = characterAvatar;

            if(bUseAStarPath == false)
            {
                navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
                //navMeshAgent = GetComponent<NavMeshAgent>();
                navMeshAgent.speed = navMeshAgentSteeringSpeed;
                navMeshAgent.stoppingDistance = navMeshAgentStoppingDistance;
                navMeshAgent.autoBraking = false;
                navMeshAgent.updateRotation = false;
                navMeshAgent.updatePosition = true;
            }
        }
        #endregion

        #region Handlers
        private void OnInitializeAllyComponents(RTSAllyComponentSpecificFields _specificComps, RTSAllyComponentsAllCharacterFields _allAllyComps)
        {
            var _RPGallAllyComps = (AllyComponentsAllCharacterFieldsRPG)_allAllyComps;
            this.bUseAStarPath = _RPGallAllyComps.bUseAStarPath;

            if (_specificComps.bBuildCharacterCompletely)
            {                
                var _rpgCharAttr = this.bUseAStarPath == false ? 
                    ((AllyComponentSpecificFieldsRPG)_specificComps).RPGCharacterAttributesObject :
                    ((AllyComponentSpecificFieldsRPG)_specificComps).ASTAR_RPGCharacterAttributesObject;
                this.damageSounds = _rpgCharAttr.damageSounds;
                this.deathSounds = _rpgCharAttr.deathSounds;
                this.deathVanishSeconds = _rpgCharAttr.deathVanishSeconds;
                this.animatorController = _rpgCharAttr.animatorController;
                this.animatorOverrideController = _rpgCharAttr.animatorOverrideController;
                this.characterAvatar = _rpgCharAttr.characterAvatar;
                this.animatorForwardCap = _rpgCharAttr.animatorForwardCap;
                this.audioSourceSpatialBlend = _rpgCharAttr.audioSourceSpatialBlend;
                this.colliderCenter = _rpgCharAttr.colliderCenter;
                this.colliderRadius = _rpgCharAttr.colliderRadius;
                this.colliderHeight = _rpgCharAttr.colliderHeight;
                this.moveSpeedMultiplier = _rpgCharAttr.moveSpeedMultiplier;
                this.animationSpeedMultiplier = _rpgCharAttr.animationSpeedMultiplier;
                this.movingTurnSpeed = _rpgCharAttr.movingTurnSpeed;
                this.stationaryTurnSpeed = _rpgCharAttr.stationaryTurnSpeed;
                this.moveThreshold = _rpgCharAttr.moveThreshold;
                this.navMeshAgentSteeringSpeed = _rpgCharAttr.navMeshAgentSteeringSpeed;
                this.navMeshAgentStoppingDistance = _rpgCharAttr.navMeshAgentStoppingDistance;
            }

            AddRequiredComponents();
            bInitializedAlly = true;
        }

        void HandleSetDestination(Vector3 _destination)
        {
            SetDestination(_destination);
            bHasSetDestination = true;
        }

        void FinishMoving()
        {
            bHasSetDestination = false;
            SetDestination(transform.position);
            if (bUseAStarPath == false)
            {
                navMeshAgent.velocity = Vector3.zero;
            }
            else
            {
#if RTSAStarPathfinding
                if (myAIPath.canMove)
                {
                    myAIPath.canMove = false;
                }
                if (myAIPath.enableRotation)
                {
                    myAIPath.enableRotation = false;
                }
#endif

            }
        }

        //void ToggleSprint()
        //{
        //    _baseSpeedMultiplier = eventHandler.bIsSprinting ?
        //        2f : 1f;
        //}

        public void Kill(Vector3 position, Vector3 force, GameObject attacker)
        {
            isAlive = false;
            StartCoroutine(KillCharacter());
        }

        IEnumerator KillCharacter()
        {
            animator.SetTrigger(DEATH_TRIGGER);

            audioSource.clip = deathSounds[UnityEngine.Random.Range(0, deathSounds.Length)];
            audioSource.Play(); // overrind any existing sounds
            yield return new WaitForSecondsRealtime(audioSource.clip.length);

            UnityEngine.Object.Destroy(gameObject, deathVanishSeconds);
        }
        #endregion

        #region Getters
        public float GetAnimSpeedMultiplier()
        {
            return animator.speed;
        }

        public AnimatorOverrideController GetOverrideController()
        {
            return animatorOverrideController;
        }
        #endregion

        #region Setters
        public void SetDestination(Vector3 worldPos)
        {
            if (bUseAStarPath == false)
            {
                navMeshAgent.destination = worldPos;
            }
            else
            {
#if RTSAStarPathfinding
                mySeeker.StartPath(transform.position, worldPos);
#endif
            }
        }

        void SetForwardAndTurn(Vector3 movement)
        {
            // convert the world relative moveInput vector into a local-relative
            // turn amount and forward amount required to head in the desired direction
            if (movement.magnitude > moveThreshold)
            {
                movement.Normalize();
            }
            var localMove = transform.InverseTransformDirection(movement);
            //CheckGroundStatus();
            //localMove = Vector3.ProjectOnPlane(localMove, m_GroundNormal);
            turnAmount = Mathf.Atan2(localMove.x, localMove.z);
            forwardAmount = localMove.z;
        }
        #endregion

        #region FreeOrNavMoving
//        void MoveFreely()
//        {
//            if (bUseAStarPath == false)
//            {
//                navMeshAgent.updateRotation = false;
//                navMeshAgent.velocity = Vector3.zero;
//            }
//            else
//            {
//#if RTSAStarPathfinding
//                if (myAIPath.enableRotation)
//                {
//                    myAIPath.enableRotation = false;
//                }
//                if (myAIPath.canMove)
//                {
//                    myAIPath.canMove = false;
//                }
//#endif
//            }
//            // X = Horizontal Z = Forward
//            // calculate move direction to pass to character
//            if (myCamera != null)
//            {
//                // calculate camera relative direction to move:
//                MyMove = myDirection.z * CamForward + myDirection.x * myCamera.transform.right;
//            }
//            else
//            {
//                // we use world-relative directions in the case of no main camera
//                MyMove = myDirection.z * Vector3.forward + myDirection.x * Vector3.right;
//            }
//            Move(MyMove);
//        }

        //bool PlayerWantsFreeMovement()
        //{
        //    if (isAlive == false ||
        //        allymember == null ||
        //        allymember.bIsCurrentPlayer == false) return false;
        //    myHorizontalMovement = CrossPlatformInputManager.GetAxis("Horizontal");
        //    myForwardMovement = CrossPlatformInputManager.GetAxis("Vertical");
        //    myDirection = Vector3.zero;
        //    myDirection.x = myHorizontalMovement;
        //    myDirection.z = myForwardMovement;
        //    myDirection.y = 0;
        //    if (myDirection.sqrMagnitude > 0.05f)
        //    {
        //        if (eventHandler.bIsNavMoving)
        //        {
        //            eventHandler.CallEventFinishedMoving();
        //        }
        //        if (eventHandler.bIsFreeMoving == false)
        //        {
        //            eventHandler.CallEventTogglebIsFreeMoving(true);
        //        }
        //        return true;
        //    }
        //    else
        //    {
        //        if (eventHandler.bIsFreeMoving)
        //        {
        //            eventHandler.CallEventTogglebIsFreeMoving(false);
        //        }
        //        return false;
        //    }
        //}

        //void MoveCharacterMain()
        //{
        //    //navMeshAgent.updatePosition = true;
        //    if (!navMeshAgent.isOnNavMesh)
        //    {
        //        Debug.LogError(gameObject.name + " uh oh this guy is not on the navmesh");
        //    }
        //    else if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance &&
        //        isAlive &&
        //        eventHandler != null &&
        //        eventHandler.bIsFreeMoving == false &&
        //        eventHandler.bIsNavMoving
        //        )
        //    {
        //        Move(navMeshAgent.desiredVelocity);
        //    }
        //    else if (eventHandler.bIsFreeMoving == false)
        //    {
        //        if (eventHandler.bIsNavMoving && bHasSetDestination)
        //        {
        //            if (Vector3.Distance(transform.position, navMeshAgent.destination) > navMeshAgent.stoppingDistance + 0.1f)
        //            {
        //                //TODO: RPGPrototype Fix Stopping Distance Issue, Which Causes Character to Stop Before Reaching Destination
        //                //string _msg = "Temporarily Ignoring Stopping Distance Issue." +
        //                //    $"Remaining Distance: {navMeshAgent.remainingDistance}" +
        //                //    $"Stopping Distance: {navMeshAgent.stoppingDistance}" +
        //                //    $"Distance To Destination: {Vector3.Distance(transform.position, navMeshAgent.destination)}";
        //                //Debug.Log(_msg);
        //            }
        //            else
        //            {
        //                eventHandler.CallEventFinishedMoving();
        //            }
        //        }
        //        Move(Vector3.zero);
        //    }
        //    else
        //    {
        //        if (eventHandler.bIsNavMoving)
        //        {
        //            eventHandler.CallEventFinishedMoving();
        //        }
        //    }
        //    navMeshAgent.updateRotation = true;
        //}

//        void MoveCharacterFromAStarPath()
//        {
//#if RTSAStarPathfinding
//            if (myAIPath.canMove != true)
//            {
//                myAIPath.canMove = true;
//            }
//            if (myAIPath.maxSpeed != speedMultiplier * 2)
//            {
//                myAIPath.maxSpeed = speedMultiplier * 2;
//            }

//            if (myAIPath.remainingDistance > myAIPath.endReachedDistance &&
//                isAlive &&
//                eventHandler != null &&
//                eventHandler.bIsFreeMoving == false &&
//                eventHandler.bIsNavMoving
//                )
//            {
//                Move(myAIPath.desiredVelocity);
//            }
//            else if (eventHandler.bIsFreeMoving == false)
//            {
//                if (eventHandler.bIsNavMoving && bHasSetDestination)
//                {
//                    if (Vector3.Distance(transform.position, myAIPath.destination) > myAIPath.endReachedDistance + 0.1f)
//                    {
//                        //TODO: RPGPrototype Fix Stopping Distance Issue, Which Causes Character to Stop Before Reaching Destination
//                        //string _msg = "Temporarily Ignoring Stopping Distance Issue." +
//                        //    $"Remaining Distance: {navMeshAgent.remainingDistance}" +
//                        //    $"Stopping Distance: {navMeshAgent.stoppingDistance}" +
//                        //    $"Distance To Destination: {Vector3.Distance(transform.position, navMeshAgent.destination)}";
//                        //Debug.Log(_msg);
//                    }
//                    else
//                    {
//                        eventHandler.CallEventFinishedMoving();
//                    }
//                }
//                Move(Vector3.zero);
//            }
//            else
//            {
//                if (eventHandler.bIsNavMoving)
//                {
//                    eventHandler.CallEventFinishedMoving();
//                }
//            }

//            if (myAIPath.enableRotation != true)
//            {
//                myAIPath.enableRotation = true;
//            }
//#endif
//        }
        #endregion

        #region Moving
        void Move(Vector3 movement)
        {
            SetForwardAndTurn(movement);
            ApplyExtraTurnRotation();
            UpdateAnimator();
        }

        void UpdateAnimator()
        {
            animator.SetFloat("Forward", forwardAmount * animatorForwardCap, 0.1f, Time.deltaTime);
            animator.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
            animator.speed = animationSpeedMultiplier;
            //animator.speed = animationSpeedMultiplier * speedMultiplier;
        }

        void ApplyExtraTurnRotation()
        {
            // help the character turn faster (this is in addition to root rotation in the animation)
            float turnSpeed = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, forwardAmount);
            transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
        }
        #endregion

        #region ExtraMovementCode
//        void CheckGroundStatus()
//        {
//            RaycastHit hitInfo;
//#if UNITY_EDITOR
//            // helper to visualise the ground check ray in the scene view
//            Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
//#endif
//            // 0.1f is a small offset to start the ray from inside the character
//            // it is also good to note that the transform position in the sample assets is at the base of the character
//            if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
//            {
//                m_GroundNormal = hitInfo.normal;
//                m_IsGrounded = true;
//                animator.applyRootMotion = true;
//            }
//            else
//            {
//                m_IsGrounded = false;
//                m_GroundNormal = Vector3.up;
//                animator.applyRootMotion = false;
//            }
//        }
        #endregion

    }
}
