using System;
using Generation;
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
                _action.Player.EndTurn.performed += End_Turn;

                _action.Player.SpawnNewCard.performed += SpawnNewCards;
                _action.Player.IncreaseRessources.performed += IncreaseRessource;
                _action.Player.DecreaseStats.performed += DecreaseStats;

                _action.Cards.Card0.performed += (ctx) => OnCardButton(0);
                _action.Cards.Card1.performed += (ctx) => OnCardButton(1);
                _action.Cards.Card2.performed += (ctx) => OnCardButton(2);
                _action.Cards.Card3.performed += (ctx) => OnCardButton(3);
                _action.Cards.Card4.performed += (ctx) => OnCardButton(4);
                _action.Cards.Next_Selection.performed += Next_Selection;
                _action.Cards.Previous_Selection.performed += Previous_Selection;
                _action.Cards.Play_Selected.performed += Play_Selected;
                _action.Cards.SwitchToNormalView.performed += Switch_To_Normal_View;
            }
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

        private void End_Turn(InputAction.CallbackContext obj)
        {
            
            GameManager.Instance.ChangeState(State.Decision_Turn);
        }

        private void OnCardButton(int index)
        {
            handScript.Play_Card_Of_Index(index);
        }

        private void SpawnNewCards(InputAction.CallbackContext obj)
        {
            handScript.Fill_Hand();
        }

        private void IncreaseRessource(InputAction.CallbackContext obj)
        {
            Ressources.Instance.add_bois(10);
            Ressources.Instance.add_nourriture(10);
            Ressources.Instance.add_pierre(10);
            Ressources.Instance.update_nourriture(Ressources.Instance._nourriture);
            Ressources.Instance.update_bois(Ressources.Instance._bois);
            Ressources.Instance.update_pierre(Ressources.Instance._pierre);
        }

        private void DecreaseStats(InputAction.CallbackContext obj)
        {
            //Stats_Perso.Instance.down_action(10);
            Stats_Perso.Instance.down_faim(10);
            Stats_Perso.Instance.down_santee(10);
        }


        private void GlitchHand(InputAction.CallbackContext obj)
        {
            handScript.Glitch_Hand(0.5f);
        }

        public void Next_Selection(InputAction.CallbackContext obj)
        {
            handScript.Move_Selected_Index(1);
        }

        public void Previous_Selection(InputAction.CallbackContext obj)
        {
            handScript.Move_Selected_Index(-1);
        }

        public void Play_Selected(InputAction.CallbackContext obj)
        {
            handScript.Play_Selected_Card();
            
        }

        //switch les controles et la camera vers la version "selection des cartes"
        private void Switch_To_Card_View(InputAction.CallbackContext obj)
        {
            camera_script.SwitchToCards();
            EnableMovement(false);
            EnableCardSelection(true);
            handScript.Show_Cursors();
        }

        //switch les controles et la camera vers la version "normal"
        private void Switch_To_Normal_View(InputAction.CallbackContext obj)
        {
            camera_script.SwitchFromCards();
            EnableCardSelection(false);
            EnableMovement(true);
            handScript.Hide_Cursors();
        }

        #region Enable/Disable input

        public void EnableMovement(bool b)
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
//            Debug.Log(b ? "Switching to movement" : "Switching to card selection");
            EnableMovement(b);
            EnableCardSelection(!b);
        }

        #endregion
        
        
    }
}
