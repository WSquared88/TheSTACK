using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int maxNumberOfBullets = 4;
    public Stack<Bullet> bullets;
	public float timeUntilReload;

	float currentReloadTime;

    public delegate void BulletPickedupEventHandler(Bullet bullet, int bulletNumberInMagazine);
    public static event BulletPickedupEventHandler BulletPickedup;
    public delegate void BulletFiredEventHandler(int numberOfBulletJustFired);
    public static event BulletFiredEventHandler BulletFired;

    // Use this for initialization
    void Start ()
    {
        bullets = new Stack<Bullet>(maxNumberOfBullets);
        PickupBullet(Instantiate(bulletPrefab).GetComponent<Bullet>());

        if(bullets.Peek() == null)
        {
            Debug.LogError("BulletManager: The bullet created from the bullet prefab is null. Did you remember to add the bullet script to it?");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetButtonDown("Reload"))
		{
			PickupBullet(Instantiate(bulletPrefab).GetComponent<Bullet>());
		}

		if(GameState.instance.mode == GameState.GunMode.TimedReloadAndUpgrade)
		{

		}
	}

    void PickupBullet(Bullet bullet)
    {
        if (bullets.Count < maxNumberOfBullets)
        {
			bullet.SubscribeToHit(OnBulletHit);
            bullets.Push(bullet);
			OnBulletPickedup(bullet, bullets.Count - 1);
        }
        else
        {
            Debug.Log("BulletManager: The magazine is full");
        }
    }

    public void FireNextBullet(Vector3 firingPosition, Vector3 direction, Vector3 fxLocation)
    {
        if (bullets.Count > 0)
        {
            bullets.Pop().Fire(firingPosition, direction, fxLocation);
            OnBulletFired(bullets.Count);
        }
        else
        {
            Debug.Log("BulletManager: The magazine is empty, unable to fire!");
        }
    }

    protected virtual void OnBulletPickedup(Bullet bullet, int bulletNumberInMagazine)
    {
        if (BulletPickedup != null)
        {
            BulletPickedup(bullet, bulletNumberInMagazine);
        }
    }

    protected virtual void OnBulletFired(int numberOfBulletJustFired)
    {
        if (BulletFired != null)
        {
            BulletFired(numberOfBulletJustFired);
        }
    }

	protected virtual void OnBulletHit()
	{
		if(GameState.instance.mode == GameState.GunMode.HitReloadAndUpgrade)
		{
			PickupBullet(Instantiate(bulletPrefab).GetComponent<Bullet>());
		}
	}
}
