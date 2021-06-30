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

    private class Block
    {
        // Clase encargada de almacenar los distintos cambios que se le haran a los layers
        private List <LayerInfo> stateTransitions;


        public Block(List <LayerInfo> stateTransitions)
        {
            this.stateTransitions = stateTransitions;
        }

        // Diccionario para almacenar el layer a editar y el valor del mismo

        public List <LayerInfo> GetLayerInfos()
        {
            return stateTransitions;
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
    public Animator animatorController;
    public bool isReady;

    private void Start()
    {
        animatorController = GetComponent<Animator>(); // Asigno el controller del personaje
        List <LayerInfo> d1 = new List <LayerInfo>();
        d1.Add(new LayerInfo("CrossArms"));
        d1.Add(new LayerInfo("HandWave"));
        Block b1 = new Block(d1);

        List <LayerInfo> d2 = new List <LayerInfo>();
        d2.Add(new LayerInfo("CrossArms"));
        Block b2 = new Block(d2);

        _blockQueue.Enqueue(b1);
        _blockQueue.Enqueue(b2);
        // _blockQueue.Enqueue(b1);
        //PeekBlocks() <-- va a haber uno
        isReady = true;
    }
    
    private bool AnimatorIsReady()
    {
        for (int i = 1; i < animatorController.layerCount; i++)
        {
            string layerName = animatorController.GetLayerName(i);
            // Debug.Log(layerName+".ready");
            var currentAnimationStateInfo = animatorController.GetCurrentAnimatorStateInfo(i);
            if (!currentAnimationStateInfo.IsName(layerName + ".ready"))
            {
                Debug.Log("i´m in");
                return false;
            }
        }

        return true;
    }
    
    // Aca podria ir un observer con el animator
    private void Update()
    {
        if (_blockQueue.IsEmpty()) return;
        if (!isReady) return;
        isReady = false;
        Debug.Log("imheremyfellas");
        Block currentBlock = _blockQueue.Dequeue();
        ExecuteAnimationBlock(currentBlock);
    }

    private void LateUpdate()
    {
        isReady = AnimatorIsReady();     
    }

    private void ExecuteAnimationBlock(Block block)
    {
        // Ejecuto el bloque
        foreach (LayerInfo layerInfo in block.GetLayerInfos()) // Por cada trigger de cada capa
        {
            animatorController.SetTrigger(layerInfo.destinyState); // Lo ejecuto
        }
    }
}