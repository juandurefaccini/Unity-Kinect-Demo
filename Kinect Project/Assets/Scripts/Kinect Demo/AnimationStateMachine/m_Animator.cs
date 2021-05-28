using System;
using Kinect_Demo.AnimationStateMachine.AnimatorStates;
using UnityEngine;

namespace Kinect_Demo.AnimationStateMachine
{
    public class m_Animator : MonoBehaviour
    {
        private AnimatorState _sad;
        private AnimatorState _begin;

        private AnimatorState _currentState;

        private void Start()
        {
            _sad = new SadState(this);
            _begin = new BeginState(this);
        
            _currentState = _begin;
        }

        public void SetState(AnimatorState animatorState)
        {
            Debug.Log("--------------------------------------" );
            Debug.Log("_currentState is "+_currentState.name );
            _currentState = animatorState;
            _currentState.OnEntryAction();
            Debug.Log("_currentState is "+_currentState.name );
            Debug.Log("--------------------------------------" );
        }

        public void OnSadButton()
        {
            _currentState.ChangeState(_sad);
        }

        public void OnBeginButton()
        {
            _currentState.ChangeState(_begin);
        }
    }
    
}