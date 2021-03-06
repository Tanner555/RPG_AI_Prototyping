﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTSCoreFramework;

namespace RPGPrototype
{
    public class SelfHealBehaviourRPG : AbilityBehaviourRPG
    {
        #region Properties
        RTSGameMaster gamemaster
        {
            get { return RTSGameMaster.thisInstance; }
        }

        AllyEventHandler eventhandler
        {
            get
            {
                if (_eventhandler == null)
                    _eventhandler = GetComponent<AllyEventHandler>();

                return _eventhandler;
            }
        }
        AllyEventHandler _eventhandler = null;
        AllyMember allymember
        {
            get
            {
                if (_allymember == null)
                    _allymember = GetComponent<AllyMember>();

                return _allymember;
            }
        }
        AllyMember _allymember = null;
        #endregion

        public override void Use(GameObject target = null)
        {
            float _extraHealth = (config as SelfHealConfigRPG).GetExtraHealth();
            PlayAbilitySound();
            allymember.AllyHeal((int)_extraHealth);
            PlayParticleEffect();
            PlayAbilityAnimation();
        }
    }
}