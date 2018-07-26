using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class FastBulletEffect : MonoBehaviour, IBulletEffect
{
	[SerializeField]
	float force;

	public Bullet.BulletHitEventHandler BulletHit;

	[SerializeField]
	int timeUntilNextUpgrade;
	public int TimeUntilNextUpgrade
	{
		get
		{
			return timeUntilNextUpgrade;
		}
	}

	[SerializeField]
	Sprite icon;
	public Sprite Icon
	{
		get
		{
			return icon;
		}
	}

	[SerializeField]
	int damage;
	MeshRenderer mesh;

	void Start()
	{
		mesh = GetComponent<MeshRenderer>();

		if (!mesh)
		{
			Debug.LogError("FastBulletEffect: Unable to find the mesh renderer!");
		}
	}

	public void Fire(Vector3 firingLocation, Vector3 direction, Vector3 fxLocation)
	{
		Debug.Log("FastBulletEffect: Firing!");
		Rigidbody bullet = gameObject.GetComponent<Rigidbody>();
		bullet.velocity = Vector3.zero;
		bullet.position = firingLocation;
		bullet.AddForce(direction * force);
		mesh.enabled = true;
	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("FastBulletEffect: I'm hitting something!");
		IDamageComponent target = other.gameObject.GetComponent<IDamageComponent>();
		if (target != null)
		{
			Debug.Log("FastBulletEffect: It was the IDamageComponent of " + other.name);
			OnHit(target);
		}
	}

	protected virtual void OnHit(IDamageComponent target)
	{
		target.TakeDamage(damage);
		gameObject.SetActive(false);

		if (BulletHit != null)
		{
			BulletHit();
		}
	}
}
