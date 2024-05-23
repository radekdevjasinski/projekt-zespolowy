using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveLoadManager : MonoBehaviour
{
    void Start()
    {
        if (SaveLoadManager.instance.GameLoaded)
        {
            GameData gameData = SaveLoadManager.instance.LoadGame();
        }
    }
}
