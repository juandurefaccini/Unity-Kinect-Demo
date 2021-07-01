using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCreationPanel : MonoBehaviour
{
    private AnimationComposer.Block _block;
    public AnimationComposer AnimationComposer;

    private ArrayList layers_trigger = new ArrayList(){"","",""};

    private void Start()
    {
        transform.GetChild(0).GetComponent<UI_Selector>().triggers = new ArrayList(){"CrossArms","Mi","Cod"};
        transform.GetChild(1).GetComponent<UI_Selector>().triggers = new ArrayList(){"HandWave","Re","Mi"};
        transform.GetChild(2).GetComponent<UI_Selector>().triggers = new ArrayList(){"SILENT HILL","LA","SOL"};
    }

    public void AddTrigger(int layer,string trigger)
    {
        if (_block is null)
        {
            _block = new AnimationComposer.Block();
        }
        Debug.Log("Layer: "+layer);
        layers_trigger[layer] = trigger;        
    }

    public void CreateBlock()
    {
        foreach (string layerinfo in layers_trigger)
        {
            _block.AddLayerInfo(new AnimationComposer.LayerInfo(layerinfo));
        }
        AnimationComposer.AddBlock(_block);
        _block = null;
    }

    public void StartBlockQueue()
    {
        AnimationComposer.StartAnimations();
    }
}