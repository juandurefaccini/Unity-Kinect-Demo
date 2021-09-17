using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimManager : MonoBehaviour
{
    public GameObject RuletaAnimacionesGeneral;

    public void playAnim(string anim)
    {
        RuletaAnimacionesGeneral.GetComponent<AnimActivator>().playAnim(anim, gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            playAnim("SadAnimInterfaz");
        }*/
    }
}
