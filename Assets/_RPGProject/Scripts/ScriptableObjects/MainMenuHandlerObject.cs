using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTSCoreFramework;
using BaseFramework;

namespace RPGPrototype
{
    [CreateAssetMenu(menuName = "RTSPrototype/MainMenu/RPGMainMenuHandlerSetupData")]
    public class MainMenuHandlerObject : ScriptableObject
    {
        [Header("Main Menu Handler Level Loading Selection Data.")]
        [SerializeField]
        public LevelIndex loadLevel;
        [SerializeField]
        public ScenarioIndex scenario;
    }
}