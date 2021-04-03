using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework
{
    public class InputManager : BaseSingleton<InputManager>
    {
        #region Properties
        //Time Properties
        protected float CurrentGameTime
        {
            get { return Time.unscaledTime; }
        }
        
        protected UiMaster uiMaster { get { return UiMaster.thisInstance; } }

        protected UiManager uiManager
        {
            get { return UiManager.thisInstance; }
        }

        protected GameMode gamemode
        {
            get { return GameMode.thisInstance; }
        }

        protected GameMaster gamemaster
        {
            get { return GameMaster.thisInstance; }
        }

        protected UnityMsgManager myUnityMsgManager
        {
            get { return UnityMsgManager.thisInstance; }
        }
        #endregion

        #region Fields
        //UI is enabled
        protected bool UiIsEnabled = false;
        #endregion

        #region UnityMessages
        protected virtual void OnEnable()
        {

        }

        protected virtual void Start()
        {
            SubToEvents();
        }

        protected virtual void OnDisable()
        {
            UnsubFromEvents();
        }
        #endregion

        #region InputSetup
        protected virtual void InputSetup()
        {

        }

        #endregion
       
        #region Handlers
        protected virtual void OnUpdateHandler()
        {
            InputSetup();
        }

        protected virtual void HandleGamePaused(bool _isPaused)
        {

        }

        protected virtual void HandleUiActiveSelf(bool _state)
        {
            UiIsEnabled = _state;
        }

        protected virtual void HandleUiActiveSelf()
        {
            UiIsEnabled = uiMaster.isUiAlreadyInUse;
        }
        #endregion

        #region InputCalls
        protected void CallMenuToggle() { uiMaster.CallEventMenuToggle(); }
        protected void CallToggleIsGamePaused() { gamemaster.CallOnToggleIsGamePaused(); }
        protected void CallOnNumberKeyPress(int _index) { gamemaster.CallOnNumberKeyPress(_index); }
        #endregion

        #region Initialization
        protected virtual void SubToEvents()
        {
            gamemaster.OnToggleIsGamePaused += HandleGamePaused;
            uiMaster.EventAnyUIToggle += HandleUiActiveSelf;
            myUnityMsgManager.RegisterOnUpdate(OnUpdateHandler);
        }

        protected virtual void UnsubFromEvents()
        {
            gamemaster.OnToggleIsGamePaused -= HandleGamePaused;
            uiMaster.EventAnyUIToggle -= HandleUiActiveSelf;
            myUnityMsgManager.DeregisterOnUpdate(OnUpdateHandler);
        }
        #endregion
    }
}