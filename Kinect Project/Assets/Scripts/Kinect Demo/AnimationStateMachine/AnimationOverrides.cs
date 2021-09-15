using System.Collections.Generic;
using UnityEngine;

namespace Kinect_Demo.AnimationStateMachine
{
    public struct AnimationOverrides
    {
        public AnimationClip OverridingClip;
        public Transform ClipMixingTransform;
        public AnimationBlendMode BlendMode;
        public WrapMode WrapMode;
        public float weight;
        public float speed;
        public int layer;
    }
}
