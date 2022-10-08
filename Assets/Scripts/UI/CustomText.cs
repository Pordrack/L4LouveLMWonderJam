using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class CustomText : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private Animator _animator;
        
        void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _animator.SetTrigger("Click");
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
