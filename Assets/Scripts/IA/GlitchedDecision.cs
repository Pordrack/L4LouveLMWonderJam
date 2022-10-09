using UnityEngine;

namespace IA
{
    public class GlitchedDecision : DecisionMaker
    
    {
        public GlitchedDecision(OtherNavBehavior nav, Brain brain, int moveAmount, Transform transform) : base(nav, brain, moveAmount, transform)
        {
        }

        public override void Decide()
        {
            //Gello there
        }

        public override void Die()
        {
            throw new System.NotImplementedException();
        }

        public override string GetId()
        {
            return "glittched";
        }
    }
}