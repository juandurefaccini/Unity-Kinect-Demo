using System.Collections;
using UnityEngine;

namespace Kinect_Demo.AnimationStateMachine.AnimatorStates
{
    public class _sad : AnimatorState
    {
        public _sad(m_Animator myAnimator) : base(myAnimator)
        {
            test();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public override IEnumerator Start()
        {
            return base.Start();
        }
        public void test()
        {
            Debug.Log("begin");
        }

        public override IEnumerator PlayAnim()
        {
            // MyAnimator.SetAnimatorState(new _begin(MyAnimator));
            Debug.Log($"Playing sad anim" );
            yield return new WaitForSeconds(2f);
            Debug.Log($"End playing sad anim" );
            yield return null;
        }

        public override IEnumerator ChangeState(AnimatorState animatorState)
        {
            PlayAnim();
            Debug.Log($"Going to begin state" );
            yield return new WaitForSeconds(2f);
            yield return base.ChangeState(animatorState);
        }
    }
}