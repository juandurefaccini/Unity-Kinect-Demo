using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimActivator : MonoBehaviour
{
    private Dictionary<string,InterfazAnim>  anim_scripts;

    // Start is called before the first frame update
    void Start()
    {
        anim_scripts = new Dictionary<string, InterfazAnim>();
        foreach (var component in GetComponents<InterfazAnim>())
        {
            if (component != this)
            {
                anim_scripts[component.GetType().ToString()]=component;
                Debug.Log("LA CLAVE ES: " + component.GetType().ToString());
            }
        }
    }

    public void playAnim(string anim, GameObject personaje)
    {
        anim_scripts[anim].playAnim(); 
    }

    // Update is called once per frame
    void Update()
    {
    }
}
