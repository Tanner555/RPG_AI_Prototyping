using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RTSCoreFramework;
using BaseFramework;

namespace RPGPrototype
{
    public class RPGMainMenuHandler : MonoBehaviour
    {
        GameInstance gameInstance
        {
            get { return GameInstance.thisInstance; }
        }

        [SerializeField]
        public MainMenuHandlerObject MyMainMenuHandlerSetupData;

        public void Btn_PlayGame()
        {
            if(MyMainMenuHandlerSetupData == null)
            {
                Debug.LogWarning("Cannot Load Level Without Main Menu Handler Setup Data.");
                return;
            }

            var _loadLevel = MyMainMenuHandlerSetupData.loadLevel;
            var _scenario = MyMainMenuHandlerSetupData.scenario;

            if (gameInstance != null)
            {
                gameInstance.LoadLevel(_loadLevel, _scenario);
            }
        }

        public void Btn_QuitGame()
        {
            Application.Quit();
        }
    }
}