using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorOverrider : MonoBehaviour // Cambias las animaciones ent tiempo de ejecucion
{
    private Animator animator;
    //private AnimatorOverrideController animator2;
    private AnimatorOverrideController animatorOverrideController;
    public AnimationClip anim;

    public void Start()
    {
        animator = GetComponent<Animator>();
        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animatorOverrideController;
    }

    public void changePersonality(string animName, AnimationClip animType) //recibe string 
    {
        anim = animType;
        string oldName = animName + "_0";
        if(animType.name != animatorOverrideController[oldName].name){
            animatorOverrideController[oldName] = animType;
        }
    }

    public void changeState(string newState){
        animator.SetTrigger("tr_"+newState);
    }

}