using System.Collections.Generic;
using UnityEngine;

public class FelizAmigable : MonoBehaviour
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
        
        //START
        List <LayerInfo> d6 = new List <LayerInfo>(); 
        d6.Add(new LayerInfo("Excited"));
        d6.Add(new LayerInfo("JoyfullJump"));
        anim.Add(new Block(d6));
        List <LayerInfo> d9 = new List <LayerInfo>();
        d9.Add(new LayerInfo("clearTorsoLayer"));
        d9.Add(new LayerInfo("clearBothArmsLayer"));
        d9.Add(new LayerInfo("clearLegsLayer"));
        anim.Add(new Block(d9));
        //END
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
}