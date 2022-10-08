using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI
{
    public class CustomText : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private Animator _animator;

        [SerializeField] private UnityEvent onClick;
        
        
        void Start()
        {
            _animator = GetComponent<Animator>();
            OnAnimationEnd.OnAnimationEndEvent += AnimationEnded;
        }

        private void AnimationEnded(Animator obj)
        {
            if (!obj.Equals(_animator)) return;
            onClick?.Invoke();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;
            _animator.SetTrigger("Selected");
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //Change to the focus mode
            _animator.SetBool("Focus", true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _animator.SetBool("Focus", false);        }
    }
}
