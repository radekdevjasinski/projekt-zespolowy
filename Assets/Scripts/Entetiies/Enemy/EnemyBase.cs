using UnityEngine;



public class EnemyBase : EntityController<float>
{
    [SerializeField] protected float maxHelathPoints = 20f; //punkty zycia wrogow
    [SerializeField] protected float currentHealthPoints;
    [SerializeField] protected float visionRange = 1f; //zasieg widzenia wrogiw
    [SerializeField] protected int damage = 1; //obrazenia wroga
    [SerializeField] protected float speed = 1f; //szybkosc wroga
    public float dropItemChance = .5f; 


    private float changeDirectionTimer = 3f; //czas po kt?rym zmieniamy kierunek ruchu
    protected float timer;
    private Vector2 randomDirection;
    protected Rigidbody2D rb;
    public bool LockMovement = true;

    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject potion;



    private Transform playerTransform;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentHealthPoints = maxHelathPoints;
        rb = GetComponent<Rigidbody2D>();
        timer = changeDirectionTimer;
        GetRandomDirection();
        //Debug.Log("get player transofrm");
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }


    //metoda kt?ra sprawnia ?e wr?g otrzymuje obra?enia, b?dzie wywo?ywana gdy wr?g otrzyma dmg


    public override void reviceDamage(float damage, Vector2 damageDirection)
    {
        //Debug.Log("enemy recive damage, now: " + this.getHealth());
        currentHealthPoints -= damage; //sprawdzenie smeirci jest w klasie bazowej
    }
    public override void reviceDamage(float damage)
    {
        //Debug.Log("enemy recive damage, now: " + this.getHealth());
        currentHealthPoints -= damage; //sprawdzenie smeirci jest w klasie bazowej
    }

    public override void dealDamageUniversal(float amount)
    {
        dealDamage(amount);
    }

    public override void dealDamageUniversal(float dmg, Vector2 vector2)
    {
        dealDamage(dmg, vector2);
    }

    //metoda do poruszania si? wroga (domy?lnie randomowo)
    protected virtual void Move()
    {
        if (!LockMovement)
        {
            rb.MovePosition(rb.position + randomDirection * speed * Time.deltaTime);
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                GetRandomDirection();
                timer = changeDirectionTimer;
            }
        }

    }

    //metoda do zmiany kierunku poruszania si? wroga
    protected virtual void GetRandomDirection()
    {
        float angle = Random.Range(0f, 2f * Mathf.PI);
        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);
        randomDirection = new Vector2(x, y).normalized;
    }

    //metoda do sprawdzania kolizji ze ?cianami
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
        //Debug.Log("Is within range");
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        return distanceToPlayer <= visionRange; //je?li dystans od gracza dp wroga jest mniejszy
        //lub r?wny polu widzenia to zwracamy albo true albo false
    }

    //metoda, kt?ry niszczy obiekt wroga
    protected override void onDie()
    {
        isLoot();
        Destroy(gameObject);
    }

    //metoda do ataku przeciwnika
    protected virtual void Attack()
    {
        if (!LockMovement)
        {
            //za?o?enie ?e ka?dy rodzaj wroga ma inny spos?b ataku//za?o?enie ?e ka?dy rodzaj wroga ma inny spos?b ataku//za?o?enie ?e ka?dy rodzaj wroga ma inny spos?b ataku//za?o?enie ?e ka?dy rodzaj wroga ma inny spos?b ataku//za?o?enie ?e ka?dy rodzaj wroga ma inny spos?b ataku//za?o?enie ?e ka?dy rodzaj wroga ma inny spos?b ataku
        }

    }


    public override float getHealth()
    {
        return currentHealthPoints;
    }

    public override float getMaxHealth()
    {
        return maxHelathPoints;
    }

    public GameObject lootEnemy()
    {

        int randomNumber = Random.Range(0, 11);
        switch (randomNumber)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                return coin;
            case 4:
            case 5:
            case 6:
                return bomb;
            case 7:
            case 8:
            case 9:
                return potion;
            default:
                return key;
        }
    }
    public void isLoot()
    {
        float randomNumber = Random.Range(0f, 1f);
        if (randomNumber < dropItemChance)
        {
            Instantiate(
            lootEnemy(),
            this.transform.position,
            new Quaternion(0, 0, 0, 0)
        );
        }
    }
}