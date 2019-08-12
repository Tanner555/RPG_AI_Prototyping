using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTSCoreFramework;

namespace RPGPrototype
{
    public class AllyMemberRPG : AllyMember
    {
        #region Fields

        #endregion

        #region Properties
        public override int AllyHealth
        {
            get { return allyStatController.Stat_Health; }
            protected set { allyStatController.Stat_Health = value; }
        }

        public override int AllyMaxHealth
        {
            get { return allyStatController.Stat_MaxHealth; }
        }

        public override int AllyStamina
        {
            get { return allyStatController.Stat_Stamina; }
            protected set { allyStatController.Stat_Stamina = value; }
        }

        public override int AllyMaxStamina
        {
            get { return allyStatController.Stat_MaxStamina; }
        }

        public override string CharacterName
        {
            get
            {
                return allyStatController.Stat_CharacterName;
            }
        }

        public override ECharacterType CharacterType
        {
            get
            {
                return allyStatController.Stat_CharacterType;
            }
        }

        public override Sprite CharacterPortrait
        {
            get
            {
                return allyStatController.Stat_CharacterPortrait;
            }
        }

        //public AllyMemberWrapper enemyTargetWrapper
        //{
        //    get { return aiControllerWrapper.currentTargettedEnemyWrapper; }
        //}

        #endregion

        #region Health
        public float healthAsPercentage { get { return AllyHealth / AllyMaxHealth; } }

        #endregion

        #region UnityMessages
        protected override void Start()
        {
            base.Start();

        }
        #endregion

        #region Handlers
        public override void AllyTakeDamage(int amount, Vector3 position, Vector3 force, AllyMember _instigator, GameObject hitGameObject)
        {
            base.AllyTakeDamage(amount, position, force, _instigator, hitGameObject);
            //if (bIsCurrentPlayer)
            //    EventHandler.ExecuteEvent<float, Vector3, Vector3, GameObject>(gameObject, "OnHealthDamageDetails", amount, position, force, _instigator.gameObject);
            
            //if (IsAlive == false)
            //{
            //    EventHandler.ExecuteEvent<Vector3, Vector3, GameObject>(gameObject, "OnDeathDetails", force, position, _instigator.gameObject);
            //}
        }

        public override void AllyOnDeath()
        {
            base.AllyOnDeath();
        }


        #endregion

        #region Getters
        public override int GetDamageRate()
        {
            return allyStatController.CalculateDamageRate();
        }
        #endregion
    }
}