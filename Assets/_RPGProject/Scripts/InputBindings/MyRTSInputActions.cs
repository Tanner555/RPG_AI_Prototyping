// GENERATED AUTOMATICALLY FROM 'Assets/_RPGProject/SaveData/Input/MyRTSInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace RPGPrototype
{
    public class @MyRTSInputActions : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @MyRTSInputActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""MyRTSInputActions"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""db60a659-2bd8-4b69-8823-bff532b3d6e4"",
            ""actions"": [
                {
                    ""name"": ""PauseGame"",
                    ""type"": ""Button"",
                    ""id"": ""2a34802d-ea03-4ec9-9fa7-01992bb5c325"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""IGBPIMenuToggle"",
                    ""type"": ""Button"",
                    ""id"": ""adc8d5be-b527-4acb-84b2-2495c87345d5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""NumberKeys"",
                    ""type"": ""Button"",
                    ""id"": ""d6fea892-a85f-4aea-87b1-c90bb4868aac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PossessAllyAdd"",
                    ""type"": ""Button"",
                    ""id"": ""4150e910-4eda-4ca6-a76c-c97b325fea5e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PossessAllySubtract"",
                    ""type"": ""Button"",
                    ""id"": ""a5df70b5-04eb-4c2d-914c-a225fd48105e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TogglePauseControlMode"",
                    ""type"": ""Button"",
                    ""id"": ""833ca658-bfce-45a7-925a-1443728474fc"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""HorizontalMovement"",
                    ""type"": ""Button"",
                    ""id"": ""00965ca1-9614-43f2-87b0-7f38275368e5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ForwardMovement"",
                    ""type"": ""Button"",
                    ""id"": ""047acbd4-7838-4456-889a-7267989378dd"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ScrollCamera"",
                    ""type"": ""Value"",
                    ""id"": ""e9626001-86f4-4c25-912b-9de5c9217209"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": ""Clamp(min=-1,max=1)"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Left Mouse"",
                    ""type"": ""Button"",
                    ""id"": ""3488e3b8-a3bc-452b-bfac-529fac9e7641"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right Mouse"",
                    ""type"": ""Button"",
                    ""id"": ""63b5dd6a-6353-4dcd-bb6c-bf889d53408d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2450b551-4ef8-42d2-8828-15aefd456efb"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""PauseGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c89a0b2-ee6c-4785-b257-e8553890c5fb"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PauseGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d4db5585-4e21-4bf5-a275-b43e5f5e2300"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""IGBPIMenuToggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c37bf4a6-f48e-473a-9f92-9a245e4cb823"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""IGBPIMenuToggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""236b7cf9-d15c-4bda-a80a-ebf63abe131e"",
                    ""path"": ""<Keyboard>/0"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""NumberKeys"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a0c89df2-68fa-45ca-8659-76a4795dca8f"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""NumberKeys"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""10acb38d-dff4-48ae-81c2-6f4b6de18290"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""NumberKeys"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3ebd58bd-7e5a-4cd6-a12d-8170aa22f06e"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""NumberKeys"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""31433cfe-6622-4391-b4e8-19e68423d546"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""NumberKeys"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b85767bd-5bae-41e6-9391-1ed53e253eff"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""NumberKeys"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""085a2bba-eb45-488c-9c76-553f2342fbaa"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""NumberKeys"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b98a0018-9164-4194-aa9c-c47de5bcd878"",
                    ""path"": ""<Keyboard>/7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""NumberKeys"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cf0ba8e4-2c34-43a2-8818-b9a866e8c808"",
                    ""path"": ""<Keyboard>/8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""NumberKeys"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""23e2f3ae-48f0-4fa9-b004-13f3ad44c00a"",
                    ""path"": ""<Keyboard>/9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""NumberKeys"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8b70ab07-1757-4213-930f-98ba3bd55964"",
                    ""path"": ""<Keyboard>/numpad1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""PossessAllyAdd"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bb9822cb-017b-4e17-8018-1f0fe5b89591"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PossessAllyAdd"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7bb96a62-b86b-484c-bbb4-401cdc14bbce"",
                    ""path"": ""<Keyboard>/numpad3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""PossessAllySubtract"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a524583-80df-46b9-9437-2696bfcce016"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PossessAllySubtract"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cbfb0796-ad6d-45b3-a174-e1e4b87902fe"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""TogglePauseControlMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""23a18ce3-442a-4f6b-b11f-8f41db614b85"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""TogglePauseControlMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard Axis"",
                    ""id"": ""5ced975e-b7d9-406c-bc01-ea3112e985be"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""ce585024-0ca1-4d73-a283-181416654255"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""6fc323d9-db79-49d2-8573-f2efb2b9dbb1"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""6da2db5e-4ad5-4e35-88ce-b2eb7c9ae676"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""8fc2a773-6ddf-4cca-a63e-a1100ad67f7e"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""3357c543-8c88-4455-95ee-88ffc9d5f0b1"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Axis"",
                    ""id"": ""81e95546-301c-4345-9668-fecfc9eea6f2"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ForwardMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""462df493-fb51-48b9-8364-68dc2f5d1b04"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""ForwardMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e1e387a4-c1b4-4fa1-a6c3-5a4891e73f92"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""ForwardMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""1dfb83ea-d195-4246-8183-1d187f7a5bc6"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ForwardMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""fe68703f-3bf0-43cb-a9c5-05946e6ae0c9"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ForwardMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""fe87da0e-fc25-44ff-8470-f56db1f2761a"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ForwardMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d841ffc6-bcb4-467d-9316-774863092f06"",
                    ""path"": ""*/{ScrollVertical}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""ScrollCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""f66897d0-065d-4210-b385-efeff93a356e"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScrollCamera"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""7747ea4b-67b0-4351-bc5b-6b0173e8b6f9"",
                    ""path"": ""<Keyboard>/numpadMinus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""ScrollCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""666594c9-0186-40cf-8367-0ddd37a845b4"",
                    ""path"": ""<Keyboard>/numpadPlus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""ScrollCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""e417f0c3-2773-46ab-84e7-f874604f21bd"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScrollCamera"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""727c24a3-d97d-40ba-852a-7721651d5f44"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ScrollCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""b7dc7412-821c-468e-b8fd-6d71db507e00"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ScrollCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e5182f94-2cf8-455c-ba7e-8a751845fa80"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Left Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""deb29105-d8bf-4c4d-b08c-7dc031d4e14d"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard And Mouse"",
                    ""action"": ""Right Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard And Mouse"",
            ""bindingGroup"": ""Keyboard And Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Gameplay
            m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
            m_Gameplay_PauseGame = m_Gameplay.FindAction("PauseGame", throwIfNotFound: true);
            m_Gameplay_IGBPIMenuToggle = m_Gameplay.FindAction("IGBPIMenuToggle", throwIfNotFound: true);
            m_Gameplay_NumberKeys = m_Gameplay.FindAction("NumberKeys", throwIfNotFound: true);
            m_Gameplay_PossessAllyAdd = m_Gameplay.FindAction("PossessAllyAdd", throwIfNotFound: true);
            m_Gameplay_PossessAllySubtract = m_Gameplay.FindAction("PossessAllySubtract", throwIfNotFound: true);
            m_Gameplay_TogglePauseControlMode = m_Gameplay.FindAction("TogglePauseControlMode", throwIfNotFound: true);
            m_Gameplay_HorizontalMovement = m_Gameplay.FindAction("HorizontalMovement", throwIfNotFound: true);
            m_Gameplay_ForwardMovement = m_Gameplay.FindAction("ForwardMovement", throwIfNotFound: true);
            m_Gameplay_ScrollCamera = m_Gameplay.FindAction("ScrollCamera", throwIfNotFound: true);
            m_Gameplay_LeftMouse = m_Gameplay.FindAction("Left Mouse", throwIfNotFound: true);
            m_Gameplay_RightMouse = m_Gameplay.FindAction("Right Mouse", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // Gameplay
        private readonly InputActionMap m_Gameplay;
        private IGameplayActions m_GameplayActionsCallbackInterface;
        private readonly InputAction m_Gameplay_PauseGame;
        private readonly InputAction m_Gameplay_IGBPIMenuToggle;
        private readonly InputAction m_Gameplay_NumberKeys;
        private readonly InputAction m_Gameplay_PossessAllyAdd;
        private readonly InputAction m_Gameplay_PossessAllySubtract;
        private readonly InputAction m_Gameplay_TogglePauseControlMode;
        private readonly InputAction m_Gameplay_HorizontalMovement;
        private readonly InputAction m_Gameplay_ForwardMovement;
        private readonly InputAction m_Gameplay_ScrollCamera;
        private readonly InputAction m_Gameplay_LeftMouse;
        private readonly InputAction m_Gameplay_RightMouse;
        public struct GameplayActions
        {
            private @MyRTSInputActions m_Wrapper;
            public GameplayActions(@MyRTSInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @PauseGame => m_Wrapper.m_Gameplay_PauseGame;
            public InputAction @IGBPIMenuToggle => m_Wrapper.m_Gameplay_IGBPIMenuToggle;
            public InputAction @NumberKeys => m_Wrapper.m_Gameplay_NumberKeys;
            public InputAction @PossessAllyAdd => m_Wrapper.m_Gameplay_PossessAllyAdd;
            public InputAction @PossessAllySubtract => m_Wrapper.m_Gameplay_PossessAllySubtract;
            public InputAction @TogglePauseControlMode => m_Wrapper.m_Gameplay_TogglePauseControlMode;
            public InputAction @HorizontalMovement => m_Wrapper.m_Gameplay_HorizontalMovement;
            public InputAction @ForwardMovement => m_Wrapper.m_Gameplay_ForwardMovement;
            public InputAction @ScrollCamera => m_Wrapper.m_Gameplay_ScrollCamera;
            public InputAction @LeftMouse => m_Wrapper.m_Gameplay_LeftMouse;
            public InputAction @RightMouse => m_Wrapper.m_Gameplay_RightMouse;
            public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
            public void SetCallbacks(IGameplayActions instance)
            {
                if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
                {
                    @PauseGame.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPauseGame;
                    @PauseGame.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPauseGame;
                    @PauseGame.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPauseGame;
                    @IGBPIMenuToggle.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnIGBPIMenuToggle;
                    @IGBPIMenuToggle.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnIGBPIMenuToggle;
                    @IGBPIMenuToggle.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnIGBPIMenuToggle;
                    @NumberKeys.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNumberKeys;
                    @NumberKeys.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNumberKeys;
                    @NumberKeys.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNumberKeys;
                    @PossessAllyAdd.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPossessAllyAdd;
                    @PossessAllyAdd.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPossessAllyAdd;
                    @PossessAllyAdd.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPossessAllyAdd;
                    @PossessAllySubtract.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPossessAllySubtract;
                    @PossessAllySubtract.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPossessAllySubtract;
                    @PossessAllySubtract.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPossessAllySubtract;
                    @TogglePauseControlMode.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTogglePauseControlMode;
                    @TogglePauseControlMode.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTogglePauseControlMode;
                    @TogglePauseControlMode.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTogglePauseControlMode;
                    @HorizontalMovement.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnHorizontalMovement;
                    @HorizontalMovement.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnHorizontalMovement;
                    @HorizontalMovement.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnHorizontalMovement;
                    @ForwardMovement.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnForwardMovement;
                    @ForwardMovement.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnForwardMovement;
                    @ForwardMovement.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnForwardMovement;
                    @ScrollCamera.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnScrollCamera;
                    @ScrollCamera.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnScrollCamera;
                    @ScrollCamera.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnScrollCamera;
                    @LeftMouse.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftMouse;
                    @LeftMouse.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftMouse;
                    @LeftMouse.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftMouse;
                    @RightMouse.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightMouse;
                    @RightMouse.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightMouse;
                    @RightMouse.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightMouse;
                }
                m_Wrapper.m_GameplayActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @PauseGame.started += instance.OnPauseGame;
                    @PauseGame.performed += instance.OnPauseGame;
                    @PauseGame.canceled += instance.OnPauseGame;
                    @IGBPIMenuToggle.started += instance.OnIGBPIMenuToggle;
                    @IGBPIMenuToggle.performed += instance.OnIGBPIMenuToggle;
                    @IGBPIMenuToggle.canceled += instance.OnIGBPIMenuToggle;
                    @NumberKeys.started += instance.OnNumberKeys;
                    @NumberKeys.performed += instance.OnNumberKeys;
                    @NumberKeys.canceled += instance.OnNumberKeys;
                    @PossessAllyAdd.started += instance.OnPossessAllyAdd;
                    @PossessAllyAdd.performed += instance.OnPossessAllyAdd;
                    @PossessAllyAdd.canceled += instance.OnPossessAllyAdd;
                    @PossessAllySubtract.started += instance.OnPossessAllySubtract;
                    @PossessAllySubtract.performed += instance.OnPossessAllySubtract;
                    @PossessAllySubtract.canceled += instance.OnPossessAllySubtract;
                    @TogglePauseControlMode.started += instance.OnTogglePauseControlMode;
                    @TogglePauseControlMode.performed += instance.OnTogglePauseControlMode;
                    @TogglePauseControlMode.canceled += instance.OnTogglePauseControlMode;
                    @HorizontalMovement.started += instance.OnHorizontalMovement;
                    @HorizontalMovement.performed += instance.OnHorizontalMovement;
                    @HorizontalMovement.canceled += instance.OnHorizontalMovement;
                    @ForwardMovement.started += instance.OnForwardMovement;
                    @ForwardMovement.performed += instance.OnForwardMovement;
                    @ForwardMovement.canceled += instance.OnForwardMovement;
                    @ScrollCamera.started += instance.OnScrollCamera;
                    @ScrollCamera.performed += instance.OnScrollCamera;
                    @ScrollCamera.canceled += instance.OnScrollCamera;
                    @LeftMouse.started += instance.OnLeftMouse;
                    @LeftMouse.performed += instance.OnLeftMouse;
                    @LeftMouse.canceled += instance.OnLeftMouse;
                    @RightMouse.started += instance.OnRightMouse;
                    @RightMouse.performed += instance.OnRightMouse;
                    @RightMouse.canceled += instance.OnRightMouse;
                }
            }
        }
        public GameplayActions @Gameplay => new GameplayActions(this);
        private int m_KeyboardAndMouseSchemeIndex = -1;
        public InputControlScheme KeyboardAndMouseScheme
        {
            get
            {
                if (m_KeyboardAndMouseSchemeIndex == -1) m_KeyboardAndMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard And Mouse");
                return asset.controlSchemes[m_KeyboardAndMouseSchemeIndex];
            }
        }
        private int m_GamepadSchemeIndex = -1;
        public InputControlScheme GamepadScheme
        {
            get
            {
                if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
                return asset.controlSchemes[m_GamepadSchemeIndex];
            }
        }
        public interface IGameplayActions
        {
            void OnPauseGame(InputAction.CallbackContext context);
            void OnIGBPIMenuToggle(InputAction.CallbackContext context);
            void OnNumberKeys(InputAction.CallbackContext context);
            void OnPossessAllyAdd(InputAction.CallbackContext context);
            void OnPossessAllySubtract(InputAction.CallbackContext context);
            void OnTogglePauseControlMode(InputAction.CallbackContext context);
            void OnHorizontalMovement(InputAction.CallbackContext context);
            void OnForwardMovement(InputAction.CallbackContext context);
            void OnScrollCamera(InputAction.CallbackContext context);
            void OnLeftMouse(InputAction.CallbackContext context);
            void OnRightMouse(InputAction.CallbackContext context);
        }
    }
}
