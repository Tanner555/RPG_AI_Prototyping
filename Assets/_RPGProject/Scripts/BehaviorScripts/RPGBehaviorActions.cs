using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseFramework;
using RTSCoreFramework;

namespace RPGPrototype
{
    public class RPGBehaviorActions : RTSBehaviorActions
    {
        #region OverrideAndHideProperties
        protected new RPGInputManager myInputManager => InputManager.thisInstance as RPGInputManager;
        #endregion

        #region Initialization
        public RPGBehaviorActions(Transform transform)
        {
            this.transform = transform;
        }

        protected RPGBehaviorActions()
        {

        }
        #endregion
    }
}