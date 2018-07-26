using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletUI : MonoBehaviour
{
	Bullet bullet;
	[SerializeField]
	Image icon;

	public void AddBullet(Bullet newBullet)
	{
		bullet = newBullet;
		bullet.BulletUpgraded += ChangeBullet;
		icon.sprite = bullet.GetIcon();
		icon.enabled = true;
		gameObject.SetActive(true);
	}

	void ChangeBullet()
	{
		icon.sprite = bullet.GetIcon();
	}

	public void RemoveBullet()
	{
		//Debug.Log("BulletUI: Trying to remove bullet from " + gameObject.name);
		bullet.BulletUpgraded -= ChangeBullet;
		icon.sprite = null;
		icon.enabled = false;
		bullet = null;
		gameObject.SetActive(false);
	}
}
