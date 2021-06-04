using UnityEngine;

namespace Kinect_Demo.AnimationStateMachine
{
    public class StateMachine : MonoBehaviour
    {
        protected AnimatorState State;

        public void SetState(AnimatorState state)
        {
            State = state;
            State.Animate();
        }
    }
}