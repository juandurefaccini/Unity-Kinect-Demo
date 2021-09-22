using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AnimActivatorGLOBAL : MonoBehaviour
{
    public Dictionary<string,MonoScript> anim_scripts;


    // Start is called before the first frame update
    void Start()
    {
        anim_scripts = new Dictionary<string, MonoScript>();
        foreach (var component in Resources.LoadAll<MonoScript>("AnimationScripts"))
        {
            anim_scripts[component.GetClass().ToString()] = component;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playAnim(string anim, GameObject personaje)
    {
        InterfazAnim animation = personaje.GetComponent(anim) as InterfazAnim;
        if (animation == null)
        {
            personaje.AddComponent(anim_scripts[anim].GetClass());
            animation = personaje.GetComponent(anim) as InterfazAnim;
            animation.personajeAAnimar = personaje;
        }
        animation.playAnim();
    }
}
