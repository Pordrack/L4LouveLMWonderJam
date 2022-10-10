using Player;
using UnityEngine;

namespace IA
{
    public class EnragedDecision : DecisionMaker
    {
        public EnragedDecision(OtherNavBehavior nav, Brain brain, int moveAmount, Transform transform) : base(nav, brain, moveAmount, transform)
        {
        }

        public override void Decide()
        {
            //Check if the player is around 
            //Player detection.
            if (EnemyManager.Singleton.IsPlayerAround(2, Brain))
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