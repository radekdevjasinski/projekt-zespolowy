using UnityEngine;

public class AutoMovement : MonoBehaviour
{
    public float speed = 5f; 
    public float changeDirectionInterval = 2f; 

    private float timer;
    private Vector3 direction;
    private bool stopped = false; 
    void Start()
    {
      
        ChooseRandomDirection();
    }

    void Update()
    {
       
        if (stopped)
            return;

     
        timer += Time.deltaTime;

      
        if (timer >= changeDirectionInterval)
        {
         
            ChooseRandomDirection();

           
            timer = 0f;
        }

        
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void ChooseRandomDirection()
    {
        int randomDirection = Random.Range(0, 4);

       
        switch (randomDirection)
        {
            case 0:
                direction = Vector3.right;
                break;
            case 1:
                direction = Vector3.left;
                break;
            case 2:
                direction = Vector3.up;
                break;
            case 3:
                direction = Vector3.down;
                break;
        }
    }

   
    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Bullet"))
        {
           
            stopped = true;
        }
    }
}
