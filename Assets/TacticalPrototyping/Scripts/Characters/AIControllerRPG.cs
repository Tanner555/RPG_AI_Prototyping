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
        //Extra
        bool bUseAStarPath = false;
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

        protected override bool AllCompsAreValid => myEventHandler && allyMember;
        #endregion

        #region Properties

        #endregion

        #region UnityMessages
        protected override void Start()
        {
            //Overriding AIController To Remove Checks and StartServices
        }
        #endregion

        #region Getters
        public override bool isSurfaceWalkable(RaycastHit hit)
        {
            if (bUseAStarPath == false)
            {
                return base.isSurfaceWalkable(hit);
            }
            return false;
        }

        public override bool isSurfaceWalkable(Vector3 _point)
        {
            if (bUseAStarPath == false)
            {
                return base.isSurfaceWalkable(_point);
            }
            return false;
        }

        bool IsTargetInRange(GameObject target)
        {
            if (myRPGWeapon == null) return false;
            float distanceToTarget = (target.transform.position - transform.position).magnitude;
            return distanceToTarget <= myRPGWeapon.GetMaxAttackRange();
        }

        public override float GetAttackRate()
        {
            return 0.25f;
        }
        #endregion

        #region Handlers
        protected override void OnAllyInitComps(RTSAllyComponentSpecificFields _specific, RTSAllyComponentsAllCharacterFields _allFields)
        {
            base.OnAllyInitComps(_specific, _allFields);
            var _RPGallAllyComps = (AllyComponentsAllCharacterFieldsRPG)_allFields;
            this.bUseAStarPath = _RPGallAllyComps.bUseAStarPath;
        }

        void PutWeaponInHand(WeaponConfig _config)
        {
            myRPGWeapon = _config;
            //Moved Comps Check Into PutWeaponInHad Handler
            if (!AllCompsAreValid)
            {
                Debug.LogError("Not all comps are valid!");
            }
            StartServices();
        }
        #endregion

        #region ShootingAndBattleBehavior
        protected override void UpdateBattleBehavior()
        {
            RPGUpdateBattleBehaviorOLD();
        }
        
        private void RPGUpdateBattleBehaviorOLD()
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
                    //myEventHandler.CallAttackRPGTarget(currentTargettedEnemy.gameObject);
                    myEventHandler.CallEventFinishedMoving();
                    StartMeleeAttackBehavior();
                }
            }
            else
            {
                if (bIsMeleeing == true)
                {
                    //myEventHandler.CallStopAttackingRPGTarget();
                    StopMeleeAttackBehavior();
                }

                myEventHandler.CallEventAIMove(currentTargettedEnemy.transform.position);
            }
        }

        //Probably Won't Use, From RTSCoreFramework AIController
        private void RPGUpdateBattleBehaviorNEW()
        {
            // Pause Ally Tactics If Ally Is Paused
            // Due to the Game Pausing Or Control Pause Mode
            // Is Active
            if (myEventHandler.bAllyIsPaused) return;

            if (bStopUpdatingBattleBehavior)
            {
                myEventHandler.CallEventStopTargettingEnemy();
                myEventHandler.CallEventFinishedMoving();
                return;
            }

            //Melee Behavior
            if (IsTargetInMeleeRange(currentTargettedEnemy.gameObject))
            {
                if (bIsMeleeing == false)
                {
                    StartMeleeAttackBehavior();
                    myEventHandler.CallEventFinishedMoving();
                }
            }
            else
            {
                if (bIsMeleeing == true)
                {
                    StopMeleeAttackBehavior();
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