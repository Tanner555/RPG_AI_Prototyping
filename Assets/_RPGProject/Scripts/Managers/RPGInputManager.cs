using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseFramework;
using RTSCoreFramework;
using RPG.Characters;

namespace RPGPrototype
{
    public class RPGInputManager : InputManager
    {
        #region Fields
        //Handles Right Mouse Down Input
        [Header("Right Mouse Down Config")]
        public float RMHeldThreshold = 0.15f;
        protected bool isRMHeldDown = false;
        protected bool isRMHeldPastThreshold = false;
        protected float RMCurrentTimer = 5f;
        //Handles Left Mouse Down Input
        [Header("Left Mouse Down Config")]
        public float LMHeldThreshold = 0.15f;
        protected bool isLMHeldDown = false;
        protected bool isLMHeldPastThreshold = false;
        protected float LMCurrentTimer = 5f;
        //Handles Mouse ScrollWheel Input
        //Scroll Input
        protected string scrollInputName = "Mouse ScrollWheel";
        protected float scrollInputAxisValue = 0.0f;
        protected bool bScrollWasPreviouslyPositive = false;
        protected bool bScrollIsCurrentlyPositive = false;
        //Scroll Timer Handling
        protected bool isScrolling = false;
        //Used to Fix First Scroll Not Working Issue
        protected bool bBeganScrolling = false;
        //Stop Scroll Functionality
        [Header("Mouse ScrollWheel Config")]
        public float scrollStoppedThreshold = 0.15f;
        protected bool isNotScrollingPastThreshold = false;
        protected float noScrollCurrentTimer = 5f;
        //Number Key Input
        protected List<int> NumberKeys = new List<int>
        {
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9
        };
        protected List<string> NumberKeyNames = new List<string>
        {
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"
        };
        #endregion

        #region OverrideAndHideProperties
        new protected RTSUiMaster uiMaster { get { return RTSUiMaster.thisInstance; } }

        new protected RTSUiManager uiManager
        {
            get { return RTSUiManager.thisInstance; }
        }

        new protected RTSGameMode gamemode
        {
            get { return RTSGameMode.thisInstance; }
        }

        protected new RPGGameMaster gamemaster
        {
            get { return RPGGameMaster.thisInstance; }
        }
        #endregion

        #region Properties
        RTSCamRaycaster raycaster
        {
            get { return RTSCamRaycaster.thisInstance; }
        }

        //Mouse Setup - Scrolling
        protected bool bScrollAxisIsPositive
        {
            get { return scrollInputAxisValue >= 0.0f; }
        }
        #endregion

        #region UnityMessages

        #endregion

        #region InputSetup
        protected override void InputSetup()
        {
            base.InputSetup();

            if (Input.GetKeyDown(KeyCode.Escape))
                CallMenuToggle();
            if (Input.GetKeyDown(KeyCode.B))
                CallIGBPIToggle();
            if (Input.GetKeyDown(KeyCode.L))
                CallLuaEditorToggle();

            if (UiIsEnabled) return;
            //All Input That Shouldn't Happen When 
            //Ui is Enabled
            if (Input.GetKeyDown(KeyCode.Keypad1))
                CallPossessAllyAdd();
            if (Input.GetKeyDown(KeyCode.Keypad3))
                CallPossessAllySubtract();
            if (Input.GetKeyDown(KeyCode.C))
                CallCoverToggle();
            if (Input.GetKeyDown(KeyCode.R))
                CallTryReload();
            if (Input.GetKeyDown(KeyCode.Space))
                CallToggleIsInPauseControl();

            foreach (int _key in NumberKeys)
            {
                if (Input.GetKeyDown(_key.ToString()))
                {
                    CallOnNumberKeyPress(_key);
                }
            }
        }
        #endregion

        #region InputCalls
        void CallToggleIsInPauseControl() { gamemaster.CallOnTogglebIsInPauseControlMode(); }
        void CallIGBPIToggle() { uiMaster.CallEventIGBPIToggle(); }
        void CallLuaEditorToggle() { uiMaster.CallEventLuaEditorToggle(); }
        void CallPossessAllyAdd() { gamemode.GeneralInCommand.PossessAllyAdd(); }
        void CallPossessAllySubtract() { gamemode.GeneralInCommand.PossessAllySubtract(); }
        void CallTryReload() { gamemode.GeneralInCommand.AllyInCommand.allyEventHandler.CallOnTryReload(); }
        void CallCoverToggle() { gamemode.GeneralInCommand.AllyInCommand.allyEventHandler.CallOnTryCrouch(); }

        #endregion

        #region MouseSetup
        void LeftMouseDownSetup()
        {
            if (UiIsEnabled) return;
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (isRMHeldDown) return;
                if (isLMHeldDown == false)
                {
                    isLMHeldDown = true;
                    LMCurrentTimer = CurrentGameTime + LMHeldThreshold;
                }

                if (CurrentGameTime > LMCurrentTimer)
                {
                    //Calls Every Update
                    //CreateSelectionSquare();
                    if (isLMHeldPastThreshold == false)
                    {
                        //OnMouseDown Code Goes Here
                        isLMHeldPastThreshold = true;
                        gamemaster.CallEventHoldingLeftMouseDown(true);
                    }
                }
            }
            else
            {
                if (isLMHeldDown == true)
                {
                    isLMHeldDown = false;
                    if (isLMHeldPastThreshold == true)
                    {
                        //When MouseDown Code Exits
                        isLMHeldPastThreshold = false;
                        gamemaster.CallEventHoldingLeftMouseDown(false);
                    }
                    else
                    {
                        //Mouse Button Was Let Go Before the Threshold
                        //Call the Click Event
                        gamemaster.CallEventOnLeftClick();
                    }
                }
            }
        }

        void RightMouseDownSetup()
        {
            if (UiIsEnabled) return;
            if (Input.GetKey(KeyCode.Mouse1))
            {
                if (isLMHeldDown) return;
                if (isRMHeldDown == false)
                {
                    isRMHeldDown = true;
                    RMCurrentTimer = CurrentGameTime + RMHeldThreshold;
                }

                if (CurrentGameTime > RMCurrentTimer)
                {
                    if (isRMHeldPastThreshold == false)
                    {
                        //OnMouseDown Code Goes Here
                        isRMHeldPastThreshold = true;
                        gamemaster.CallEventHoldingRightMouseDown(true);
                    }
                }
            }
            else
            {
                if (isRMHeldDown == true)
                {
                    isRMHeldDown = false;
                    if (isRMHeldPastThreshold == true)
                    {
                        //When MouseDown Code Exits
                        isRMHeldPastThreshold = false;
                        gamemaster.CallEventHoldingRightMouseDown(false);
                    }
                    else
                    {
                        //Mouse Button Was Let Go Before the Threshold
                        //Call the Click Event
                        gamemaster.CallEventOnRightClick();
                    }
                }
            }

        }

        void StopMouseScrollWheelSetup()
        {
            if (UiIsEnabled) return;
            scrollInputAxisValue = Input.GetAxis(scrollInputName);
            if (Mathf.Abs(scrollInputAxisValue) > 0.0f)
            {
                if (isLMHeldDown) return;
                bScrollIsCurrentlyPositive = bScrollAxisIsPositive;

                //Fixes First Scroll Not Working Issue
                if (bBeganScrolling == false)
                {
                    bBeganScrolling = true;
                    gamemaster.CallEventEnableCameraZoom(true, bScrollAxisIsPositive);
                }

                if (bScrollWasPreviouslyPositive != bScrollIsCurrentlyPositive)
                {
                    gamemaster.CallEventEnableCameraZoom(true, bScrollAxisIsPositive);
                    bScrollWasPreviouslyPositive = bScrollAxisIsPositive;
                }

                if (isScrolling == false)
                {
                    isScrolling = true;
                    if (isNotScrollingPastThreshold == true)
                    {
                        //When ScrollWheel Code Starts
                        isNotScrollingPastThreshold = false;
                        gamemaster.CallEventEnableCameraZoom(true, bScrollAxisIsPositive);
                        bScrollWasPreviouslyPositive = bScrollAxisIsPositive;
                    }
                    else
                    {
                        //Scroll Wheel Started Before the Stop Threshold
                        //Do nothing for now
                    }
                }
            }
            else
            {
                if (isScrolling == true)
                {
                    isScrolling = false;
                    noScrollCurrentTimer = CurrentGameTime + scrollStoppedThreshold;
                }

                if (CurrentGameTime > noScrollCurrentTimer)
                {
                    if (isNotScrollingPastThreshold == false)
                    {
                        //OnScrollWheel Stopping Code Goes Here
                        isNotScrollingPastThreshold = true;
                        gamemaster.CallEventEnableCameraZoom(false, bScrollAxisIsPositive);
                    }
                }
            }
        }

        #endregion

        #region Handlers
        protected override void OnUpdateHandler()
        {
            base.OnUpdateHandler();
            LeftMouseDownSetup();
            RightMouseDownSetup();
            StopMouseScrollWheelSetup();
        }

        protected override void HandleGamePaused(bool _isPaused)
        {
            base.HandleGamePaused(_isPaused);
            if (_isPaused)
            {
                ResetMouseSetup();
            }
        }

        protected override void HandleUiActiveSelf(bool _state)
        {
            base.HandleUiActiveSelf(_state);
            if (_state == true)
            {
                ResetMouseSetup();
            }
        }

        protected override void HandleUiActiveSelf()
        {
            base.HandleUiActiveSelf();
            if (uiMaster.isUiAlreadyInUse)
            {
                ResetMouseSetup();
            }
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Used Whenever Mouse Setup Needs to be disabled,
        /// Such as when a UI Menu (Pause Menu) is Active
        /// </summary>
        void ResetMouseSetup()
        {
            if (isRMHeldPastThreshold)
            {
                isRMHeldPastThreshold = false;
                gamemaster.CallEventHoldingRightMouseDown(false);
            }
            if (isLMHeldPastThreshold)
            {
                isLMHeldPastThreshold = false;
                gamemaster.CallEventHoldingLeftMouseDown(false);
            }

            isLMHeldDown = false;
            isRMHeldDown = false;
            //Reset Scrolling
            isScrolling = false;
            isNotScrollingPastThreshold = true;
            bBeganScrolling = false;
            noScrollCurrentTimer = 0.0f;
            gamemaster.CallEventEnableCameraZoom(false, bScrollAxisIsPositive);
        }
        #endregion

        #region CommentedCode
        //void CallInventoryToggle() { uiMaster.CallEventInventoryUIToggle(); }
        //void CallSelectPrevWeapon() { gamemode.GeneralInCommand.AllyInCommand.allyEventHandler.CallOnSwitchToPrevItem(); }
        //void CallSelectNextWeapon() { gamemode.GeneralInCommand.AllyInCommand.allyEventHandler.CallOnSwitchToNextItem(); }
        //void CallTryFire() { gamemode.GeneralInCommand.AllyInCommand.allyEventHandler.CallOnTryUseWeapon(); }
        //void CallSprintToggle() { gamemode.GeneralInCommand.AllyInCommand.allyEventHandler.CallEventToggleIsSprinting(); }

        ////Handles Multi Unit Selection
        //[Header("Selection Config")]
        //[SerializeField]
        //private RectTransform SelectionImage;
        //Vector3 selectionStartPos;
        //Vector3 selectionEndPos;
        //Sprinting Setup
        //private bool isSprinting = false;
        //private AllyMember setupSprintAlly = null;

        //private AllyMoveSpeed setupMoveSpeed;

        //void SelectionInitialize()
        //{
        //    if (SelectionImage == null) return;
        //    selectionStartPos = Input.mousePosition;
        //    //RaycastHit _hit;
        //    //if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit, Mathf.Infinity))
        //    //{
        //    //    selectionStartPos = _hit.point;
        //    //}

        //}

        //void CreateSelectionSquare()
        //{
        //    //YouTube Vids
        //    //Unity Tutorial - RTS Controls - Selection Box GUI - Part 4 
        //    //https://youtu.be/vsdIhyLKgjc
        //    //Unity Tutorial - RTS Controls - Selection Box Function - Part 5 
        //    //https://youtu.be/ceMyupol6AQ

        //    if (SelectionImage == null) return;

        //    if (!SelectionImage.gameObject.activeSelf)
        //        SelectionImage.gameObject.SetActive(true);

        //    selectionEndPos = Input.mousePosition;
        //    selectionEndPos.z = 0f;
        //    Vector3 _squareStart = selectionStartPos;
        //    //Vector3 _squareStart = Camera.main.WorldToScreenPoint(selectionStartPos);
        //    _squareStart.z = 0f;
        //    Vector3 _center = (_squareStart + selectionEndPos) / 2f;

        //    SelectionImage.position = _center;

        //    float _sizeX = Mathf.Abs(_squareStart.x - selectionEndPos.x);
        //    float _sizeY = Mathf.Abs(_squareStart.y - selectionEndPos.y);
        //    SelectionImage.sizeDelta = new Vector2(_sizeX, _sizeY);

        //}

        //void StopSelectionSquare()
        //{
        //    if (SelectionImage == null) return;

        //    if (SelectionImage.gameObject.activeSelf)
        //        SelectionImage.gameObject.SetActive(false);
        //}
        #endregion

    }
}