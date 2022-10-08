using System;
using Tools;
using UnityEngine;

namespace PlayerH
{
    public class NavigationController : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        private const int H = 100;
        private const int W = 100;
        
        
        private Transform _tf; //for fewer c++ calls
        
        private int _currentPos;
        private Vector3? _target;
        
        private Vector3[] _map;

        private void Awake()
        {
            _tf = transform;
            _map = new Vector3[H * W];
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    _map[i * W + j] = new Vector3(j, _tf.position.y, i);
                }
            }
        }

        private void Update()
        {
            if (_target is null) return;
            _tf.position = Vector3.MoveTowards(_tf.position, _target.Value, speed*Time.deltaTime);
            if (_tf.position == _target.Value)
            {
                _target = null;
            }
            //_tf.position += speed*Time.deltaTime*_target;
        }

        public void TryToMove(int dir)
        {
            if (_target is not null) return;
            
            //Check if the movement is possible
            if (IsMoveLegal())
            {
                _currentPos += dir; //Update pos index
                _target = _map[_currentPos]; //Update target
                
            }
            else
            {
                
            }
        }

        private bool IsMoveLegal()
        {
            return true;
        }
    }
}
