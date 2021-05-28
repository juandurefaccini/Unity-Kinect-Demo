using System.Collections;
using UnityEngine;

namespace Kinect_Demo.AnimationStateMachine.AnimatorStates
{
    public class _begin : AnimatorState
    {
        public _begin(m_Animator myAnimator) : base(myAnimator)
        {
            test();
        }

        public void test()
        {
            Debug.Log("begin");
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public override IEnumerator Start()
        {
            return base.Start();
        }

        public override IEnumerator PlayAnim()
        {
            Debug.Log($"Playing begin anim" );
            yield return new WaitForSeconds(2f);
            Debug.Log($"End playing begin anim" );
            Debug.Log($"Going to sad state" );
            yield return new WaitForSeconds(2f);
            MyAnimator.SetAnimatorState(new _sad(MyAnimator));
        }

        public override IEnumerator ChangeState(AnimatorState animatorState)
        {
            MyAnimator.SetState(animatorState);
            return base.ChangeState(animatorState);
        }
    }
}