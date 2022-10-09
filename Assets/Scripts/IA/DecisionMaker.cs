using UnityEngine;

namespace IA
{
    public abstract class DecisionMaker
    {
        protected OtherNavBehavior Nav;
        protected Brain Brain;
        protected int MoveAmount;
        protected Transform Tf;

        protected DecisionMaker(OtherNavBehavior nav, Brain brain, int moveAmount, Transform transform)
        {
            Nav = nav;
            Brain = brain;
            MoveAmount = moveAmount;
            Tf = transform;
        }

        public abstract void Decide();

        public abstract void Die();

        public abstract string GetId();
    }
}