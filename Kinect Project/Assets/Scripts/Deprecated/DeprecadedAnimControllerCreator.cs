using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class DeprecatedAnimControllerCreator : MonoBehaviour
{
    /*
    public class ReadyBehaviour : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Debug.Log("Entro al estado Ready");
        }
    }
    */
    
    [Serializable] public struct stateDefinition
    {
        public string stateName;
        public Motion stateAnimation;
        public string parameterName;
    }
    [Serializable] public struct LayerDefinition
    {
        public string layerName;
        public stateDefinition[] _stateDefinitions;
        public AvatarMask mask;

    }

    public LayerDefinition[] layers;
    public string controllerName = "default";
    /*
    public AvatarMask[] avatarMasks;
    public Motion[] animations;
    */
    void Start()
    {
        CreateController();
    }

    void CreateController()
    {
        var controller =
            UnityEditor.Animations.AnimatorController.CreateAnimatorControllerAtPath(
                ("Assets/" + controllerName + ".controller"));

        //capa base
        var defaultState = controller.layers[0].stateMachine.AddState(layers[0]._stateDefinitions[0].stateName);
        defaultState.motion = layers[0]._stateDefinitions[0].stateAnimation;
        controller.layers[0].stateMachine.defaultState = defaultState;
        
        //otras capas
        for (int i = 1; i < layers.Length; i++)
        {
            var currentLayerDef = layers[i];
            controller.AddLayer(currentLayerDef.layerName); //la layer queda con numero i
            
            //para que Unity nos deje modificar las propiedades de controller.layers
            AnimatorControllerLayer[] l = controller.layers;
            l[i].avatarMask = currentLayerDef.mask;
            l[i].defaultWeight = 1;
            controller.layers = l;
            
            var currentStateMachine = controller.layers[i].stateMachine;
            
            //estados
            var layerReadyState = currentStateMachine.AddState("ready");
            currentStateMachine.defaultState = layerReadyState;
            layerReadyState.AddStateMachineBehaviour<ReadyBehaviour>();
            
            foreach (var stateDefinition in currentLayerDef._stateDefinitions)
            {
                //el parametro para ir de ready al nuevo estado
                controller.AddParameter(stateDefinition.parameterName, AnimatorControllerParameterType.Trigger);
                //agrega el nuevo estado con su nombre y  animacion
                var state = currentStateMachine.AddState(stateDefinition.stateName);
                state.motion = stateDefinition.stateAnimation;
                //agrega la transicion del ready al estado agregado
                layerReadyState.AddTransition(state, false)
                    .AddCondition(AnimatorConditionMode.If, 0, stateDefinition.parameterName);
                //agrega la transicion del estado agregado de nuevo al ready
                var backToReady = state.AddTransition(layerReadyState, false);
                backToReady.hasExitTime = true;
                backToReady.exitTime = 1;
                backToReady.duration = 0.5f;
            }
            

        } 
        /*
         //parametros
        controller.AddParameter("CrossArms", AnimatorControllerParameterType.Trigger);
        controller.AddParameter("HandWave", AnimatorControllerParameterType.Trigger);
        controller.AddParameter("HeadGrab", AnimatorControllerParameterType.Trigger);

        //maquinas de estado
        controller.AddLayer("TorsoLayer"); //done
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
        */
    }
    
}
