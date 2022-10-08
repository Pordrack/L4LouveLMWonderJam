using System;
using Tools;
using UnityEngine;

namespace PlayerH
{
    public class NavigationController : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        private int _currentPos;
        private Vector3? _target;

        public GameObject plate;
        
        private const int H = 100;
        private const int W = 100;
        private Vector3[] _map;
        
        
        private Transform _tf; //for fewer c++ calls
        

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
            
        }

        public void TryToMove(int dir)
        {
            if (_target is not null) return;
            
            //Check if the movement is possible
            if (IsMoveLegal(dir))
            {
                _currentPos += dir; //Update pos index
                _target = _map[_currentPos]; //Update target
                
            }
            else
            {
                
            }
        }

        private bool IsMoveLegal(int deltaDir)
        {
            //Check if the movement is out of the board
            var index = _currentPos + deltaDir;
            if (index < 0 || index >= H * W) return false;
            if(index% W == 0 && deltaDir == -1) return false;
            if(index% W == W - 1 && deltaDir == 1) return false;
            
            //Check if there is a collision on the block
            //TODO : map[index].type == 0 means free block we can go to.
            return true;
        }
        
    }
}
