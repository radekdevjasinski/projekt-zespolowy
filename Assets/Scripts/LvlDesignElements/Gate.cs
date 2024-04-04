using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private bool isOpen = false;
    public string nextSceneName;
    private SceneController sceneController;
    private SpriteRenderer sr;
    private DestroyableObject destroyableObject;
    public Sprite openGate;
    private void Awake()
    {
        sceneController = GetComponent<SceneController>();
        sr = GetComponent<SpriteRenderer>();
        destroyableObject = GetComponent<DestroyableObject>();
    }
    private void FixedUpdate()
    {
        if (destroyableObject.health <= 0)
        {
            OpenGate();
        }
    }

    private void OpenGate()
    {
        sr.sprite = openGate;
        isOpen = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isOpen && collision.gameObject.tag == "Player")
        {
            sceneController.ChangeScene(nextSceneName);
        }
    }
}
