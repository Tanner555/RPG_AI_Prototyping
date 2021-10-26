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
        protected new AIControllerRPG aiController => base.aiController as AIControllerRPG;
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

        #region IsAllyTargetInRange
        /// <summary>
        /// Checks If Target is In Range Using RPG Weapon Attack Range.
        /// </summary>
        public override bool IsAllyTargetInRange(ref Transform CurrentTargettedEnemy)
        {
            if (aiController.myRPGWeapon == null)
            {
                Debug.LogWarning("myRPGWeapon is NULL, couldn't update target in range task");
                return false;
            }
            float distanceToTarget = (CurrentTargettedEnemy.position - transform.position).magnitude;
            if (distanceToTarget <= aiController.myRPGWeapon.GetMaxAttackRange())
            {
                return true;
            }
            else
            {
                return false;
            }
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