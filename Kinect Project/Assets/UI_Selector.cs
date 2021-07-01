using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Selector : MonoBehaviour
{
    public ArrayList triggers = new ArrayList();
    private int triggerIndex;
    public BlockCreationPanel BlockCreationPanel;
    public TextMeshProUGUI selectorText;

    private void Start()
    {
        // Load json
    }

    public void NextTrigger()
    {
        try
        {
            if (triggerIndex == triggers.Count - 1)
            {
                triggerIndex = 0;
                return;
            }

            triggerIndex += 1;
        }
        finally
        {
            selectorText.SetText(triggers[triggerIndex].ToString());
        }
    }
    
    public void PreviousTrigger()
    {
        try
        {
            if (triggerIndex == 0)
            {
                triggerIndex = triggers.Count - 1;
                return;
            }

            triggerIndex -= 1;
        }
        finally
        {
            selectorText.SetText(triggers[triggerIndex].ToString());
        }

    }

    public void tuhermana()
    {
        Debug.Log("SOY TU HERMANA");
    }
    
    public void addTrigger(int layer)
    {
        BlockCreationPanel.AddTrigger(layer,triggers[triggerIndex].ToString());
    }
}
