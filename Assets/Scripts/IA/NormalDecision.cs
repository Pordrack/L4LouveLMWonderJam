using UnityEngine;

namespace IA
{
    public class NormalDecision : DecisionMaker
    {
        
        public NormalDecision(OtherNavBehavior nav, Brain brain, int moveAmount, Transform transform) : base(nav, brain, moveAmount, transform)
        {
        }

        public override void Decide()
        {
            for(var i=0; i<MoveAmount; i++)
            {
                var pos = Tf.position; //Get Cerf position
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
            Debug.Log("He's dead Jim.");
        }

        public override string GetId()
        {
            return "Normal";
        }
    }
}