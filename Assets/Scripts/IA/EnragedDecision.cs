using Generation;
using Player;
using UnityEngine;

namespace IA
{
    public class EnragedDecision : DecisionMaker
    {
        public EnragedDecision(OtherNavBehavior nav, Brain brain, int moveAmount, Transform transform) : base(nav, brain, moveAmount, transform)
        {
        }

        private bool IsAround(int range)
        {
            var around = false;
            var pos = Tf.localPosition;
            var x = (int) pos.x;
            var y = (int) pos.z;
            for (var i = -range; i < range + 1; i++)
            {
                for (var j = -range; j < range + 1; j++)
                {
                    if (!GenerationMap.IsInMap(x + i, y + j)) continue;
                    around = around || GenerationMap.Map[x+i, y+j];
                }
            }

            return around;
        }

        public override void Decide()
        {
            //Check if the player is around 
            //Player detection.
            
            
            if (IsAround(2))
            {
                Stats_Perso.Instance.down_santee(20);
                AudioManager.instance.GetDmgSound();
                return;
            }
            
            

            //move random
            var pos = Tf.position;
            for(var i=0; i<MoveAmount; i++)
            {
                var surroundings = Brain.GetAvailableSurrounding(pos);
                if(surroundings.Count == 0)
                {
                    return;
                }
                var rand = Random.Range(0, (int)surroundings.Count);
                Nav.PerformMove(surroundings[rand]);
            }
        }
        public override void Die()
        {
            Debug.Log("Je meurs Johnny.");
        }

        public override string GetId()
        {
            return "enraged";
        }
    }
}