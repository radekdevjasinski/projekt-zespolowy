using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBomb : MonoBehaviour
{
    public GameObject bomb;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            CreateBomb();
        }
    }
    public void CreateBomb() 
    {
        GameObject newBomb = Instantiate(bomb, transform.position, Quaternion.identity);
    }

}
