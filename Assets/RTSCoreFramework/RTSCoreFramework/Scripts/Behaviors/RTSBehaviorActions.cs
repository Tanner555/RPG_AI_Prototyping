using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseFramework;

namespace RTSCoreFramework
{
    public class RTSBehaviorActions
    {
        #region EssentialComponents
        public Transform transform { get; protected set; }
        AllyMember allyMember
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

        AllyAIController aiController
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

        AllyEventHandler myEventHandler
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
        IAllyMovable allyMovable
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
        #endregion

        #region PropertyReferences
        protected InputManager myInputManager => InputManager.thisInstance;
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
        Vector3 CamForward
        {
            get
            {
                return Vector3.Scale(myCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
            }
        }
        #endregion

        #region Getters
        bool bIsAlive => allyMember != null && allyMember.IsAlive;
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

        #region SetNavDestFromTargetPos
        public bool SetNavDestFromTargetPos(Vector3 MyNavDestination, bool bHasSetDestination, Transform CurrentTargettedEnemy, 
            out (Vector3 MyNavDestination, bool bHasSetDestination, Transform CurrentTargettedEnemy) allRefs)
        {
            bool _success = SetNavDestFromTargetPos(ref MyNavDestination, ref bHasSetDestination, ref CurrentTargettedEnemy);
            allRefs = (MyNavDestination, bHasSetDestination, CurrentTargettedEnemy);
            return _success;
        }

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