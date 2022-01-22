using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseFramework;
using UnityEngine.AI;

namespace RTSCoreFramework
{
    public class RTSBehaviorActions
    {
        #region EssentialComponents
        public Transform transform { get; protected set; }

        public GameObject gameObject { get; protected set; }

        protected AllyMember allyMember
        {
            get
            {
                if (_allymember == null)
                {
                    _allymember = transform.GetComponent<AllyMember>();
                }
                return _allymember;
            }
        }
        AllyMember _allymember = null;

        protected AllyAIController aiController
        {
            get
            {
                if (_aiController == null)
                {
                    _aiController = transform.GetComponent<AllyAIController>();
                }
                return _aiController;
            }
        }
        AllyAIController _aiController = null;

        protected AllyEventHandler myEventHandler
        {
            get
            {
                if (_myEventhandler == null)
                {
                    _myEventhandler = transform.GetComponent<AllyEventHandler>();
                }
                return _myEventhandler;
            }
        }
        AllyEventHandler _myEventhandler = null;
        protected IAllyMovable allyMovable
        {
            get
            {
                if (_allyMovable == null)
                {
                    _allyMovable = transform.GetComponent(typeof(IAllyMovable)) as IAllyMovable;
                }
                return _allyMovable;
            }
        }
        IAllyMovable _allyMovable = null;

        protected NavMeshAgent navMeshAgent
        {
            get
            {
                if (_navMeshAgent == null)
                {
                    _navMeshAgent = transform.GetComponent<NavMeshAgent>();
                }
                return _navMeshAgent;
            }
        }
        NavMeshAgent _navMeshAgent = null;
        #endregion

        #region PropertyReferences
        protected InputManager myInputManager => InputManager.thisInstance;
        protected Camera myCamera
        {
            get
            {
                if (_myCamera == null)
                    _myCamera = Camera.main;

                return _myCamera;
            }
        }
        Camera _myCamera = null;
        protected Vector3 CamForward
        {
            get
            {
                return Vector3.Scale(myCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
            }
        }
        #endregion

        #region OtherProperties
        protected int AllyActiveTimeBar
        {
            get
            {
                return allyMember.AllyActiveTimeBar;
            }
            set
            {
                allyMember.AllyActiveTimeBar = value;
            }
        }
        #endregion

        #region Getters
        protected bool bIsAlive => allyMember != null && allyMember.IsAlive;

        protected int AllyMaxActiveTimeBar
        {
            get
            {
                return allyMember.AllyMaxActiveTimeBar;
            }
        }
        #endregion

        #region Fields
        //Update Waypoint Renderer
        protected LineRenderer waypointRenderer;
        protected NavMeshPath myNavPath = null;
        //PlayerWantsFreeMovement
        protected float myHorizontalMovement, myForwardMovement = 0.0f;
        protected Vector3 myDirection = Vector3.zero;
        #endregion

        #region Initialization
        public RTSBehaviorActions(Transform transform)
        {
            this.transform = transform;
            this.gameObject = transform.gameObject;
        }

        protected RTSBehaviorActions()
        {

        }
        #endregion

        #region Conditions

        #region IsActiveTimeBarFull
        /// <summary>
        /// Returns True if The Active Time Bar Is Full.
        /// Will Return False if AllyMember doesn't exist.
        /// </summary>
        public bool IsActiveTimeBarFull()
        {
            return allyMember != null && allyMember.ActiveTimeBarIsFull();
        }
        #endregion

        #region PlayerWantsFreeMovement
        /// <summary>
        /// Returns Success if Owner is Current Player and Movement Input Has Been Received
        /// </summary>
        public bool PlayerWantsFreeMovement(ref Vector3 MyMoveDirection, ref bool bIsFreeMoving, 
            bool bUseNewInputSystem = true)
        {
            if (bIsAlive == false ||
                allyMember == null ||
                allyMember.bIsCurrentPlayer == false)
            {
                ResetFreeMoveDirection(ref MyMoveDirection);
                bIsFreeMoving = false;
                return false;
            }

            if (bUseNewInputSystem)
            {
                CalculateMoveInputFromManager();
            }
            else
            {
                CalculateMoveInputFromOLDCrossPlatformManager();
            }
            myDirection = Vector3.zero;
            myDirection.x = myHorizontalMovement;
            myDirection.z = myForwardMovement;
            myDirection.y = 0;

            if (myDirection.sqrMagnitude > 0.05f)
            {
                //Also Calculate Move Direction Used For Movement Task
                CalculateFreeMoveDirection(ref MyMoveDirection);
                bIsFreeMoving = true;
                return true;
            }
            else
            {
                ResetFreeMoveDirection(ref MyMoveDirection);
                bIsFreeMoving = false;
                return false;
            }
        }

        protected virtual void CalculateMoveInputFromManager()
        {

        }

        protected virtual void CalculateMoveInputFromOLDCrossPlatformManager()
        {

        }

        protected void ResetFreeMoveDirection(ref Vector3 MyMoveDirection)
        {
            MyMoveDirection = Vector3.zero;
        }

        protected void CalculateFreeMoveDirection(ref Vector3 MyMoveDirection)
        {
            // X = Horizontal Z = Forward
            // calculate move direction to pass to character
            if (myCamera != null)
            {
                // calculate camera relative direction to move:
                MyMoveDirection = myDirection.z * CamForward + myDirection.x * myCamera.transform.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                MyMoveDirection = myDirection.z * Vector3.forward + myDirection.x * Vector3.right;
            }
        }
        #endregion

        #region IsAllyTargetInRange
        /// <summary>
        /// Checks If Target is In Range Using RPG Weapon Attack Range.
        /// </summary>
        public virtual bool IsAllyTargetInRange(ref Transform CurrentTargettedEnemy)
        {
            Debug.Log("IsAllyTargetInRange Should Be Implemented In RPG Behavior Actions.");
            return false;
        }
        #endregion

        #region IsAllyTargetValid
        /// <summary>
        /// Simple Valid Target Check using bTargetEnemy and CurrentTargettedEnemy. 
        /// CheckAllyAndResetTargetIfFail Checkbox Will Check Ally (Not NULL and isAlive) 
        /// And Reset Target if Fails
        /// </summary>
        public bool IsAllyTargetValid(ref bool bTargetEnemy, ref Transform CurrentTargettedEnemy, bool CheckAllyAndResetTargetIfFail = false)
        {
            //Updates _CurrentTargettedEnemyAlly Var Instead of Using A Property
            IsAllyTargetValid_UpdateCurrTarget(ref CurrentTargettedEnemy);

            if (CheckAllyAndResetTargetIfFail)
            {
                //Ally Check and Reset If Failed
                if (bTargetEnemy && CurrentTargettedEnemy != null &&
                    _IsAllyTargetValid_CurrTarget != null && _IsAllyTargetValid_CurrTarget.IsAlive)
                {
                    return true;
                }
                else
                {
                    bTargetEnemy = false;
                    CurrentTargettedEnemy = null;
                    IsAllyTargetValid_ResetCurrTarget();
                    return false;
                }
            }
            else
            {
                //Simple Validity Check, No Reset
                if (bTargetEnemy && CurrentTargettedEnemy != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //Helpers
        private void IsAllyTargetValid_UpdateCurrTarget(ref Transform currentTargetTransform)
        {
            //Don't Retrieve AllyMember if TargetTransform Doesn't Exist
            if (currentTargetTransform == null) return;
            //If TargetAllyComp is NULL or TargetAllyComp is a Reference of another Ally (Switched Target)
            if (_IsAllyTargetValid_CurrTarget == null ||
                _IsAllyTargetValid_CurrTarget.transform != currentTargetTransform)
            {
                _IsAllyTargetValid_CurrTarget = currentTargetTransform.GetComponent<AllyMember>();
            }
        }

        private void IsAllyTargetValid_ResetCurrTarget()
        {
            _IsAllyTargetValid_CurrTarget = null;
        }

        private AllyMember _IsAllyTargetValid_CurrTarget = null;
        #endregion

        #endregion

        #region Actions

        #region DepleteActiveTimeBar
        /// <summary>
        /// Empties The Active Time Bar. OnlyDepleteIfAboveMinimum Checkbox Will Only Deplete Time Bar if Amount is Greater Than 0.
        /// </summary>
        public bool DepleteActiveTimeBar(bool OnlyDepleteIfAboveMinimum = false)
        {
            if (OnlyDepleteIfAboveMinimum)
            {
                if (allyMember.AllyActiveTimeBar > allyMember.AllyMinActiveTimeBar)
                {
                    allyMember.DepleteActiveTimeBar();
                }
            }
            else
            {
                allyMember.DepleteActiveTimeBar();
            }
            return true;
        }
        #endregion

        #region UpdateActiveTimeBar
        /// <summary>
        /// Updates Active Time Bar if bUpdateActiveTimeBar is Set. ActiveTimeBarRefillRate Determines How Much the Bar is Filled On Each Update.
        /// </summary>
        public bool UpdateActiveTimeBar(bool bUpdateActiveTimeBar, int ActiveTimeBarRefillRate)
        {
            if (bUpdateActiveTimeBar && AllyActiveTimeBar < AllyMaxActiveTimeBar)
            {
                AllyActiveTimeBar = Mathf.Min(AllyActiveTimeBar + ActiveTimeBarRefillRate, AllyMaxActiveTimeBar);
            }
            return true;
        }
        #endregion

        #region ResetCharacterNavMovement
        /// <summary>
        /// Resets The Provided Character Navigation Movement BlackBoard Variables and NavMeshAgent Dest. Optional OnlyResetIfHasSetDestination Checkbox Will Only Reset Variables If Destination Has Been Set.
        /// </summary>
        public bool ResetCharacterNavMovement(ref Vector3 MyNavDestination, ref bool bHasSetDestination, 
            ref bool bHasSetCommandMove, bool OnlyResetIfHasSetDestination = false)
        {
            if (OnlyResetIfHasSetDestination)
            {
                //Only Reset if Destination has been set.
                if (bHasSetDestination)
                {
                    ResetCharacterNavMovement_Helper(ref MyNavDestination, ref bHasSetDestination, ref bHasSetCommandMove);
                }
            }
            else
            {
                ResetCharacterNavMovement_Helper(ref MyNavDestination, ref bHasSetDestination, ref bHasSetCommandMove);
            }
            return true;
        }

        /// <summary>
        /// Helper For Action: ResetCharacterNavMovement.  
        /// </summary>
        protected void ResetCharacterNavMovement_Helper(ref Vector3 MyNavDestination, ref bool bHasSetDestination, ref bool bHasSetCommandMove)
        {
            MyNavDestination = Vector3.zero;
            bHasSetDestination = false;
            bHasSetCommandMove = false;
            navMeshAgent.SetDestination(transform.position);
            navMeshAgent.velocity = Vector3.zero;
        }
        #endregion

        #region MoveDirFromNavDestination
        /// <summary>
        /// Provides Move Direction From Navigation Destination.
        /// </summary>
        public bool MoveDirFromNavDestination(ref Vector3 MyMoveDirection, 
            ref Vector3 MyNavDestination, ref bool bFinishedMoving)
        {
            //By Default, Will Keep Moving Until in a Finished State
            bFinishedMoving = false;

            if (navMeshAgent == null) Debug.LogError(gameObject.name + "navmesh is null");
            if (!navMeshAgent.isOnNavMesh) Debug.LogError(gameObject.name + " uh oh this guy is not on the navmesh");
            if (navMeshAgent == null || !navMeshAgent.isOnNavMesh)
            {
                //Stop Moving and Finish Task
                bFinishedMoving = true;
                return false;
            }

            if (navMeshAgent.destination != MyNavDestination) navMeshAgent.SetDestination(MyNavDestination);

            if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
            {
                navMeshAgent.updateRotation = true;
                MyMoveDirection = navMeshAgent.desiredVelocity;
                //Haven't Finished Moving Yet, Returning Success Temporarily
                return true;
            }
            else if (Vector3.Distance(transform.position, navMeshAgent.destination) > navMeshAgent.stoppingDistance + 0.1f)
            {
                //Fix Stopping Distance Issue, Which Causes Character to Stop Before Reaching Destination
                //string _msg = "Temporarily Ignoring Stopping Distance Issue." +
                //    $"Remaining Distance: {navMeshAgent.remainingDistance}" +
                //    $"Stopping Distance: {navMeshAgent.stoppingDistance}" +
                //    $"Distance To Destination: {Vector3.Distance(transform.position, navMeshAgent.destination)}";
                //Debug.Log(_msg);
                navMeshAgent.updateRotation = true;
                MyMoveDirection = Vector3.zero;
                //Haven't Finished Moving Yet, Returning Success Temporarily
                return true;
            }
            else
            {
                //Finished Moving, Stop Running This Task
                bFinishedMoving = true;
                return true;
            }
        }
        #endregion

        #region UpdateWaypointRenderer
        /// <summary>
        /// Updates Waypoint When Command Navigation Moving.
        /// </summary>
        public bool UpdateWaypointRenderer(ref Material waypointMaterial,
            ref Color waypointStartColor, ref Color waypointEndColor,
            float waypointStartWidth = 0.05f, float waypointEndWidth = 0.05f)
        {
            if (waypointRenderer != null && waypointRenderer.enabled == false)
            {
                //Enable Line Renderer if Comp Not Null and Isn't Enabled.
                waypointRenderer.enabled = true;
            }
            else if (waypointRenderer == null)
            {
                //Add Line Renderer To This GameObject
                waypointRenderer = this.gameObject.AddComponent<LineRenderer>();
                if (waypointMaterial != null)
                {
                    waypointRenderer.material = waypointMaterial;
                }
                waypointRenderer.startWidth = waypointStartWidth;
                waypointRenderer.endWidth = waypointEndWidth;
                waypointRenderer.startColor = waypointStartColor;
                waypointRenderer.endColor = waypointEndColor;
            }
            //Get All Corners From NavMeshAgent Path
            myNavPath = navMeshAgent.path;
            waypointRenderer.positionCount = myNavPath.corners.Length;
            //Iterate All Path Corners, Setting The Line Render Positions
            for (int i = 0; i < myNavPath.corners.Length; i++)
            {
                waypointRenderer.SetPosition(i, myNavPath.corners[i]);
            }
            //Task Successful
            return true;
        }
        #endregion

        #region ResetWaypointRenderer
        /// <summary>
        /// Disables Waypoint Renderer If It Exists and Is Enabled.
        /// </summary>
        public bool ResetWaypointRenderer()
        {
            if (waypointRenderer != null)
            {
                waypointRenderer.enabled = false;
            }
            return true;
        }
        #endregion

        #region MoveRPGCharacter
        /// <summary>
        /// Moves RPG Character From A Given Direction Using The Animator.
        /// </summary>
        public bool MoveRPGCharacter(ref Vector3 MyMoveDirection, ref bool bIsFreeMoving)
        {
            if (allyMovable != null)
            {
                allyMovable.MoveAlly(MyMoveDirection, bIsFreeMoving);
                return true;
            }
            return false;
        }
        #endregion

        #region SetNavDestFromTargetPos
        /// <summary>
        /// Sets Nav Destination To Target Position.
        /// </summary>
        public bool SetNavDestFromTargetPos(ref Vector3 MyNavDestination, ref bool bHasSetDestination, ref Transform CurrentTargettedEnemy)
        {
            MyNavDestination = CurrentTargettedEnemy.position;
            bHasSetDestination = true;
            return true;
        }
        #endregion

        #region DamageTarget
        /// <summary>
        /// Damages The Ally Target. Doesn't Use Any Animations.
        /// </summary>
        public bool DamageTarget(ref Transform CurrentTargettedEnemy)
        {
            int _damage = allyMember.GetDamageRate();
            AllyMember _ally = CurrentTargettedEnemy.GetComponent<AllyMember>();
            _ally.AllyTakeDamage(_damage, allyMember);
            return true;
        }
        #endregion

        #region TryAttackTarget
        /// <summary>
        /// Attempts To Use Weapon And Attack Enemy. 
        /// Bind HalfWeaponAttackRate Variable For Wait Node.
        /// Currently Doesn't Apply Damage To Target. 
        /// Use DamageTarget Task To Apply Damage.
        /// </summary>
        public bool TryAttackTarget(ref float HalfWeaponAttackRate, ref Transform CurrentTargettedEnemy)
        {
            HalfWeaponAttackRate = (aiController.GetAttackRate()) / 2;
            myEventHandler.CallOnTryUseWeapon(CurrentTargettedEnemy);
            return true;
        }
        #endregion

        #region ResetHasTargetAndTransform
        /// <summary>
        /// Resets bTargetEnemy and CurrentTargettedEnemy If Character Is Free Moving. 
        /// AlsoResetsIfUsingAbility Checkbox Will Also Reset Target if Using Ability.
        /// </summary>
        public bool ResetHasTargetAndTransform(ref bool bIsFreeMoving,
            ref bool bTargetEnemy, ref Transform CurrentTargettedEnemy, 
            ref bool AlsoResetsIfUsingAbility, ref bool bTryUseAbility)
        {
            if (AlsoResetsIfUsingAbility)
            {
                //Also Reset If Using Ability
                if (bIsFreeMoving || bTryUseAbility)
                {
                    bTargetEnemy = false;
                    CurrentTargettedEnemy = null;
                }
            }
            else
            {
                //Normal FreeMoving Check
                if (bIsFreeMoving)
                {
                    bTargetEnemy = false;
                    CurrentTargettedEnemy = null;
                }
            }
            return true;
        }
        #endregion

        #endregion
    }
}