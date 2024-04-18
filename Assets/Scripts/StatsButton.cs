using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsButton : MonoBehaviour
{
    public GameObject statsPanel; 
    private bool statsPanelActive = false; 
    void Start()
    {
        statsPanel.SetActive(false);
    }

    public void ToggleStatsPanel()
    {
        statsPanelActive = !statsPanelActive;
        statsPanel.SetActive(statsPanelActive);
    }
}

