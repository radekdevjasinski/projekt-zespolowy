using UnityEngine;

public class EnemyBase : EntityController<float>
{
    [SerializeField]
    protected const float maxHelathPoints = 20f; //punkty ¿ycia wrogów
    protected float currentHealthPoints;
    protected float visionRange = 1f; //zasiêg widzenia wrogów
    protected int damage = 1; //damage wrogów per hit
    protected float speed = 1f; //szybkosc wroga

    private float changeDirectionTimer = 3f; //czas po którym zmieniamy kierunek ruchu
    protected float timer;
    private Vector2 randomDirection;
    private Rigidbody2D rb;


    private Transform playerTransform;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentHealthPoints = maxHelathPoints;
        rb = GetComponent<Rigidbody2D>();
        timer = changeDirectionTimer;
        GetRandomDirection();
        Debug.Log("get player transofrm");
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }


    //metoda która sprawnia ¿e wróg otrzymuje obra¿enia, bêdzie wywo³ywana gdy wróg otrzyma dmg


    protected override void reviceDamage(float damage)
    {
        Debug.Log("enemy recive damage, now: "+ this.getHealth());
        currentHealthPoints -= damage; //sprawdzenie smeirci jest w klasie bazowej
    }

    //metoda do poruszania siê wroga (domyœlnie randomowo)
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

    //metoda do zmiany kierunku poruszania siê wroga
    protected virtual void GetRandomDirection()
    {
        float angle = Random.Range(0f, 2f * Mathf.PI);
        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);
        randomDirection = new Vector2(x, y).normalized;
    }

    //metoda do sprawdzania kolizji ze œcianami
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
       // (przeniseion wyszukiwanie obekitu player do zmiennej globlanej aby nie trzeba bylo szukac do update) 
       Debug.Log("Is within range");
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        return distanceToPlayer <= visionRange; //jeœli dystans od gracza dp wroga jest mniejszy
        //lub równy polu widzenia to zwracamy albo true albo false
    }

    //metoda, który niszczy obiekt wroga
    protected override void onDie()
    {
        Destroy(gameObject);
    }

    //metoda do ataku przeciwnika
    protected virtual void Attack()
    {
        //za³o¿enie ¿e ka¿dy rodzaj wroga ma inny sposób ataku
    }


    protected override float getHealth()
    {
        return currentHealthPoints;
    }

    protected override float getMaxHealth()
    {
        return maxHelathPoints;
    }

 
}
