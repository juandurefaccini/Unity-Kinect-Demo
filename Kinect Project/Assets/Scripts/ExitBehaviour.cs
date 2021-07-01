using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBehaviour : StateMachineBehaviour
{
    private bool notified = false;
    
    public AnimationComposer CompositionController;
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime % 1 > 0.99 && !notified)
        {
            CompositionController.signalAnimationComplete();
            notified = true; //para que pregunte una sola vez
        }
    }
}
