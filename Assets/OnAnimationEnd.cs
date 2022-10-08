using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAnimationEnd : StateMachineBehaviour
{

    public static event Action<Animator> OnAnimationEndEvent;

    private bool _once = true;
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(_once && stateInfo.normalizedTime >= 1.0f)
        {
            _once = false;
            Debug.Log("Animation ended.");
            OnAnimationEndEvent?.Invoke(animator);
        }
    }

}
