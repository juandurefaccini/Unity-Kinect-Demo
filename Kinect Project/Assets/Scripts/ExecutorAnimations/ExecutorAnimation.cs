using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecutorAnimation : MonoBehaviour
{
    private AngryAnim enojado;
    private SadAnim triste;
    
    // Start is called before the first frame update
    void Start()
    {
        enojado = GetComponent<AngryAnim>();
        triste = GetComponent<SadAnim>();

        Transform personaje1 = enojado.personajeAAnimar.gameObject.transform;
        Transform personaje2 = triste.personajeAAnimar.gameObject.transform;
        
        personaje1.LookAt(personaje2);
        personaje2.LookAt(personaje1);
        
        enojado.playAnim();
        triste.playAnim();
    }
}
