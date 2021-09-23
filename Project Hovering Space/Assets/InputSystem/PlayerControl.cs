// GENERATED AUTOMATICALLY FROM 'Assets/InputSystem/PlayerControl.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControl : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControl"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""a7928f21-e391-43ea-8a40-d7df8fa2a889"",
            ""actions"": [
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""e9bb0849-3453-475c-b83b-f6038d375ab3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""0159996f-7c59-4ffc-98f8-54731e60c446"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ScopeAim"",
                    ""type"": ""Button"",
                    ""id"": ""c4e700a0-1404-4b50-8531-6a7b9065f342"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""60b2b8a8-4d72-4e46-8ccb-2efb9d9db3b0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Holster"",
                    ""type"": ""Button"",
                    ""id"": ""01587090-5ae8-4c33-b1f7-d87118767d96"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""WeaponWheel"",
                    ""type"": ""PassThrough"",
                    ""id"": ""6becd304-b107-41cd-8fe8-e6a40473ec26"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Primary"",
                    ""type"": ""Button"",
                    ""id"": ""82141bc2-b285-40ca-8078-51c4d2cba572"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Secondary"",
                    ""type"": ""Button"",
                    ""id"": ""36bf5ed1-91e6-4864-89c5-d4df0939c41a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Equipment1"",
                    ""type"": ""Button"",
                    ""id"": ""d227ef73-f99a-444f-b380-522dcfbf0668"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Equipment2"",
                    ""type"": ""Button"",
                    ""id"": ""1fa3cea1-9565-4b3a-ae21-8619946e2b1c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""95d28ae5-1aea-4d72-a4a1-0694dcb12381"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RopeDart"",
                    ""type"": ""Button"",
                    ""id"": ""a4e454fe-d09e-419e-9ab9-7210de3207a8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interaction"",
                    ""type"": ""Button"",
                    ""id"": ""13272774-b3f2-4190-b353-f299bb744500"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4c4ceda2-cca2-436d-8fea-d7fc491f05db"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""64199772-4adb-49e1-b1d0-f271d11cd90d"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""831b1391-7009-4a94-82b3-d842a21a6fdb"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Hold(duration=0.2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""ScopeAim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3cb46ac3-6edd-44dd-9b23-b3b0b1d38dea"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4b6e7682-788f-473b-a245-f48f0a15675a"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Holster"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f4c43e5e-52b0-446a-8ac4-a192e5f929f3"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""WeaponWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""45e375a8-bd4f-4faa-8e97-542e07d2bde7"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Primary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fa219c55-2d77-455d-ab5e-e7df690879bb"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Secondary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e904e453-98b1-42ea-b9b0-a5b912866b09"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Equipment1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2b8bdb07-c8e4-4563-9018-08f8d3f847be"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Equipment2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""3d8319ab-2bbb-4255-9c2b-e7e369283cd6"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a6c56b65-e181-411f-b609-0f1415746817"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""cfd43134-65a3-4acb-9480-e7547c31ab67"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f2de034b-15ad-4910-91fb-75be9c4306dd"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4505c36e-379e-4e50-be16-8b9b62e8d778"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""63896137-d717-4b54-a0de-a8f4832359f0"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""RopeDart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7ebdc532-7e9b-4129-8422-16c449cc734f"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and Mouse"",
            ""bindingGroup"": ""Keyboard and Mouse"",
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
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Shoot = m_Player.FindAction("Shoot", throwIfNotFound: true);
        m_Player_Aim = m_Player.FindAction("Aim", throwIfNotFound: true);
        m_Player_ScopeAim = m_Player.FindAction("ScopeAim", throwIfNotFound: true);
        m_Player_Reload = m_Player.FindAction("Reload", throwIfNotFound: true);
        m_Player_Holster = m_Player.FindAction("Holster", throwIfNotFound: true);
        m_Player_WeaponWheel = m_Player.FindAction("WeaponWheel", throwIfNotFound: true);
        m_Player_Primary = m_Player.FindAction("Primary", throwIfNotFound: true);
        m_Player_Secondary = m_Player.FindAction("Secondary", throwIfNotFound: true);
        m_Player_Equipment1 = m_Player.FindAction("Equipment1", throwIfNotFound: true);
        m_Player_Equipment2 = m_Player.FindAction("Equipment2", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_RopeDart = m_Player.FindAction("RopeDart", throwIfNotFound: true);
        m_Player_Interaction = m_Player.FindAction("Interaction", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Shoot;
    private readonly InputAction m_Player_Aim;
    private readonly InputAction m_Player_ScopeAim;
    private readonly InputAction m_Player_Reload;
    private readonly InputAction m_Player_Holster;
    private readonly InputAction m_Player_WeaponWheel;
    private readonly InputAction m_Player_Primary;
    private readonly InputAction m_Player_Secondary;
    private readonly InputAction m_Player_Equipment1;
    private readonly InputAction m_Player_Equipment2;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_RopeDart;
    private readonly InputAction m_Player_Interaction;
    public struct PlayerActions
    {
        private @PlayerControl m_Wrapper;
        public PlayerActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Shoot => m_Wrapper.m_Player_Shoot;
        public InputAction @Aim => m_Wrapper.m_Player_Aim;
        public InputAction @ScopeAim => m_Wrapper.m_Player_ScopeAim;
        public InputAction @Reload => m_Wrapper.m_Player_Reload;
        public InputAction @Holster => m_Wrapper.m_Player_Holster;
        public InputAction @WeaponWheel => m_Wrapper.m_Player_WeaponWheel;
        public InputAction @Primary => m_Wrapper.m_Player_Primary;
        public InputAction @Secondary => m_Wrapper.m_Player_Secondary;
        public InputAction @Equipment1 => m_Wrapper.m_Player_Equipment1;
        public InputAction @Equipment2 => m_Wrapper.m_Player_Equipment2;
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @RopeDart => m_Wrapper.m_Player_RopeDart;
        public InputAction @Interaction => m_Wrapper.m_Player_Interaction;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Shoot.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                @Aim.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @ScopeAim.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnScopeAim;
                @ScopeAim.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnScopeAim;
                @ScopeAim.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnScopeAim;
                @Reload.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @Holster.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHolster;
                @Holster.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHolster;
                @Holster.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHolster;
                @WeaponWheel.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWeaponWheel;
                @WeaponWheel.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWeaponWheel;
                @WeaponWheel.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWeaponWheel;
                @Primary.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrimary;
                @Primary.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrimary;
                @Primary.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrimary;
                @Secondary.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSecondary;
                @Secondary.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSecondary;
                @Secondary.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSecondary;
                @Equipment1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEquipment1;
                @Equipment1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEquipment1;
                @Equipment1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEquipment1;
                @Equipment2.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEquipment2;
                @Equipment2.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEquipment2;
                @Equipment2.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEquipment2;
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @RopeDart.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRopeDart;
                @RopeDart.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRopeDart;
                @RopeDart.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRopeDart;
                @Interaction.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteraction;
                @Interaction.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteraction;
                @Interaction.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteraction;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @ScopeAim.started += instance.OnScopeAim;
                @ScopeAim.performed += instance.OnScopeAim;
                @ScopeAim.canceled += instance.OnScopeAim;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
                @Holster.started += instance.OnHolster;
                @Holster.performed += instance.OnHolster;
                @Holster.canceled += instance.OnHolster;
                @WeaponWheel.started += instance.OnWeaponWheel;
                @WeaponWheel.performed += instance.OnWeaponWheel;
                @WeaponWheel.canceled += instance.OnWeaponWheel;
                @Primary.started += instance.OnPrimary;
                @Primary.performed += instance.OnPrimary;
                @Primary.canceled += instance.OnPrimary;
                @Secondary.started += instance.OnSecondary;
                @Secondary.performed += instance.OnSecondary;
                @Secondary.canceled += instance.OnSecondary;
                @Equipment1.started += instance.OnEquipment1;
                @Equipment1.performed += instance.OnEquipment1;
                @Equipment1.canceled += instance.OnEquipment1;
                @Equipment2.started += instance.OnEquipment2;
                @Equipment2.performed += instance.OnEquipment2;
                @Equipment2.canceled += instance.OnEquipment2;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @RopeDart.started += instance.OnRopeDart;
                @RopeDart.performed += instance.OnRopeDart;
                @RopeDart.canceled += instance.OnRopeDart;
                @Interaction.started += instance.OnInteraction;
                @Interaction.performed += instance.OnInteraction;
                @Interaction.canceled += instance.OnInteraction;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_KeyboardandMouseSchemeIndex = -1;
    public InputControlScheme KeyboardandMouseScheme
    {
        get
        {
            if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
            return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnShoot(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnScopeAim(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnHolster(InputAction.CallbackContext context);
        void OnWeaponWheel(InputAction.CallbackContext context);
        void OnPrimary(InputAction.CallbackContext context);
        void OnSecondary(InputAction.CallbackContext context);
        void OnEquipment1(InputAction.CallbackContext context);
        void OnEquipment2(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnRopeDart(InputAction.CallbackContext context);
        void OnInteraction(InputAction.CallbackContext context);
    }
}
