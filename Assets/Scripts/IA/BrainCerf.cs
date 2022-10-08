using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace IA
{
    public class BrainCerf : Brain
    {
        public override void Decide()
        {
            var pos = Tf.position; //Get player position
            var surroundings = GetAvailableSurrounding(pos);
            
            var rand = Random.Range(0, surroundings.Count);
            Nav.PerformMove(surroundings[rand]);
        }

        private void Update()
        {
            ShallBeDrawn();
        }
    }
}
