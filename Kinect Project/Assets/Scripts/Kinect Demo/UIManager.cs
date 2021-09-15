using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    Animator anim;
    Animator anim_sphere;
    public GameObject cube;
    public GameObject sphere;
    // Start is called before the first frame update
    void Start()
    {
        anim = cube.GetComponent<Animator>();
        anim_sphere = sphere.GetComponent<Animator>();
    }

    public void Strafe(string newState)
    {
        anim.SetTrigger("tr_"+ newState);
        anim_sphere.SetTrigger("tr_"+ newState);
    }
    
}
