using System;
using UnityEngine;

namespace IA
{
    public class OtherNavBehavior : MonoBehaviour
    {

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
                y = 1.5f,
                z = y
            };
        }
        private void Update()
        {
            if (_target is null) return;
            transform.position = Vector3.MoveTowards(transform.position, _target.Value, 0.1f);
            if (transform.position == _target.Value) _target = null;
        }
    }

}

