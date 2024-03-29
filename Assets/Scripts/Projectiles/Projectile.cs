using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{

    void Awake()
    {
        Debug.Log("Porjectile awake: "+this.GetComponent<Collider2D>().isTrigger );
    }

    //Modifers
    private int damageModifer;
    private int rangeModifer;

    //base Attributes
    [Header("atrributes")]
    [SerializeField] private int baseDamage;
    [SerializeField] private float baseLifeTime=0.5f;
    [SerializeField] private float projectileTimeMultiplayer=0.2f;

    [Header("sounds")]
    [SerializeField] AudioClip onShootAudio;
    [SerializeField] AudioClip onhitAudio;

    //sets attribures, has to be activated to remove later projectile
    public void setupProjectileParams(int damageModifer, int rangeModifer)
    {
        if(onShootAudio!=null)
            SoundManager.playSound(this.onShootAudio);
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
        Debug.Log("collsion: "+ colider.tag);
        if (!colider.CompareTag("Player") && colider.CompareTag("Collider") || colider.CompareTag("Entity"))
        {
            if (this.onhitAudio!= null)
                SoundManager.playSound(this.onhitAudio);
            onHit(colider.gameObject);
            kill();
        }
    }

    protected abstract void onHit(GameObject obj);
    protected abstract void OnDestroyed();
}
