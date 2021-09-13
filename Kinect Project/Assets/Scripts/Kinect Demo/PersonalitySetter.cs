using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalitySetter : MonoBehaviour
{

    public GameObject character; //cube es placeholder de avatar

    public void CambiarAnimacion(string newAnim)
    {
        // Debug.Log(newAnim);
        string oldAnim = newAnim.Split('_')[0];
        Debug.Log(oldAnim);
        character.GetComponent<AnimatorOverrider>().changePersonality(oldAnim, GetComponent<animationContainer>().GetAnimation(newAnim));
    }

}