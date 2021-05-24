using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseFramework;
using RTSCoreFramework;
using UnityStandardAssets.CrossPlatformInput;

namespace RPGPrototype
{
    public class RPGBehaviorActions : RTSBehaviorActions
    {
        #region OverrideAndHideProperties
        protected new RPGInputManager myInputManager => InputManager.thisInstance as RPGInputManager;
        #endregion

        #region Conditions

        #region PlayerWantsFreeMovement
        protected override void CalculateMoveInputFromManager()
        {
            myHorizontalMovement = myInputManager.HorizontalMovement;
            myForwardMovement = myInputManager.ForwardMovement;
        }

        protected override void CalculateMoveInputFromOLDCrossPlatformManager()
        {
            myHorizontalMovement = CrossPlatformInputManager.GetAxis("Horizontal");
            myForwardMovement = CrossPlatformInputManager.GetAxis("Vertical");
        }
        #endregion

        #endregion

        #region Actions

        #endregion

        #region Initialization
        public RPGBehaviorActions(Transform transform)
        {
            this.transform = transform;
            this.gameObject = transform.gameObject;
        }

        protected RPGBehaviorActions()
        {

        }
        #endregion
    }
}