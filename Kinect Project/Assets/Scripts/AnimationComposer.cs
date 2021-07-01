using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationComposer : MonoBehaviour
{
    private int animsInProgress = 0;
    public class LayerInfo
    {
        public string destinyState { get; }

        public LayerInfo(string destinyState)
        {
            this.destinyState = destinyState;
        }

    }

    public class Block
    {
        // Clase encargada de almacenar los distintos cambios que se le haran a los layers
        private List <LayerInfo> stateTransitions;


        // DEPRECATED
        public Block(List <LayerInfo> stateTransitions)
        {
            this.stateTransitions = stateTransitions;
        }

        public Block()
        {
            this.stateTransitions = new List<LayerInfo>();
        }

        // Diccionario para almacenar el layer a editar y el valor del mismo

        public List <LayerInfo> GetLayerInfos()
        {
            return stateTransitions;
        }
        
        public void AddLayerInfo(LayerInfo layerInfo)
        {
            stateTransitions.Add(layerInfo);
        }
    }

    private class BlockQueue
    {
        // Cola de prioridad de bloques que nos permite secuencializar las animaciones
        Queue<Block> blocks;

        public BlockQueue() => blocks = new Queue<Block>();

        // Encolar
        public void Enqueue(Block block) => blocks.Enqueue(block);

        // Desencolar
        public Block Dequeue() => blocks.Dequeue();

        public bool IsEmpty() => blocks.Count == 0; // Peek agarra la primera, si es null esta vacia
    }

    private readonly BlockQueue _blockQueue = new BlockQueue();
    private Animator animatorController;

    private void Start()
    {
        animatorController = GetComponent<Animator>(); // Asigno el controller del personaje
        var exitBehaviours = animatorController.GetBehaviours<ExitBehaviour>();
        foreach (var behaviour in exitBehaviours)
        {
            behaviour.CompositionController = this;
        }

        //ANGRY-START
        List <LayerInfo> d1 = new List <LayerInfo>();
        d1.Add(new LayerInfo("CrossArms"));
        d1.Add(new LayerInfo("RotTorsoR"));
        
        List <LayerInfo> d2 = new List <LayerInfo>();
        d2.Add(new LayerInfo("clearTorsoLayer")); 
        d2.Add(new LayerInfo("clearRightArmLayer"));
        d2.Add(new LayerInfo("Stomp")); 
        
        List <LayerInfo> d3 = new List <LayerInfo>();
        d3.Add(new LayerInfo("clearLegsLayer"));
        d3.Add(new LayerInfo("clearTorsoLayer"));
        d3.Add(new LayerInfo("clearLeftArmLayer"));

        Block b1 = new Block(d1);
        Block b2 = new Block(d2);
        Block b3 = new Block(d3);

        _blockQueue.Enqueue(b1);
        _blockQueue.Enqueue(b2);
        _blockQueue.Enqueue(b3);
        //ANGRY-END
        
        //HAPPY-START
        List <LayerInfo> d4 = new List <LayerInfo>();
        d4.Add(new LayerInfo("Jump"));
        //d4.Add(new LayerInfo("RaiseArmsOverHead"));
        _blockQueue.Enqueue(new Block(d4));
        List <LayerInfo> d5 = new List <LayerInfo>();
        d5.Add(new LayerInfo("ThumbsUp"));
        _blockQueue.Enqueue(new Block(d5));
        //HAPPY-END
        
        //SAD-START
        List <LayerInfo> d6 = new List <LayerInfo>();
        d6.Add(new LayerInfo("Sad"));
        d6.Add(new LayerInfo("GrabHead"));
        _blockQueue.Enqueue(new Block(d6));
        //SAD-END
    }

    // private void Update()
    // {
    //     if (!_blockQueue.IsEmpty() && animsInProgress == 0)
    //     {
    //         Block currentBlock = _blockQueue.Dequeue();
    //         ExecuteAnimationBlock(currentBlock);
    //     }
    // }

    private void ExecuteAnimationBlock(Block block)
    {
        animsInProgress = 0;
        // Ejecuto el bloque
        foreach (LayerInfo layerInfo in block.GetLayerInfos()) // Por cada trigger de cada capa
        {
            animatorController.SetTrigger(layerInfo.destinyState); // Lo ejecuto
            if (!layerInfo.destinyState.Contains("clear"))
            {
                animsInProgress++;
            }
        }
    }
    
    public void signalAnimationComplete()
    {
        animsInProgress--;
    }

    public void AddBlock(Block block)
    {
        _blockQueue.Enqueue(block);
    }

    public void StartAnimations()
    {
        Debug.Log("Aca arrancaria la animacion");
        while (!_blockQueue.IsEmpty())
        {
            if (animsInProgress == 0)
            {
                Block currentBlock = _blockQueue.Dequeue();
                ExecuteAnimationBlock(currentBlock);
            }
        }
    }
}
