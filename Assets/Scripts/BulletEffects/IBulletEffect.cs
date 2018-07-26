using UnityEngine;
using System;
using System.Collections;

public interface IBulletEffect
{
	event Bullet.BulletHitEventHandler BulletHit;
	int TimeUntilNextUpgrade { get; }
	Sprite Icon { get; }
	void Fire(Vector3 firingLocation, Vector3 direction, Vector3 fxLocation);
}

//public abstract class BulletEffect : MonoBehaviour
//{
//    [SerializeField]
//    protected float force;
//    [SerializeField]
//    protected int timeUntilNextUpgradeInSecs;
//    public int TimeUntilNextUpgradeInSecs
//    {
//        get
//        {
//            return timeUntilNextUpgradeInSecs;
//        }
//    }

//	[SerializeField]
//	protected Sprite icon;
//	public Sprite Icon
//	{
//		get
//		{
//			return icon;
//		}
//	}

//    [SerializeField]
//    protected int damage;
//    [SerializeField]
//    protected Color bulletColor;
//    //public bool isActive = false;

//    public virtual void Fire(Vector3 firingLocation, Vector3 direction, Vector3 fxLocation)
//    {
//        Debug.Log("BulletEffect: Firing!");
//        Rigidbody bullet = gameObject.GetComponent<Rigidbody>();
//        GetComponent<Renderer>().material.SetColor("_Color", bulletColor);
//        bullet.velocity = Vector3.zero;
//        bullet.position = firingLocation;
//        bullet.AddForce(direction * force);
//        gameObject.SetActive(true);
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        Debug.Log("BulletEffect: I'm hitting something!");
//        IDamageComponent target = other.gameObject.GetComponent<IDamageComponent>();
//        if (target != null)
//        {
//            Debug.Log("BulletEffect: It was the IDamageComponent of " + other.name);
//            OnHit(target);
//           // effects[currentEffectIndex].OnHit(target);
//        }
//    }

//	protected virtual void OnHit(IDamageComponent target)
//	{
//		target.TakeDamage(damage);
//		gameObject.SetActive(false);
//	}
//}
