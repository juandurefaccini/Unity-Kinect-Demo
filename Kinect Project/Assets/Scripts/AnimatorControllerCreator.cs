using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class AnimatorControllerCreator : MonoBehaviour
{
    public AvatarMask[] avatarMasks;
    public Motion[] animations;
    
    void Start()
    {
        CreateController();
    }

    void CreateController()
    {
        var controller =
            UnityEditor.Animations.AnimatorController.CreateAnimatorControllerAtPath(
                ("Assets/ComposedAnims.controller"));
                
        //parametros
        controller.AddParameter("CrossArms", AnimatorControllerParameterType.Trigger);
        controller.AddParameter("HandWave", AnimatorControllerParameterType.Trigger);
        controller.AddParameter("HeadGrab", AnimatorControllerParameterType.Trigger);

        //maquinas de estado
        controller.AddLayer("TorsoLayer");
        controller.AddLayer("HeadLayer");
        var torsoStateMachine = controller.layers[1].stateMachine;
        controller.layers[1].defaultWeight = 1;
        var headStateMachine = controller.layers[2].stateMachine;
        controller.layers[2].defaultWeight = 1;
        
        
        //estados
        var torsoReady = torsoStateMachine.AddState("ready");
        torsoStateMachine.defaultState = torsoReady;
        var armCrossState = torsoStateMachine.AddState("stateCrossArms");
        armCrossState.motion = animations[0];
        var handWaveState = torsoStateMachine.AddState("stateHandWave");
        handWaveState.motion = animations[1];

        var headReady = headStateMachine.AddState("ready");
        headStateMachine.defaultState = headReady;
        var headGrabState = headStateMachine.AddState("stateGrabHead");
        headGrabState.motion = animations[2];
        
        //mascaras
        controller.layers[1].avatarMask = avatarMasks[0];
        controller.layers[2].avatarMask = avatarMasks[1];
        
        //transiciones
        var transitionCrossArms = torsoReady.AddTransition(armCrossState, false);
        transitionCrossArms.AddCondition(AnimatorConditionMode.If, 0, "CrossArms");
        transitionCrossArms.duration = 0;

        var transitionHandWave = torsoReady.AddTransition(handWaveState, false);
        transitionHandWave.AddCondition(AnimatorConditionMode.If, 0, "HandWave");
        transitionHandWave.duration = 0;

        var transitionBackToReady = armCrossState.AddTransition(torsoReady, false);
        transitionBackToReady.hasExitTime = true;
        transitionBackToReady.exitTime = 1;
        transitionBackToReady.duration = 0;
        
        transitionBackToReady = handWaveState.AddTransition(torsoReady, false);
        transitionBackToReady.hasExitTime = true;
        transitionBackToReady.exitTime = 1;
        transitionBackToReady.duration = 0;
        
        var transitionHeadGrab = headReady.AddTransition(headGrabState, false);
        transitionHeadGrab.AddCondition(AnimatorConditionMode.If, 0, "HeadGrab");
        transitionHeadGrab.duration = 0;

        transitionBackToReady = headGrabState.AddTransition(headReady, false);
        transitionBackToReady.hasExitTime = true;
        transitionBackToReady.exitTime = 1;
        transitionBackToReady.duration = 0;

    }
    
}
