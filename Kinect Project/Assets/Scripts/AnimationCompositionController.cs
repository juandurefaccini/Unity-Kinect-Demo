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
        private Dictionary<int, LayerInfo> stateTransitions;


        public Block(Dictionary<int, LayerInfo> stateTransitions)
        {
            this.stateTransitions = stateTransitions;
        }

        // Diccionario para almacenar el layer a editar y el valor del mismo

        public Dictionary<int, LayerInfo> GetLayerInfos()
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

        public bool IsEmpty() => blocks.Peek() is null; // Peek agarra la primera, si es null esta vacia
    }

    private readonly BlockQueue _blockQueue = new BlockQueue();
    public Animator animatorController;
    public bool isReady;

    private void Start()
    {
        animatorController = GetComponent<Animator>(); // Asigno el controller del personaje
        Dictionary<int, LayerInfo> d1 = new Dictionary<int, LayerInfo>();
        d1[1] = new LayerInfo("CrossArms");
        // l1.Add(new LayerInfo("HeadGrab"));
        Block b1 = new Block(d1);

        _blockQueue.Enqueue(b1);
    }

    private bool AnimatorIsReady()
    {
        for (int i = 1; i < animatorController.layerCount; i++)
        {
            if (!animatorController.GetCurrentAnimatorStateInfo(i).IsName("ready"))
            {
                return false;
            }
        }

        return true;
    }

    // Aca podria ir un observer con el animator
    private void Update()
    {
        if (!_blockQueue.IsEmpty()) // Si hay bloques
        {
            if (AnimatorIsReady()) // Si todos los layers estan en el estado "Ready"
            {
                Block currentBlock = _blockQueue.Dequeue();
                ExecuteAnimationBlock(currentBlock);
            }
        }
    }

    private void ExecuteAnimationBlock(Block block)
    {
        // Ejecuto el bloque
        foreach (LayerInfo layerInfo in block.GetLayerInfos().Values) // Por cada trigger de cada capa
        {
            animatorController.SetTrigger(layerInfo.destinyState); // Lo ejecuto
        }
    }
}