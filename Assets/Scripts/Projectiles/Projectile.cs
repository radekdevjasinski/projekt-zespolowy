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
    [SerializeField] private int baseDamage;
    [SerializeField] private float baseLifeTime=0.5f;
    [SerializeField] private float projectileTimeMultiplayer=0.2f;

    //sets attribures, has to be activated to remove later projectile
    public void setupProjectileParams(int damageModifer, int rangeModifer)
    {
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
        Debug.Log("tigger enter");
        onHit(colider.gameObject);
        kill();
    }

    protected abstract void onHit(GameObject obj);
    protected abstract void OnDestroyed();
}
