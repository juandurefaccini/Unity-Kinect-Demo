using System.Collections.Generic;
using UnityEngine;

public class AnimationComposer : MonoBehaviour
{
    private int _animsInProgress;
    private bool _started;
    /*
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
    */
    private BlockQueue _blockQueue = new BlockQueue();
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>(); // Asigno el controller del personaje
        var exitBehaviours = _animator.GetBehaviours<ExitBehaviour>();
        foreach (var behaviour in exitBehaviours)
        {
            behaviour.compositionController = this;
        }
        /*
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
        d3.Add(new LayerInfo("clearBothArmsLayer"));

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
        d4.Add(new LayerInfo("FistPump"));
        _blockQueue.Enqueue(new Block(d4));
        List <LayerInfo> d5 = new List <LayerInfo>();
        d5.Add(new LayerInfo("clearBothArmsLayer"));
        d5.Add(new LayerInfo("ThumbsUp"));
        _blockQueue.Enqueue(new Block(d5));
        List <LayerInfo> dc = new List <LayerInfo>();
        dc.Add(new LayerInfo("clearLegsLayer"));
        dc.Add(new LayerInfo("clearRightArmLayer"));
        _blockQueue.Enqueue(new Block(dc));
        //HAPPY-END
        
        //SAD-START
        List <LayerInfo> d6 = new List <LayerInfo>();
        d6.Add(new LayerInfo("Sad"));
        d6.Add(new LayerInfo("GrabHead"));
        _blockQueue.Enqueue(new Block(d6));
        List <LayerInfo> d7 = new List <LayerInfo>();
        d7.Add(new LayerInfo("clearTorsoLayer"));
        d7.Add(new LayerInfo("clearBothArmsLayer"));
        _blockQueue.Enqueue(new Block(d7));
        //SAD-END
        */
        StartAnimations();
    }

    private void Update()
    {
        if (!_started)
            return;
        
        
        if (!_blockQueue.IsEmpty() && _animsInProgress == 0)
        {
            Block currentBlock = _blockQueue.Dequeue();
            ExecuteAnimationBlock(currentBlock);
        }
    }

    private void ExecuteAnimationBlock(Block block)
    {
        _animsInProgress = 0;
        // Ejecuto el bloque
        foreach (LayerInfo layerInfo in block.GetLayerInfos()) // Por cada trigger de cada capa
        {
            _animator.SetTrigger(layerInfo.DestinyState); // Lo ejecuto
            if (!layerInfo.DestinyState.Contains("clear"))
            {
                _animsInProgress++;
            }
        }
    }
    
    public void SignalAnimationComplete()
    {
        _animsInProgress--;
        Debug.Log(_animsInProgress);
    }

    public void AddBlock(Block block)
    {
        _blockQueue.Enqueue(block);
    }

    public void StartAnimations()
    {
        _started = true;
    }
    
    public void ClearAnims()
    {
        var clear = new List<LayerInfo>();
        for (var l = 1; l < _animator.layerCount; l++)
        {
            clear.Add(new LayerInfo("clear"+_animator.GetLayerName(l)));
        }
        _blockQueue.Clear();
        _animsInProgress = 0;
        _blockQueue.Enqueue(new Block(clear));
    }
}
