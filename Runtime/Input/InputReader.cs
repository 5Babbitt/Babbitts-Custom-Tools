using Babbitt.Tools.EventSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Babbitt.Tools.Input 
{
    [CreateAssetMenu(menuName = "Input Reader")]
    public class InputReader : ScriptableObject, GameInput.IGameplayActions, GameInput.IUIActions
    {
        private GameInput gameInput;

        [Header("Gameplay Events")]
        [Tooltip("Events for input actions that take place during gameplay, e.g. Player crouch, run, shoot, pause, interact, etc...")]
        public GameEvent MoveInputEvent;
        public GameEvent JumpPressedInputEvent;
        public GameEvent JumpReleasedInputEvent;
        public GameEvent PauseInputEvent;

        [Header("UI Events")]
        [Tooltip("Events for input actions that take place when in menu's, e.g Resume, Tabbing menu items, arrow keys to increase numbers, etc...")]
        public GameEvent ResumeInputEvent;

        // Only executes if there is a reference to the scriptable object in the scene
        private void OnEnable()
        {
            if (gameInput == null)
            {
                gameInput = new GameInput(); // A reference to the generated input class

                // Will recieve input events directly from the input class
                gameInput.Gameplay.SetCallbacks(this);
                gameInput.UI.SetCallbacks(this);

                // Enabling the default starting input action map
                SetStartingControlScheme();
            }
        }

        #region Action Maps
        public void SetStartingControlScheme()
        {
            SetUI();
        }

        public void SetGameplay()
        {
            gameInput.Gameplay.Enable();
            gameInput.UI.Disable();
        }

        public void SetUI()
        {
            gameInput.UI.Enable();
            gameInput.Gameplay.Disable();
        }
        #endregion

        #region Gameplay
        public void OnMove(InputAction.CallbackContext context)
        {
            MoveInputEvent.Raise(context.ReadValue<Vector2>());
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
                JumpPressedInputEvent.Raise();
            else if (context.canceled)
                JumpReleasedInputEvent.Raise();
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.performed)
                PauseInputEvent.Raise();
        }
        #endregion

        #region UI
        public void OnResume(InputAction.CallbackContext context)
        {
            if (context.performed)
                ResumeInputEvent.Raise();
        }
        #endregion
    }
}