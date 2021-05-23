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
        public bool ResetCharacterNavMovement(Vector3 MyNavDestination, bool bHasSetDestination, bool bHasSetCommandMove,
            ref System.Action<Vector3, bool, bool> SetterAction, bool OnlyResetIfHasSetDestination = false)
        {
            bool _success = ResetCharacterNavMovement(ref MyNavDestination, ref bHasSetDestination, ref bHasSetCommandMove, OnlyResetIfHasSetDestination);
            if (SetterAction != null)
            {
                SetterAction(MyNavDestination, bHasSetDestination, bHasSetCommandMove);
            }
            return _success;
        }

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
        public void ResetCharacterNavMovement_Helper(ref Vector3 MyNavDestination, ref bool bHasSetDestination, ref bool bHasSetCommandMove)
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
            ref Vector3 MyNavDestination, ref bool bFinishedMoving,
            ref System.Action<Vector3, Vector3, bool> SetterAction)
        {
            bool _success = MoveDirFromNavDestination(ref MyMoveDirection, ref MyNavDestination, ref bFinishedMoving);
            if (SetterAction != null)
            {
                SetterAction(MyMoveDirection, MyNavDestination, bFinishedMoving);
            }
            return _success;
        }

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

        #region SetNavDestFromTargetPos
        /// <summary>
        /// Resets The Provided Character Navigation Movement BlackBoard Variables and Nav
        /// </summary>
        public bool SetNavDestFromTargetPos(ref Vector3 MyNavDestination, ref bool bHasSetDestination, ref Transform CurrentTargettedEnemy,
            ref System.Action<Vector3, bool, Transform> SetterAction)
        {
            bool _success = SetNavDestFromTargetPos(ref MyNavDestination, ref bHasSetDestination, ref CurrentTargettedEnemy);
            if (SetterAction != null)
            {
                SetterAction(MyNavDestination, bHasSetDestination, CurrentTargettedEnemy);
            }
            return _success;
        }

        /// <summary>
        /// Resets The Provided Character Navigation Movement BlackBoard Variables and Nav
        /// </summary>
        public bool SetNavDestFromTargetPos(Vector3 MyNavDestination, bool bHasSetDestination, Transform CurrentTargettedEnemy,
            out (Vector3 MyNavDestination, bool bHasSetDestination, Transform CurrentTargettedEnemy) allRefs)
        {
            bool _success = SetNavDestFromTargetPos(ref MyNavDestination, ref bHasSetDestination, ref CurrentTargettedEnemy);
            allRefs = (MyNavDestination, bHasSetDestination, CurrentTargettedEnemy);
            return _success;
        }

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


        #endregion
    }
}