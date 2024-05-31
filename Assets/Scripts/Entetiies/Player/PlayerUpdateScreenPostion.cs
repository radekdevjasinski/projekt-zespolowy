using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpdateScreenPostion : MonoBehaviour
{
    void LateUpdate()
    {
        //Debug.Log("Cmear size: "+ new Vector2(Screen.height, Screen.width));
        //Debug.Log("player pos: "+ Camera.main.WorldToScreenPoint(transform.position) / new Vector2(Screen.height, Screen.width));
        Shader.SetGlobalVector("_PlayerScreenPosition",Camera.main.WorldToScreenPoint(transform.position)/new Vector2(Screen.height,Screen.width));
    }
}
