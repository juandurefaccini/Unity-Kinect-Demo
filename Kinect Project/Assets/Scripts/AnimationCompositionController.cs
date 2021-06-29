using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCompositionController : MonoBehaviour
{
    public class LayerInfo
    {
        public string destinyState;

        public LayerInfo(string destinyState)
        {
            this.destinyState = destinyState;
        }
    }

    public class Block
    {
        // Clase encargada de almacenar los distintos cambios que se le haran a los layers
        List<LayerInfo> layerInfo;


        public Block(List<LayerInfo> _layerInfo)
        {
            layerInfo = _layerInfo;
        }

        // Diccionario para almacenar el layer a editar y el valor del mismo

        public List<LayerInfo> GetLayerInfos()
        {
            return layerInfo;
        }
    }

    public class BlockQueue
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

    private void Start()
    {
        List<LayerInfo> l1 = new List<LayerInfo>();
        l1.Add(new LayerInfo("crossArms"));
        Block b1 = new Block(l1);

        _blockQueue.Enqueue(b1);
    }

    // Aca podria ir un observer con el animator
    private void Update()
    {
        if (_blockQueue.IsEmpty())
        {
            // Por qeu joraca es un cero
            if (animatorController.GetCurrentAnimatorStateInfo(0).IsName("ready"))
            {
                ExecuteAnimationBlock(_blockQueue.Dequeue());
            }
        }

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