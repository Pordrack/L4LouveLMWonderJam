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
            var player = EnemyManager.Singleton.GetPlayerPosition();
            var pos = Tf.position;
            var sqrDistance = Vector3.SqrMagnitude(player-pos);

            //if he is close enough, attack him
            if (sqrDistance < 4)
            {
                Stats_Perso.Instance.down_santee(20);
                AudioManager.instance.GetDmgSound();
                return;
            }

            //move random
            
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
            throw new System.NotImplementedException();
        }
    }
}