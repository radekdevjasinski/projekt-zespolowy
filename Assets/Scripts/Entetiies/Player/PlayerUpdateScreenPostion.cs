using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerUpdateScreenPostion : MonoBehaviour
{
    void LateUpdate()
    {
   
        Debug.Log("Cmear size: "+ new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight));
        Debug.Log("Camera.main.WorldToScreenPoint(transform.position): "+ Camera.main.WorldToScreenPoint(transform.position));
        Vector4 toUPdate = Camera.main.WorldToScreenPoint(transform.position)/ new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
        Debug.Log("player pos: "+ toUPdate);
        //Debug.Log("player pos global: " + Camera.main.WorldToScreenPoint(transform.position) );
    
        Shader.SetGlobalVector("_PlayerScreenPosition", toUPdate);
    }
}
