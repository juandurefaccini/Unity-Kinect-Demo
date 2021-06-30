using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class ReadyBehaviour : StateMachineBehaviour
{
    public AnimationCompositionController CompositionController;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CompositionController.signalAnimationComplete();
    }
}
