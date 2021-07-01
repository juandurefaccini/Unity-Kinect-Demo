using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCompositionController : MonoBehaviour
{
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
    private int currentReadyStates;
    private int totalReadyStates;

    private void Start()
    {
        animatorController = GetComponent<Animator>(); // Asigno el controller del personaje
        var readyBehaviours = animatorController.GetBehaviours<ReadyBehaviour>();
        foreach (var behaviour in readyBehaviours)
        {
            behaviour.CompositionController = this;
        }
        
        List <LayerInfo> d1 = new List <LayerInfo>();
        d1.Add(new LayerInfo("CrossArms"));
        d1.Add(new LayerInfo("HandWave"));
        Block b1 = new Block(d1);

        List <LayerInfo> d2 = new List <LayerInfo>();
        d2.Add(new LayerInfo("CrossArms"));
        Block b2 = new Block(d2);

        _blockQueue.Enqueue(b1);
        _blockQueue.Enqueue(b2);
        
        currentReadyStates = 0;
        totalReadyStates = readyBehaviours.Length;
    }

    public void signalAnimationComplete()
    {
        currentReadyStates++;

        Debug.Log(currentReadyStates);
    }

    // Aca podria ir un observer con el animator
    // private void Update()
    // {
    //     if (!_blockQueue.IsEmpty())
    //     {
    //         if (currentReadyStates == totalReadyStates)
    //         {
    //             Debug.Log("imheremyfellas"); //gets here immediately, should be a few seconds
    //             Block currentBlock = _blockQueue.Dequeue();
    //             ExecuteAnimationBlock(currentBlock);
    //         }
    //     }
    // }

    private void ExecuteAnimationBlock(Block block)
    {
        // Ejecuto el bloque
        foreach (LayerInfo layerInfo in block.GetLayerInfos()) // Por cada trigger de cada capa
        {
            animatorController.SetTrigger(layerInfo.destinyState); // Lo ejecuto
            currentReadyStates--;
        }
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
            Debug.Log("A");
            if (currentReadyStates == totalReadyStates)
            {
                Debug.Log("imheremyfellas"); //gets here immediately, should be a few seconds
                Block currentBlock = _blockQueue.Dequeue();
                ExecuteAnimationBlock(currentBlock);
            }
        }
    }
}