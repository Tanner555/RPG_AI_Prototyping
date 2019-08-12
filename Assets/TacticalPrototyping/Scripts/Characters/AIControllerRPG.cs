using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTSCoreFramework;
using RPG.Characters;

namespace RPGPrototype
{
    public class AIControllerRPG : AllyAIController
    {
        #region Fields
        WeaponConfig myRPGWeapon = null;

        #endregion

        #region ComponentsAndSingletons
        new RPGGameMaster gamemaster
        {
            get { return RPGGameMaster.thisInstance; }
        }

        new AllyEventHandlerRPG myEventHandler
        {
            get
            {
                if (_eventhandler == null)
                    _eventhandler = GetComponent<AllyEventHandlerRPG>();

                return _eventhandler;
            }
        }
        AllyEventHandlerRPG _eventhandler = null;

        new AllyMemberRPG allyMember
        {
            get
            {
                if (_allymember == null)
                    _allymember = GetComponent<AllyMemberRPG>();

                return _allymember;
            }
        }
        AllyMemberRPG _allymember = null;
        #endregion

        #region Properties
        bool bIsDead
        {
            get
            {
                return allyMember == null ||
                  allyMember.IsAlive == false;
            }
        }

        bool bIsMeleeing
        {
            get
            {
                return myEventHandler.bIsMeleeingEnemy;
            }
        }
        #endregion

        #region Getters
        bool IsTargetInRange(GameObject target)
        {
            if (myRPGWeapon == null) return false;
            float distanceToTarget = (target.transform.position - transform.position).magnitude;
            return distanceToTarget <= myRPGWeapon.GetMaxAttackRange();
        }
        #endregion

        #region Handlers
        void PutWeaponInHand(WeaponConfig _config)
        {
            myRPGWeapon = _config;
        }
        #endregion

        #region ShootingAndBattleBehavior
        protected override void UpdateBattleBehavior()
        {
            // Pause Ally Tactics If Ally Is Paused
            // Due to the Game Pausing Or Control Pause Mode
            // Is Active
            if (myEventHandler.bAllyIsPaused) return;

            if (currentTargettedEnemy == null ||
                currentTargettedEnemy.IsAlive == false ||
                myEventHandler.bIsFreeMoving)
            {
                myEventHandler.CallEventStopTargettingEnemy();
                myEventHandler.CallEventFinishedMoving();
                return;
            }

            if (IsTargetInRange(currentTargettedEnemy.gameObject))
            {
                if (bIsMeleeing == false)
                {
                    myEventHandler.CallAttackRPGTarget(currentTargettedEnemy.gameObject);
                }
            }
            else
            {
                if (bIsMeleeing == true)
                {
                    myEventHandler.CallStopAttackingRPGTarget();
                }

                myEventHandler.CallEventAIMove(currentTargettedEnemy.transform.position);
            }
        }
        #endregion

        #region Initialization
        protected override void SubToEvents()
        {
            base.SubToEvents();
            myEventHandler.PutRPGWeaponInHand += PutWeaponInHand;
        }

        protected override void UnSubFromEvents()
        {
            base.UnSubFromEvents();
            myEventHandler.PutRPGWeaponInHand -= PutWeaponInHand;
        }
        #endregion

        /// <summary>
        /// Temporary Method To Prevent Animation Event Errors
        /// </summary>
        public void Hit()
        {
            //TODO: RPGPrototype-Find another way to stop Hit Animation Event Errors
        }

    }
}