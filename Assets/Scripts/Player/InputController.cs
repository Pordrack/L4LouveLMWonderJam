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
        [SerializeField] private CameraScript camera_script;

        #endregion

        private PlayerAction _action;
        
        
        
        private void OnEnable()
        {
            if (_action is null)
            {
                _action = new PlayerAction();
                _action.Player.Move.performed += OnMove;
                _action.Player.Card0.performed += (ctx)=>OnCardButton(0);
                _action.Player.Card1.performed += (ctx) => OnCardButton(1);
                _action.Player.Card2.performed += (ctx) => OnCardButton(2);
                _action.Player.Card3.performed += (ctx) => OnCardButton(3);
                _action.Player.Card4.performed += (ctx) => OnCardButton(4);
                _action.Player.GlitchHand.performed += GlitchHand;
                _action.Player.SwitchToCardView.performed += Switch_To_Card_View;

                _action.Player.SpawnNewCard.performed += SpawnNewCards;

                _action.Cards.Card0.performed += (ctx) => OnCardButton(0);
                _action.Cards.Card1.performed += (ctx) => OnCardButton(1);
                _action.Cards.Card2.performed += (ctx) => OnCardButton(2);
                _action.Cards.Card3.performed += (ctx) => OnCardButton(3);
                _action.Cards.Card4.performed += (ctx) => OnCardButton(4);
                _action.Cards.SwitchToNormalView.performed += Switch_To_Normal_View;
            }

            Debug.LogWarning("_action.Enable(); est toujours lancé dans InputController.cs !");
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

        private void OnCardButton(int index)
        {
            handScript.Play_Card_Of_Index(index);
        }

        private void SpawnNewCards(InputAction.CallbackContext obj)
        {
            handScript.Fill_Hand();
        }

        private void GlitchHand(InputAction.CallbackContext obj)
        {
            handScript.Glitch_Hand(0.5f);
        }

        //switch les controles et la camera vers la version "selection des cartes"
        private void Switch_To_Card_View(InputAction.CallbackContext obj)
        {
            camera_script.SwitchToCards();
            EnableMovement(false);
            EnableCardSelection(true);
        }

        //switch les controles et la camera vers la version "normal"
        private void Switch_To_Normal_View(InputAction.CallbackContext obj)
        {
            camera_script.SwitchFromCards();
            EnableCardSelection(false);
            EnableMovement(true);
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
