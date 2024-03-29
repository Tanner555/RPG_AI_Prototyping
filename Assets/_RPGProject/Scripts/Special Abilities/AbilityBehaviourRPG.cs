﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTSCoreFramework;

namespace RPGPrototype
{
    public abstract class AbilityBehaviourRPG : AbilityBehaviour
    {
        public override abstract void Use(GameObject target = null);

        const string ATTACK_TRIGGER = "Attack";
        const string DEFAULT_ATTACK_STATE = "DEFAULT ATTACK";
        const float PARTICLE_CLEAN_UP_DELAY = 20f;

        //public Opsive.UltimateCharacterController.Character.Abilities.Ability TPCAbility
        //{
        //    get
        //    {
        //        if (_TPCAbility == null)
        //            _TPCAbility = GetTPCAbility();

        //        return _TPCAbility;
        //    }
        //}
        //private Opsive.UltimateCharacterController.Character.Abilities.Ability _TPCAbility = null;

        protected override void PlayAbilityAnimation()
        {
            if (CanUseAbility())
            {
                //TPCAbility.StartAbility();
                //allyEventHandler.CallEventToggleIsUsingAbility(true);
                //Invoke("StopAbilityAnimation", config.GetAbilityAnimationTime());
                //From Old Ability Behaviour Method
                var _rpgconfig = (AbilityConfigRPG)config;
                var animatorOverrideController = GetComponent<RPGCharacter>().GetOverrideController();
                var animator = GetComponent<Animator>();
                animator.runtimeAnimatorController = animatorOverrideController;
                animatorOverrideController[DEFAULT_ATTACK_STATE] = _rpgconfig.GetAbilityAnimation();
                animator.SetTrigger(ATTACK_TRIGGER);
            }
        }

        public override void StopAbilityAnimation()
        {
            //TPCAbility.StopAbility();
            //allyEventHandler.CallEventToggleIsUsingAbility(false);
        }

        //protected virtual Opsive.UltimateCharacterController.Character.Abilities.Ability GetTPCAbility()
        //{
        //    //Override To Get the Actual TPC Ability
        //    return null;
        //}

        public override bool CanUseAbility()
        {
            return true;
        }

        protected override void OnAllyDeath(Vector3 position, Vector3 force, GameObject attacker)
        {
            base.OnAllyDeath(position, force, attacker);
            //Stop The Ability When Ally Dies
            StopAbilityAnimation();
            //if (TPCAbility != null && TPCAbility.IsActive)
            //{
            //    StopAbilityAnimation();
            //}
        }
    }
}