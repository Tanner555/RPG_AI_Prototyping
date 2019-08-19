using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using RTSCoreFramework;
using RPG.Characters;

namespace RPGPrototype
{
    #region RTSAllyComponentSpecificFields
    [System.Serializable]
    public class AllyComponentSpecificFieldsRPG : RTSAllyComponentSpecificFields
    {
        [Header("RPG Character Attributes")]
        [SerializeField]
        public RPGAllySpecificCharacterAttributesObject RPGCharacterAttributesObject;
    }
    #endregion

    #region RTSAllyComponentsAllCharacterFields
    [System.Serializable]
    public class AllyComponentsAllCharacterFieldsRPG : RTSAllyComponentsAllCharacterFields
    {
        
    }
    #endregion

    public class AllyEventHandlerRPG : AllyEventHandler
    {
        #region Fields
        public bool bIsMeleeingEnemy = false;
        #endregion

        #region Delegates
        public delegate void OneGameObjectParamHandler(GameObject _target);
        public event OneGameObjectParamHandler AttackRPGTarget;
        public event GeneralEventHandler StopAttackingRPGTarget;

        public delegate void WeaponConfigParamHandler(WeaponConfig weaponToUse);
        public event WeaponConfigParamHandler PutRPGWeaponInHand;
        #endregion

        #region Calls
        public void CallAttackRPGTarget(GameObject _target)
        {
            bIsMeleeingEnemy = true;
            if (AttackRPGTarget != null) AttackRPGTarget(_target);
        }

        public void CallStopAttackingRPGTarget()
        {
            bIsMeleeingEnemy = false;
            if (StopAttackingRPGTarget != null) StopAttackingRPGTarget();
        }

        public void CallPutRPGWeaponInHand(WeaponConfig _weapon)
        {
            if (PutRPGWeaponInHand != null) PutRPGWeaponInHand(_weapon);
        }
        #endregion

        #region OverrideCalls
        protected override void CallEventCommandMove(Vector3 _point)
        {
            CallStopAttackingRPGTarget();
            base.CallEventCommandMove(_point);
        }

        public override void CallEventStopTargettingEnemy()
        {
            CallStopAttackingRPGTarget();
            base.CallEventStopTargettingEnemy();
        }
        #endregion
    }
}