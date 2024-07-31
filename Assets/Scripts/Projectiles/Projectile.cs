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
    protected float damageModifer;
    protected float rangeModifer;

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
    public void setupProjectileParams(float damageModifer, float rangeModifer)
    {
        if(onShootAudio!=null)
            SoundManager.instance.playSound(this.onShootAudio,this.transform.position);
        this.damageModifer = damageModifer;
        this.rangeModifer = rangeModifer;
        Invoke("kill", baseLifeTime+ (rangeModifer* projectileTimeMultiplayer));
    }


    protected virtual void kill()
    {
        OnDestroyed();
        Destroy(this.gameObject);
    }

    protected void OnTriggerEnter2D(Collider2D colider)
    {
        if (colider.CompareTag("Shield"))
        {
            colider.gameObject.GetComponent<Shield>().shieldGlow();
            colider.gameObject.GetComponentInParent<ZombieShield>().hitShield(this.gameObject);
        }
        //Debug.Log("collsion: "+ colider.tag);
        if (colider.CompareTag("Collider") || colider.CompareTag("Entity") || colider.CompareTag("Enemy") || (colider.CompareTag("Player")& !wasShootByPlayer))
        {
            if (this.onhitAudio!= null)
                SoundManager.instance.playSound(this.onhitAudio,transform.position);
            onHit(colider.gameObject);
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
