using UnityEngine;

public class ClearAnimations : InterfazAnim
{

    private AnimationComposer _composer;
    public override void playAnim()
    {
        play = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        _composer = personajeAAnimar.GetComponent<AnimationComposer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!play)
            return;
        if (!personajeAAnimar)
            return;
        
        _composer.ClearAnims();
        
        play = false;
    }
}
