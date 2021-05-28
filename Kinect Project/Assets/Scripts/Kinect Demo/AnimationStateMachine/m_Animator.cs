using System;
using Kinect_Demo.AnimationStateMachine.AnimatorStates;
using TMPro;
using UnityEngine;

namespace Kinect_Demo.AnimationStateMachine
{
    public class m_Animator : MonoBehaviour
    {
        private AnimatorState _sad;
        private AnimatorState _begin;

        private AnimatorState _currentState;
        public GameObject currentText;

        private void Start()
        {
            _sad = new HeadGrabState(this);
            _begin = new FoldedArmsState(this);
        
            _currentState = _begin;
        }

        public void SetState(AnimatorState animatorState)
        {
            Debug.Log("--------------------------------------" );
            Debug.Log("_currentState is "+_currentState.name );
            _currentState = animatorState;
            currentText.GetComponent<TextMeshProUGUI>().text = $"Current state is: {_currentState.name}";
            // currenText.text = "Current state is:  ${_currentState.name}";
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