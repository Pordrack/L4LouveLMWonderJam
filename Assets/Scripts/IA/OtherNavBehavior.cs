using System;
using Generation;
using UnityEngine;

namespace IA
{
    public class OtherNavBehavior : MonoBehaviour
    {
        private Brain _brain;

        private void Awake()
        {
            _brain = GetComponent<Brain>();
        }

        private Vector3? _target;
        public void PerformMove(int[] target)
        {
            if (target.Length != 2)
                throw new Exception("Should only contains two elements ! " +
                                    "(PerformMove in OtherNavBehavior).");
            var x = target[0];
            var y = target[1];
            
            _target = new Vector3()
            {
                x = x,
                y = transform.position.y,
                z = y
            };
            
            //updates the free blocks (usefull for correct teleportation) 
            GenerationMap.AddFreeBlock(new Vector2Int(x,y));
            var pos = transform.position;
            GenerationMap.RemoveFreeBlock(new Vector2Int((int) pos.x, (int) pos.y));
        }
        private void Update()
        {
            if (_target is null) return;
            transform.position = Vector3.MoveTowards(transform.position, _target.Value, 0.1f);
            _brain.ShallBeDrawn();
            if (transform.position == _target.Value) _target = null;
        }
    }

}

