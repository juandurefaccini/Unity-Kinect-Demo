using System.Collections.Generic;
using UnityEngine;

namespace Kinect_Demo.AnimationStateMachine
{
    //interfaces
    public interface ICharacterMoveState
    {
        public ICharacterMoveState HandleInput(Animation animation, string movement);
        public void Enter(Animation animation);
    }

    
    public interface ICharacterEmotionState
    {
        public ICharacterEmotionState HandleInput(Animation animation, string emotion,
            List<AnimationOverrides> animationOverridesList);

        public void Enter(Animation animation);
    }
    
    //movimientos concretos
    
    //podriamos hacerlas estaticas porque no tienen atributos, pero capaz queremos que tenga un atributo
    //para la animacion asi le podemos asignar diferentes animaciones segun el personaje
    public class IdleMoveState : ICharacterMoveState
    {
        public void Enter(Animation animation)
        {
            animation.CrossFade("dab1"); //placeholder
        }
        public ICharacterMoveState HandleInput(Animation animation, string movement)
        {

            if (movement == "walking")
            {
                return new WalkingMoveState();
            }

            return null;
        }
    }

    public class WalkingMoveState : ICharacterMoveState
    {
        public ICharacterMoveState HandleInput(Animation animation, string movement)
        {
            if (movement == "idle")
            {
                return new IdleMoveState();
            }

            return null;
        }

        public void Enter(Animation animation)
        {
            animation.CrossFade("saludo1");
        }
    }
    
    /*
    --Si usaramos State--
    Todas las emociones tienen el mismo comportamiento, como se puede pasar de cualquier emocion a cualquier
    otra no ganamos mucho usando el state, se duplicaria el codigo en cada estado completo
    
    //emociones concretas
    
    //estas no pueden ser estaticas porque van a tener los overrides que le corresponden a cada personaje
    //los estados son para que se separe el control de la construccion del perfil. En basa a la IA definimos
    //como van a ser los estados para cada emocion de un personaje
    public class NormalEmotionState : ICharacterEmotionState
    {
        private List<AnimationOverrides> _overridesList;//sale del perfil
        public ICharacterEmotionState HandleInput(Animation animation, string emotion, List<AnimationOverrides> animationOverridesList)
        {
            if (emotion == "happy")
            {
                return new HappyEmotionState();
            }
            
            _overridesList = animationOverridesList;
            
            return null;
        }

        private void SetAnimConfig(Animation animation, AnimationOverrides anim)
        {
            if (anim.ClipMixingTransform != null)
            {
                animation[anim.OverridingClip.name].AddMixingTransform(anim.ClipMixingTransform);
            }
            animation[anim.OverridingClip.name].layer = 1; //el movimiento esta en la capa 0 por
            //defecto, poner las emociones en la capa 1 hace que se reemplace la animacion de
            //movimiento por la animacion de emocion.
            animation[anim.OverridingClip.name].blendMode = anim.BlendMode;
            animation[anim.OverridingClip.name].wrapMode = anim.WrapMode;
            animation[anim.OverridingClip.name].enabled = true;
            animation[anim.OverridingClip.name].weight = anim.weight;
            animation[anim.OverridingClip.name].speed = anim.speed;
        }

        public void Enter(Animation animation)
        {
            foreach (AnimationOverrides anim in _overridesList)
            {
                if (animation[anim.OverridingClip.name] == null) //si no esta, lo agregamos
                {
                    animation.AddClip(anim.OverridingClip, anim.OverridingClip.name);
                    SetAnimConfig(animation, anim);
                }
                else
                {
                    SetAnimConfig(animation, anim);
                }
                animation.CrossFade(anim.OverridingClip.name);
            }
        }
    }
    */
}
