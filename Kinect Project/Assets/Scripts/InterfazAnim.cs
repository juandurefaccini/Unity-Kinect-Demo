using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InterfazAnim
{
    private bool play = false;
    public GameObject personaAAnimar;
    
    public void playAnim()
    {
        play = true;
    }
}
