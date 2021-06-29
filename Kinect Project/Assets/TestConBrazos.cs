using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestConBrazos : MonoBehaviour
{
    private Animator animator;

    private bool hace_cosas = true;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hace_cosas)
        {
            if (animator.GetCurrentAnimatorStateInfo(1).IsName("ready"))
            {
                animator.SetTrigger("CrossArms");
                //hace_cosas = false;
            }

            if (animator.GetCurrentAnimatorStateInfo(2).IsName("ready"))
            {
                animator.SetTrigger("HandWave");
            }
        }
    }
}
