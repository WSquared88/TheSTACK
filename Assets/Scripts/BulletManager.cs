using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int maxNumberOfBullets = 4;
    public Stack<Bullet> bullets;

	// Use this for initialization
	void Start ()
    {
        bullets = new Stack<Bullet>(maxNumberOfBullets);
        bullets.Push(Instantiate(bulletPrefab).GetComponent<Bullet>());
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void PickupBullet(Bullet bullet)
    {
        if (bullets.Count < maxNumberOfBullets)
        {
            bullets.Push(bullet);
        }
        else
        {
            Debug.Log("The magazine is full");
        }
    }

    public void FireNextBullet(Vector3 firingPosition, Vector3 direction, Vector3 fxLocation)
    {
        if (bullets.Count > 0)
        {
            bullets.Pop().Fire(firingPosition, direction, fxLocation);
            PickupBullet(Instantiate(bulletPrefab).GetComponent<Bullet>());
        }
        else
        {
            Debug.Log("Can't fire, the magazine is empty!");
        }
    }
}
