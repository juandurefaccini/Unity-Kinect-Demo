using System.Collections;
using UnityEngine;

namespace Kinect_Demo.AnimationStateMachine.AnimatorStates
{
    public class SadState : AnimatorState
    {
        public SadState(m_Animator myAnimator) : base(myAnimator)
        {
            name = "Sad";
        }

        public override void OnEntryAction()
        {
            Animate();
        }

        public override void Animate()
        {
            Debug.Log("Playing Sad animation");
            Debug.Log("Sad animation ended");
        }

        public override IEnumerator ChangeState(AnimatorState animatorState)
        {
            MyAnimator.SetState(animatorState);
            return base.ChangeState(animatorState);
        }
    }
}