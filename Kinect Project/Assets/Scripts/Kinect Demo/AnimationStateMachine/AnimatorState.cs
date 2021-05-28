using System.Collections;
using Kinect_Demo.AnimationStateMachine.AnimatorStates;
using UnityEngine;

namespace Kinect_Demo.AnimationStateMachine
{
    public abstract class AnimatorState
    {
        protected m_Animator MyAnimator;

        public AnimatorState(m_Animator animator)
        {
            MyAnimator = animator;
        }
        public virtual IEnumerator Start()
        {
            yield break;
        }
        
        public virtual IEnumerator PlayAnim()
        {
            yield break;
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public virtual IEnumerator ChangeState(AnimatorState animatorState)
        {
            yield break;
        }
    }
}