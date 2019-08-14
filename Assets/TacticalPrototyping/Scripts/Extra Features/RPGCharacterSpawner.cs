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
        #region Properties
        RPGGameMode gamemode => RPGGameMode.thisInstance;
        RPGGameMaster gamemaster => RPGGameMaster.thisInstance;
        #endregion

        #region CharacterSetup_UpdateCharacterSetup
        protected override IEnumerator CharacterSetup_UpdateCharacterSetup()
        {

            // Wait For 0.05 Seconds
            yield return new WaitForSeconds(0.05f);

            //Delay Adding These Components
            
        }
        #endregion
    }
}