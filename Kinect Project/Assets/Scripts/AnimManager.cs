using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AnimManager : MonoBehaviour
{
    public GameObject animactivatorGLOBAL;

    public void playAnim(string anim)
    {
        animactivatorGLOBAL.GetComponent<AnimActivatorGLOBAL>().playAnim(anim, gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playAnim("AgusAnim"); 
        }

    }
}
