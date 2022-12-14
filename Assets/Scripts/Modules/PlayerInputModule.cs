using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities.Modules
{
    /// <summary>
    /// A module that handles the player inputs.
    /// </summary>
    public class PlayerInputModule : MonoBehaviour, IInputModule
    {
        #region IInputModule
        public Action<Vector2> OnAxisAction { get; set; }
        public Action<ButtonType, bool> OnButtonAction { get; set; }
        #endregion

        private InputActions inputActions;          // Unity's InputSystem input actions.

        /// <summary>
        /// Initializes the module with the passed ModuleHandler. Subscribes for all player inputs.
        /// </summary>
        /// <param name="handler">Entity handler.</param>
        public void Init(IModuleHandler handler)
        {
            inputActions = new InputActions();

            // Subscribes to inputs.
            inputActions.Player.Axis.performed += Axis_performed;
            inputActions.Player.Axis.canceled += Axis_performed;

            inputActions.Player.Action.performed += delegate { OnButtonAction?.Invoke(ButtonType.Action, true); };
            inputActions.Player.Action.canceled += delegate { OnButtonAction?.Invoke(ButtonType.Action, false); };
            inputActions.Player.Cancel.performed += delegate { OnButtonAction?.Invoke(ButtonType.Cancel, true); };
            inputActions.Player.Cancel.canceled += delegate { OnButtonAction?.Invoke(ButtonType.Cancel, false); };
            inputActions.Player.Pause.performed += delegate { OnButtonAction?.Invoke(ButtonType.Pause, true); };
            inputActions.Player.Pause.canceled += delegate { OnButtonAction?.Invoke(ButtonType.Pause, false); };

            // Enables inputs.
            inputActions.Player.Axis.Enable();
            inputActions.Player.Action.Enable();
            inputActions.Player.Cancel.Enable();
            inputActions.Player.Pause.Enable();
        }

        /// <summary>
        /// Sends an axis event.
        /// </summary>
        private void Axis_performed(InputAction.CallbackContext obj) => OnAxisAction?.Invoke(obj.ReadValue<Vector2>());

        /// <summary>
        /// Returns the game object to which the module is attached.
        /// </summary>
        /// <returns>Entity gameObject.</returns>
        public GameObject GetGameObject() => gameObject;
    }
}
