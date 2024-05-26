using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static CameraController Instance { get; private set; }
    private Camera camera;
    private void Awake()
    {
      

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            camera = GetComponentInChildren<Camera>();
            Instance = this;
        }
    }


    public void Shake()
    {
        Debug.Log("Aniamtior shake Camera" +
                  "");
        this.GetComponent<Animator>().SetTrigger("Shake");
    }

}
