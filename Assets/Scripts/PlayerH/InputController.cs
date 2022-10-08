using Tools;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerH
{
    public class InputController : MonoBehaviour
    {
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
            _action.Enable();
        }

        private void OnDisable()
        {
            _action.Disable();
        }
        
        private void OnMove(InputAction.CallbackContext obj)
        {
            var dirVector = obj.ReadValue<Vector2>();
            playerNav.TryToMove(MiscTools.ConvertVector2ToIndex(dirVector, 100));
            
        }
        
        
    }
}
