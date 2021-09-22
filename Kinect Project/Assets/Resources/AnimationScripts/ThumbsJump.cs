using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ThumbsJump : InterfazAnim
{
    private List<Block> anim = new List<Block>();
       
    private AnimationComposer _composer;

    public override void playAnim()
    {
        play = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        _composer = personajeAAnimar.GetComponent<AnimationComposer>();

        //resetear las extremidades que hayan quedado animadas
        List<LayerInfo> clear = new List<LayerInfo>();
        var animator = personajeAAnimar.GetComponent<Animator>();
        for (int l = 1; l < animator.layerCount; l++)
        {
            clear.Add(new LayerInfo("clear" + animator.GetLayerName(l)));
        }
        Block clearBlock = new Block(clear);
        anim.Add(clearBlock);

        //OTHER-START
        //Lista 7 reservada para los clears mas abajo
        List<LayerInfo> d6 = new List<LayerInfo>();
        List<LayerInfo> d8 = new List<LayerInfo>();
        List<LayerInfo> d9 = new List<LayerInfo>();
        //List<LayerInfo> d10 = new List<LayerInfo>();
        //List<LayerInfo> d11 = new List<LayerInfo>();

        d6.Add(new LayerInfo("ThumbsUp"));
        anim.Add(new Block(d6));
        
        d8.Add(new LayerInfo("clearBothArmsLayer"));
        anim.Add(new Block(d8));

        d9.Add(new LayerInfo("Jump"));
        anim.Add(new Block(d9));

        
        //d10.Add(new LayerInfo("clearLeftArmLayer"));
        //anim.Add(new Block(d10));
        

        List<LayerInfo> d7 = new List<LayerInfo>();
        d7.Add(new LayerInfo("clearTorsoLayer"));
        d7.Add(new LayerInfo("clearLeftArmLayer"));
        d7.Add(new LayerInfo("clearRightArmLayer"));
        d7.Add(new LayerInfo("clearBothArmsLayer"));    
        anim.Add(new Block(d7));
        //OTHER-END
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
