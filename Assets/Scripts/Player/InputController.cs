using System;
using Player;
using Tools;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerH
{
    public class InputController : MonoBehaviour
    {
        #region Singleton declaration

        public static InputController Instance;

        private void Awake()
        {
            if(Instance != null && Instance != this) Destroy(gameObject);

            Instance = this;
        }

        #endregion
        
        #region External components

        [SerializeField] private NavigationController playerNav;

        #endregion
        
        private PlayerAction _action;
        
        
        
        private void OnEnable()
        {
            if (_action is null)
            {
                _action = new PlayerAction();
                _action.Player.Move.performed += OnMove;
            }
            //_action.Enable();
        }

        private void OnDisable()
        {
            _action.Disable();
        }
        
        private void OnMove(InputAction.CallbackContext obj)
        {
            var dirVector = obj.ReadValue<Vector2>();
            
            //Tell player nav to move and adjust the input to constraint movement on x and z axis only 
            playerNav.TryToMove(dirVector.sqrMagnitude > 1f ? Vector2.zero : dirVector);
            
        }


        #region Enable/Disable input

        private void EnableMovement(bool b)
        {
            if(b) _action.Player.Enable();
            else _action.Player.Disable();
        }

        private void EnableCardSelection(bool b)
        {
            if(b) _action.Cards.Enable();
            else _action.Cards.Disable();
        }

        public void SwitchInputToMovement(bool b)
        {
            Debug.Log(b ? "Switching to movement" : "Switching to card selection");
            EnableMovement(b);
            EnableCardSelection(!b);
        }

        #endregion
        
        
    }
}
