using System;
using Kinect_Demo.AnimationStateMachine.AnimatorStates;
using UnityEngine;

namespace Kinect_Demo.AnimationStateMachine
{
    public class m_Animator : MonoBehaviour
    {
        private AnimatorState _sad;
        private AnimatorState _begin;

        private AnimatorState _animatorState;

        private void Start()
        {
            _sad = new _sad(this);
            _begin = new _begin(this);
        
            _animatorState = _begin;
        }

        public m_Animator()
        {
            _sad = new _sad(this);
            _begin = new _begin(this);

            _animatorState = _begin;
            
        }

        public void SetAnimatorState(AnimatorState animatorState)
        {
            StartCoroutine(_animatorState.ChangeState(animatorState));
        }

        public void OnSadButton()
        {
            Debug.Log("sadbutton");
            StartCoroutine(_animatorState.ChangeState(_sad));
        }
        
        public void OnBeginButton()
        {
            Debug.Log("beginbutton");
            StartCoroutine(_animatorState.ChangeState(_begin));
        }

        public void SetState(AnimatorState animatorState)
        {
            _animatorState = animatorState;
        }
    }
    
}