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

        #region SetNavDestFromTargetPos
        /// <summary>
        /// Resets The Provided Character Navigation Movement BlackBoard Variables and Nav
        /// </summary>
        public bool SetNavDestFromTargetPos(Vector3 MyNavDestination, bool bHasSetDestination, Transform CurrentTargettedEnemy,
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