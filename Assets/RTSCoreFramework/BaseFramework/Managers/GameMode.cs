using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework
{
    public class GameMode : MonoBehaviour
    {
        #region Properties
        protected virtual int RefreshRate
        {
            get
            {
                if (_refreshRate == -1)
                    _refreshRate = Screen.currentResolution.refreshRate;

                return _refreshRate;
            }
        }
        private int _refreshRate = -1;

        protected UiMaster uiMaster
        {
            get { return UiMaster.thisInstance; }
        }

        protected UiManager uiManager
        {
            get { return UiManager.thisInstance; }
        }

        protected GameInstance gameInstance
        {
            get { return GameInstance.thisInstance; }
        }

        public GameMaster gamemaster
        {
            get { return GameMaster.thisInstance; }
        }

        protected UnityMsgManager myUnityMsgManager
        {
            get { return UnityMsgManager.thisInstance;}
        }

        public static GameMode thisInstance
        {
            get; protected set;
        }
        #endregion

        #region UnityMessages
        protected virtual void OnEnable()
        {
            if (thisInstance != null)
                Debug.LogWarning("More than one instance of GameMode in scene.");
            else
                thisInstance = this;

            ResetGameModeStats();
            InitializeGameModeValues();
            SubscribeToEvents();
            UpdateFrameRateLimit();
        }

        protected virtual void Start()
        {
            StartServices();

            //if (uiManager == null)
            //    Debug.LogWarning("There is no uimanager in the scene!");
        }

        protected virtual void OnDisable()
        {
            UnsubscribeFromEvents();
        }
        #endregion

        #region Handlers
        protected virtual void OnUpdateHandler()
        {
            UpdateFrameRateLimit();
        }
        #endregion

        #region Updaters and Resetters
        protected virtual void UpdateFrameRateLimit()
        {
            if (QualitySettings.vSyncCount != 0)
            {
                //Frame Limit Doesn't Work If VSync Is Set Above 0
                QualitySettings.vSyncCount = 0;
            }
            if (Application.targetFrameRate != RefreshRate)
            {
                Application.targetFrameRate = RefreshRate;
            }
        }

        protected virtual void UpdateGameModeStats()
        {

        }

        protected virtual void ResetGameModeStats()
        {

        }
        #endregion

        #region GameModeSetupFunctions
        protected virtual void InitializeGameModeValues()
        {

        }

        protected virtual void SubscribeToEvents()
        {
            myUnityMsgManager.RegisterOnUpdate(OnUpdateHandler);
        }

        protected virtual void UnsubscribeFromEvents()
        {
            myUnityMsgManager.DeregisterOnUpdate(OnUpdateHandler);
        }

        protected virtual void StartServices()
        {
            //InvokeRepeating("SE_UpdateTargetUI", 0.1f, 0.1f);
        }
        #endregion
    }
}