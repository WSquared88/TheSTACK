using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagazineUI : MonoBehaviour
{
	BulletUI[] bullets;

	// Use this for initialization
	void Awake ()
    {
        BulletManager.BulletPickedup += OnBulletPickedup;
        BulletManager.BulletFired += OnBulletFired;
		bullets = GetComponentsInChildren<BulletUI>(true);
	}

    void OnBulletPickedup(Bullet bullet, int bulletNumberInMagazine)
    {
		bullets[bullets.Length - bulletNumberInMagazine - 1].AddBullet(bullet);
        Debug.Log("MagazineUI: Ammo was picked up.");
    }

    void OnBulletFired(int numberOfBulletJustFired)
    {
		bullets[bullets.Length - numberOfBulletJustFired - 1].RemoveBullet();
        Debug.Log("MagazineUI: A bullet was fired.");
    }
}
