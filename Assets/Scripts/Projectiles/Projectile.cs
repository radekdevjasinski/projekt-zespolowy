using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{

    void Awake()
    {
        //Debug.Log("Porjectile awake: "+this.GetComponent<Collider2D>().isTrigger );
    }

    //Modifers
    private int damageModifer;
    private int rangeModifer;

    //base Attributes
    [Header("atrributes")]
    [SerializeField] protected int baseDamage;
    [SerializeField] private float baseLifeTime=0.5f;
    [SerializeField] private float projectileTimeMultiplayer=0.2f;

    [Header("sounds")]
    [SerializeField] GameObject onShootAudio;
    [SerializeField] GameObject onhitAudio;

    private bool wasShootByPlayer = false;
    //sets attribures, has to be activated to remove later projectile
    public void setupProjectileParams(int damageModifer, int rangeModifer)
    {
        if(onShootAudio!=null)
            SoundManager.instance.playSound(this.onShootAudio,transform.position);
        this.damageModifer = damageModifer;
        this.rangeModifer = rangeModifer;
        Invoke("kill", baseLifeTime+ (rangeModifer* projectileTimeMultiplayer));
    }


    private void kill()
    {
        OnDestroyed();
        Destroy(this.gameObject);
    }

    protected void OnTriggerEnter2D(Collider2D colider)
    {
        //Debug.Log("collsion: "+ colider.tag);
        if (colider.CompareTag("Collider") || colider.CompareTag("Entity") ||(colider.CompareTag("Player")& !wasShootByPlayer))
        {
            if (this.onhitAudio!= null)
                SoundManager.instance.playSound(this.onhitAudio,transform.position);
            onHit(colider.gameObject);
            kill();
        }
        if (colider.gameObject.TryGetComponent<KnightController>(out KnightController knightController))
        {
            if (this.onhitAudio != null)
                SoundManager.instance.playSound(this.onhitAudio, transform.position);
            knightController.TakeDamage(this.baseDamage);
            kill();
            Debug.Log("damaged " + this.baseDamage);
        if (colider.gameObject.TryGetComponent<ArcherPatrol>(out ArcherPatrol archerPatrol))
        {
            if (this.onhitAudio != null)
                SoundManager.instance.playSound(this.onhitAudio, transform.position);
            archerPatrol.TakeDamage(this.baseDamage);
            kill();
        }
    }

    protected abstract void onHit(GameObject obj);
    protected abstract void OnDestroyed();

    public void setWasShootByPlayer(bool val)
    {
        this.wasShootByPlayer = val;
    }
}
