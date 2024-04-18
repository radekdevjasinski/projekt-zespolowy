using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected float healthPoints = 20f; //punkty �ycia wrog�w
    [SerializeField] protected float currentHealthPoints;
    protected float visionRange = 1f; //zasi�g widzenia wrog�w
    protected float damage = 1f; //damage wrog�w per hit
    protected float speed = 1f; //szybkosc wroga

    private float changeDirectionTimer = 3f; //czas po kt�rym zmieniamy kierunek ruchu
    protected float timer;
    private Vector2 randomDirection;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        currentHealthPoints = healthPoints;
        rb = GetComponent<Rigidbody2D>();
        timer = changeDirectionTimer;
        GetRandomDirection();
    }


    //metoda kt�ra sprawnia �e wr�g otrzymuje obra�enia, b�dzie wywo�ywana gdy wr�g otrzyma dmg
    public virtual void TakeDamage(float damage)
    {
        if (currentHealthPoints <= 0) //je�eli �ycie wroga b�dzie mniejsze lub rowne 0 to ginie
        {
            Die();
        }
        currentHealthPoints -= damage;
    }

    //metoda do poruszania si� wroga (domy�lnie randomowo)
    protected virtual void Move()
    {
        rb.MovePosition(rb.position + randomDirection * speed * Time.deltaTime);
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            GetRandomDirection();
            timer = changeDirectionTimer;
        }
    }

    //metoda do zmiany kierunku poruszania si� wroga
    protected virtual void GetRandomDirection()
    {
        float angle = Random.Range(0f, 2f * Mathf.PI);
        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);
        randomDirection = new Vector2(x, y).normalized;
    }

    //metoda do sprawdzania kolizji ze �cianami
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            GetRandomDirection();
        }
    }

    //metoda do sprawdzenia czy gracz jest w zasiegu wroga
    protected virtual bool IsWithinRange()
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        return distanceToPlayer <= visionRange; //je�li dystans od gracza dp wroga jest mniejszy
        //lub r�wny polu widzenia to zwracamy albo true albo false
    }

    //metoda, kt�ry niszczy obiekt wroga
    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    //metoda do ataku przeciwnika
    protected virtual void Attack()
    {
        //za�o�enie �e ka�dy rodzaj wroga ma inny spos�b ataku
    }
}
