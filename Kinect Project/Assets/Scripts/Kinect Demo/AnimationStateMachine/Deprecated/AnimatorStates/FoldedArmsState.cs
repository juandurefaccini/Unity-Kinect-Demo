using System.Collections;
using UnityEngine;

namespace Kinect_Demo.AnimationStateMachine.AnimatorStates
{
    public class FoldedArmsState : AnimatorState
    {
        public FoldedArmsState(m_Animator myAnimator) : base(myAnimator)
        {
            name = "FoldedArmsState"; 
        }

        public override void OnEntryAction()
        {
            Animate();
        }

        public override void Animate()
        {
            Debug.Log("Playing Begin animation");
            Animation animationComponent = MyAnimator.GetComponent<Animation>();
            animationComponent.Play("crucebrazos");
            Debug.Log("Begin animation ended");
        }

        public override IEnumerator ChangeState(AnimatorState animatorState)
        {
            MyAnimator.SetState(animatorState);
            // OnExitAction();
            return base.ChangeState(animatorState);
        }
    }
}