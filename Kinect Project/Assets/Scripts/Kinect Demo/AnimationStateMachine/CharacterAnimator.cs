using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kinect_Demo.AnimationStateMachine
{
    public class CharacterAnimator : MonoBehaviour
    {
        //esto deberia estar en otro lado...basicamente es una instancia de AnimationOverrides,
        //seguramente no deberia instanciarse aca
        public AnimationClip happyClipDemo;
        public AnimationClip otraDemo;
        public Transform MixingTransform;
        public Transform OtraTransform;
        
        private ICharacterMoveState _movementState;

        //private ICharacterEmotionState _emotionState;

        private Animation _animation;

        public Dictionary<string, List<AnimationOverrides>> EmotionOverrides { get; set; } //seteados por la IA
        
        private void HandleInput(Animation animationComponent, string movement,
            List<AnimationOverrides> overridingAnimClips)
        {
            ICharacterMoveState newMovementState = 
                _movementState.HandleInput(animationComponent, movement);
            
            if (newMovementState != null)
            {
                _movementState = newMovementState; //transicion de estado
            }

            _movementState.Enter(_animation);
            
            /*
             si usamos estados para las emociones...todas tienen el mismo comportamiento, capaz no es necesaria
             la maquina de estados
             
            ICharacterEmotionState newEmotionState =
                _emotionState.HandleInput(animationComponent, emotion, overridingAnimClips);
            
            if (newEmotionState != null)
            {
                _emotionState = newEmotionState;
            }
            */
            
            SetEmotionAnimations(_animation, overridingAnimClips);
        }


        private void SetAnimConfig(Animation animationComponent, AnimationOverrides anim)
        {
            if (anim.ClipMixingTransform != null)
            {
                animationComponent[anim.OverridingClip.name].AddMixingTransform(anim.ClipMixingTransform);
            }
            animationComponent[anim.OverridingClip.name].layer = anim.layer; //el movimiento esta en la capa 0 por
            //defecto, poner las emociones en la capa 1 hace que se reemplace la animacion de
            //movimiento por la animacion de emocion.
            animationComponent[anim.OverridingClip.name].blendMode = AnimationBlendMode.Blend;
            //animationComponent[anim.OverridingClip.name].wrapMode = anim.WrapMode;
            animationComponent[anim.OverridingClip.name].enabled = true; 
            animationComponent[anim.OverridingClip.name].weight = 1.0f;
            //animationComponent[anim.OverridingClip.name].speed = anim.speed;
        }
            

        private void SetEmotionAnimations(Animation animationComponent, List<AnimationOverrides> overridesList)
        {
            foreach (AnimationOverrides anim in overridesList)
            {
                if (animationComponent[anim.OverridingClip.name] == null) //si no esta, lo agregamos
                {
                    animationComponent.AddClip(anim.OverridingClip, anim.OverridingClip.name);
                    SetAnimConfig(animationComponent, anim);
                }
                else
                {
                    SetAnimConfig(animationComponent, anim);
                }
                Debug.Log("playing" + anim.OverridingClip.name);
                Debug.Log(anim.ClipMixingTransform.name);
                Debug.Log(anim.layer);
                animationComponent.CrossFade(anim.OverridingClip.name);
            }
        }
        
        // Start is called before the first frame update
        void Start()
        {
        _movementState = new IdleMoveState();

        //_emotionState = NormalEmotionState;

        _animation = GetComponent<Animation>();
        
        //conseguir los overrides de la IA (placeholder)
        var animationOverrides = new AnimationOverrides
        {
            OverridingClip = happyClipDemo, WrapMode = WrapMode.Loop, ClipMixingTransform = MixingTransform, layer = 1
        };
        var overridesList = new List<AnimationOverrides> {animationOverrides};
        /*
        overridesList.Add(new AnimationOverrides
        {
            OverridingClip = otraDemo, WrapMode = WrapMode.Loop, ClipMixingTransform = OtraTransform, layer = 1
        });*/
        var keyValuePair = new KeyValuePair<string, List<AnimationOverrides>>("happy", overridesList);
        EmotionOverrides = new Dictionary<string, List<AnimationOverrides>>();
        EmotionOverrides.Add(keyValuePair.Key, keyValuePair.Value);
        }

        // Update is called once per frame
        void Update()
        {
            var detectedMovement = "walking"; //placeholder
            var detectedEmotion = "happy"; //placeholder
            HandleInput(_animation, detectedMovement, EmotionOverrides[detectedEmotion]);
        }
    }
}
