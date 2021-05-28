using System.Collections;
using UnityEngine;

namespace Kinect_Demo.AnimationStateMachine.AnimatorStates
{
    public class HeadGrabState : AnimatorState
    {
        public HeadGrabState(m_Animator myAnimator) : base(myAnimator)
        {
            name = "HeadGrabState";
        }

        public override void OnEntryAction()
        {
            Animate();
        }

        public override void Animate()
        {
            Debug.Log("Playing Sad animation");
            Animation animationComponent = MyAnimator.GetComponent<Animation>();
            animationComponent.Play("oof1");
            Debug.Log("Sad animation ended");
        }

        public override IEnumerator ChangeState(AnimatorState animatorState)
        {
            MyAnimator.SetState(animatorState);
            return base.ChangeState(animatorState);
        }
    }
}