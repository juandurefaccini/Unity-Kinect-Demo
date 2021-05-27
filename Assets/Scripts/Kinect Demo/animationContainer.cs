using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using System.Threading;

public class animationContainer : MonoBehaviour
{
    private Dictionary<string, AnimationClip> animations;
    // Start is called before the first frame update
    public AnimationClip[] anims;
    void Start()
    {
    }

    public AnimationClip GetAnimation(string key)
    {
        string newPath = "HumanoidAnimations/"+key;
        Debug.Log(newPath);
        return Resources.Load<AnimationClip>(newPath);
    }
}
