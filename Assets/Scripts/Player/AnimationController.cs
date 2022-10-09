using System;
using UnityEngine;

namespace Player
{
   
    public class AnimationController : MonoBehaviour
    {
        private int _upHash;
        private int _downHash;
        private int _leftHash;
        private int _rightHash;
        
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _upHash = Animator.StringToHash("Up");
            _downHash = Animator.StringToHash("Down");
            _leftHash = Animator.StringToHash("Left");
            _rightHash = Animator.StringToHash("Right");
        }

        public void MoveAnimation(AnimationMove dir)
        {
            switch (dir)
            {
                case AnimationMove.Up:
                    _animator.SetTrigger(_upHash);
                    break;
                case AnimationMove.Down:
                    _animator.SetTrigger(_downHash);
                    break;
                case AnimationMove.Left:
                    _animator.SetTrigger(_leftHash);
                    break;
                case AnimationMove.Right:
                    _animator.SetTrigger(_rightHash);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
            }
        }

        public enum AnimationMove
        {
            Up,
            Down,
            Left,
            Right
        }
    }
}