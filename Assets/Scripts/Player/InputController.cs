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
        [SerializeField] private HandScript handScript;

        #endregion

        private PlayerAction _action;
        
        
        
        private void OnEnable()
        {
            if (_action is null)
            {
                _action = new PlayerAction();
                _action.Player.Move.performed += OnMove;
                _action.Player.Card0.performed += OnCard0Button;
                _action.Player.Card1.performed += OnCard1Button;
                _action.Player.Card2.performed += OnCard2Button;
                _action.Player.Card3.performed += OnCard3Button;
                _action.Player.Card4.performed += OnCard4Button;

                _action.Player.SpawnNewCard.performed += SpawnNewCards;

                _action.Cards.Card0.performed += OnCard0Button;
                _action.Cards.Card1.performed += OnCard0Button;
                _action.Cards.Card2.performed += OnCard0Button;
                _action.Cards.Card3.performed += OnCard0Button;
                _action.Cards.Card4.performed += OnCard0Button;
            }
            _action.Enable();
            _action.Cards.Disable();
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

        private void OnCard0Button(InputAction.CallbackContext obj)
        {
            handScript.Play_Card_Of_Index(0);
        }

        private void OnCard1Button(InputAction.CallbackContext obj)
        {
            handScript.Play_Card_Of_Index(1);
        }

        private void OnCard2Button(InputAction.CallbackContext obj)
        {
            handScript.Play_Card_Of_Index(2);
        }

        private void OnCard3Button(InputAction.CallbackContext obj)
        {
            handScript.Play_Card_Of_Index(3);
        }

        private void OnCard4Button(InputAction.CallbackContext obj)
        {
            handScript.Play_Card_Of_Index(4);
        }

        private void SpawnNewCards(InputAction.CallbackContext obj)
        {
            handScript.Fill_Hand();
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
