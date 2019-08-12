using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Characters;

namespace RPGPrototype
{
    public class RPGSpecialAbilities : MonoBehaviour
    {
        #region Properties
        RPGGameMaster gamemaster
        {
            get { return RPGGameMaster.thisInstance; }
        }

        AllyEventHandlerRPG eventhandler
        {
            get
            {
                if (_eventhandler == null)
                    _eventhandler = GetComponent<AllyEventHandlerRPG>();

                return _eventhandler;
            }
        }
        AllyEventHandlerRPG _eventhandler = null;
        AllyMemberRPG allymember
        {
            get
            {
                if (_allymember == null)
                    _allymember = GetComponent<AllyMemberRPG>();

                return _allymember;
            }
        }
        AllyMemberRPG _allymember = null;

        bool bIsDead
        {
            get
            {
                return allymember == null ||
                allymember.IsAlive == false;
            }
        }

        //Stamina
        int AllyStamina
        {
            get { return allymember.AllyStamina; }
        }
        int AllyMaxStamina
        {
            get { return allymember.AllyMaxStamina; }
        }
        float energyAsPercent { get { return AllyStamina / AllyMaxStamina; } }
        #endregion

        #region Fields
        [SerializeField] AbilityConfig[] abilities;
        [SerializeField] Image energyBar;
        //Once per second
        float addStaminaRepeatRate = 1f;
        int regenPointsPerSecond = 10;
        [SerializeField] AudioClip outOfEnergy;

        AudioSource audioSource;

        /// <summary>
        /// Allows me to store a behavior on this script
        /// instead of depending on the config for behavior reference
        /// </summary>
        Dictionary<AbilityConfig, AbilityBehaviour> AbilityDictionary = new Dictionary<AbilityConfig, AbilityBehaviour>();
        #endregion

        #region UnityMessages
        // Use this for initialization
        void Start()
        {
            audioSource = GetComponent<AudioSource>();

            InitializeAbilityDictionary();
            InvokeRepeating("SE_AddEnergyPoints", 1f, addStaminaRepeatRate);
            eventhandler.EventAllyDied += OnAllyDeath;
            gamemaster.OnNumberKeyPress += OnKeyPress;
        }

        private void OnDisable()
        {
            eventhandler.EventAllyDied -= OnAllyDeath;
            gamemaster.OnNumberKeyPress -= OnKeyPress;
        }
        #endregion

        #region AbilitiesAndEnergy
        public void AttemptSpecialAbility(int abilityIndex, GameObject target = null)
        {
            var energyComponent = GetComponent<RPGSpecialAbilities>();
            var energyCost = abilities[abilityIndex].GetEnergyCost();

            if (energyCost <= AllyStamina)
            {
                ConsumeEnergy(energyCost);
                AbilityDictionary[abilities[abilityIndex]].Use(target);
            }
            else
            {
                audioSource.PlayOneShot(outOfEnergy);
            }
        }

        public int GetNumberOfAbilities()
        {
            return abilities.Length;
        }

        public void ConsumeEnergy(float amount)
        {
            allymember.AllyDrainStamina((int)amount);
        }
        #endregion

        #region Services
        void SE_AddEnergyPoints()
        {
            allymember.AllyRegainStamina(regenPointsPerSecond);
        }
        #endregion

        #region Handlers
        void OnKeyPress(int _key)
        {
            if (bIsDead) return;

            if (_key == 0 ||
                _key >= GetNumberOfAbilities() ||
                allymember.bIsCurrentPlayer == false) return;

            AttemptSpecialAbility(_key);
        }

        void OnAllyDeath()
        {
            CancelInvoke();
        }
        #endregion

        #region DictionaryBehavior
        public AbilityBehaviour AddAbilityBehaviorFromConfig(AbilityConfig _config, GameObject objectToattachTo)
        {
            AbilityBehaviour _behaviourComponent = 
                _config.AddBehaviourComponent(objectToattachTo);
            _behaviourComponent.SetConfig(_config);
            return _behaviourComponent;
        }

        void InitializeAbilityDictionary()
        {
            for (int abilityIndex = 0; abilityIndex < abilities.Length; abilityIndex++)
            {
                AbilityDictionary.Add(
                    abilities[abilityIndex],
                    AddAbilityBehaviorFromConfig(
                        abilities[abilityIndex], this.gameObject
                    ));
            }
        }
        #endregion
    }
}