using System.Collections.Generic;
using UnityEngine;

public class MamografiaAnim : MonoBehaviour
{
    public bool play;
    private List<Block> anim = new List<Block>();

    public GameObject personajeAAnimar;

    private AnimationComposer _composer;
    // Start is called before the first frame update
    void Start()
    {
        _composer = personajeAAnimar.GetComponent<AnimationComposer>();
        
        //resetear las extremidades que hayan quedado animadas
        List<LayerInfo> clear = new List<LayerInfo>();
        var animator = personajeAAnimar.GetComponent<Animator>();
        for (int l = 1; l < animator.layerCount; l++)
        {
            clear.Add(new LayerInfo("clear"+animator.GetLayerName(l)));
        }
        Block clearBlock = new Block(clear);
        anim.Add(clearBlock);
        
        /* "clearTorsoLayer", "RotTorsoL", "RotTorsoR", "Sad"
         "clearBothArmsLayer", "CrossArms", "GrabHead", "FistPump"
         "clearLeftArmLayer", "RaiseArmL", "ScratchHeadL"
         "clearRightArmLayer", "HandWave", "RaiseArmR", "ScratchHeadR", "ThumbsUp"
         "clearLegsLayer", "Jump", "Stomp" */
        
        CreateBlock(new List<string>(){"RaiseArmR"});
        CreateBlock(new List<string>(){"CrossArms"});
        CreateBlock(new List<string>(){"CrossArms"});
        CreateBlock(new List<string>(){"HandWave", "CrossArms"});
        CreateBlock(new List<string>(){"CrossArms"});
        CreateBlock(new List<string>(){"clearLegsLayer", "clearTorsoLayer", "clearRightArmLayer", "clearLeftArmLayer", "clearBothArmsLayer"});
    }

    // Update is called once per frame
    void Update()
    {
        if (!play)
            return;
        if (!personajeAAnimar)
            return;
        //le pasamos una copia de la queue anim asi anim no se vacia y podemos ejecutar la animacion mas de una vez

        foreach (var block in anim)
        {
            _composer.AddBlock(block);
        }
        
        play = false;
    }
    
    void CreateBlock(List<string> destinyStates)
    {
        List<LayerInfo> layers = new List<LayerInfo>();
        
        foreach (var state in destinyStates)
        {
            layers.Add(new LayerInfo(state));
        }

        Block block = new Block(layers);
        anim.Add(block);
    }
}
