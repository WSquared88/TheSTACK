using UnityEngine;
using System;
using System.Collections;

public abstract class BulletEffect : MonoBehaviour
{
    [SerializeField]
    protected float force;
    [SerializeField]
    protected int timeUntilNextUpgrade;
    public int TimeUntilNextUpgrade
    {
        get
        {
            return timeUntilNextUpgrade;
        }
    }

    [SerializeField]
    protected int damage;
    [SerializeField]
    protected Color bulletColor;
    public bool isActive = false;

    public virtual void Fire(Vector3 firingLocation, Vector3 direction, Vector3 fxLocation)
    {
        Debug.Log("Firing in BulletEffect");
        Rigidbody bullet = gameObject.GetComponent<Rigidbody>();
        GetComponent<Renderer>().material.SetColor("_Color", bulletColor);
        bullet.velocity = Vector3.zero;
        bullet.position = firingLocation;
        bullet.AddForce(direction * force);
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("The bullet is hitting something");
        IDamageComponent target = other.gameObject.GetComponent<IDamageComponent>();
        if (target != null)
        {
            Debug.Log("Turns out it was the IDamageComponent of " + other.name);
            OnHit(target);
           // effects[currentEffectIndex].OnHit(target);
        }
    }

    protected virtual void OnHit(IDamageComponent target)
    {
        if (isActive)
        {
            target.TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
