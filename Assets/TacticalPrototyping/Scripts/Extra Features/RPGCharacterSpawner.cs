using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using RTSCoreFramework;
using BaseFramework;

namespace RPGPrototype { 
    public class RPGCharacterSpawner : CharacterSpawner
    {
        #region CharacterSetupFields
        //Character Setup Fields
        [Header("Character Setup Fields")]
        [Header("Ally Instance Setup Fields")]
        [SerializeField]
        protected RTSAllyComponentSpecificFields AllySpecificComponentsToSetUp;

        [Header("All Allies Setup Fields")]
        [SerializeField]
        protected RPGAllyComponentSetupObject AllAllyComponentFieldsObject;

        protected RTSAllyComponentsAllCharacterFields AllAllyComponentFields
        {
            get { return AllAllyComponentFieldsObject.AllyComponentSetupFields; }
        }
        #endregion

        #region Properties
        RPGGameMode gamemode => RPGGameMode.thisInstance;
        RPGGameMaster gamemaster => RPGGameMaster.thisInstance;
        #endregion

        #region CharacterBuilder_BuildCharacter
        protected override IEnumerator CharacterBuilder_BuildCharacter()
        {
            yield return new WaitForSeconds(0.05f);
        }
        #endregion

        #region CharacterSetup_SetupCharacter
        protected override IEnumerator CharacterSetup_SetupCharacter()
        {
            yield return new WaitForSeconds(0f);
        }
        #endregion

        #region ItemBuilder_BuildItem
        protected override IEnumerator ItemBuilder_BuildItem()
        {
            yield return new WaitForSeconds(0f);
        }
        #endregion

        #region CharacterBuilder_UpdateCharacter
        protected override IEnumerator CharacterBuilder_UpdateCharacter()
        {
            yield return new WaitForSeconds(0f);
        }
        #endregion

        #region CharacterSetup_UpdateCharacterSetup
        protected override IEnumerator CharacterSetup_UpdateCharacterSetup()
        {
            spawnedGameObject.layer = gamemode.SingleAllyLayer;
            spawnedGameObject.tag = gamemode.AllyTag;

            // Wait For 0.05 Seconds
            yield return new WaitForSeconds(0.05f);

            //Delay Adding These Components

            //Call Ally Init Comps Event
            var _eventHandler = spawnedGameObject.GetComponent<AllyEventHandler>();
            _eventHandler.CallInitializeAllyComponents(AllySpecificComponentsToSetUp, AllAllyComponentFields);
        }
        #endregion

        #region Helpers
        void TryRetrievingExistingInitData()
        {

        }
        #endregion
    }
}