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
           
            //if he is close enough, attack him
           
            //if so, try to move towards him 
           
            //if not, try to move towards the last known position of the player
           
            //else move randomly
        }

        public override void Die()
        {
            throw new System.NotImplementedException();
        }
    }
}