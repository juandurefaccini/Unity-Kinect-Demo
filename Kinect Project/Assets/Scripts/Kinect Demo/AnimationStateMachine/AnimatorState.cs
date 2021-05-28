using System;
using System.Collections;
using Kinect_Demo.AnimationStateMachine.AnimatorStates;
using UnityEngine;

namespace Kinect_Demo.AnimationStateMachine
{
    public abstract class AnimatorState
    {
        protected m_Animator MyAnimator;
        public String name;
        public AnimatorState(m_Animator animator)
        {
            MyAnimator = animator;
        }
        public virtual void Animate()
        {
            return;
        }

        public virtual void OnEntryAction()
        {
            return;
        }
        
        public virtual void OnExitAction()
        {
            return;
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public virtual IEnumerator ChangeState(AnimatorState animatorState)
        {
            yield break;
        }
    }
}