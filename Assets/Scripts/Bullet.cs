using UnityEngine;
using System;
using System.Collections;

public class Bullet : MonoBehaviour
{
    IBulletEffect[] effects;
    int currentEffectIndex = 0;
    bool fired = false;
    float timeUntilNextUpgrade;
	//BulletManager magazine;

	public delegate void BulletUpgradedEventHandler();
	public event BulletUpgradedEventHandler BulletUpgraded;
	public delegate void BulletHitEventHandler();
	public static event BulletHitEventHandler BulletHit;

	void Awake()
	{
		effects = GetComponentsInChildren<IBulletEffect>(true);
	}

	// Use this for initialization
	void Start ()
    {
        Upgrade(currentEffectIndex);
		//magazine = BulletManager.instance;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!fired && timeUntilNextUpgrade <= 0 && currentEffectIndex + 1 < effects.Length)
        {
            Upgrade(currentEffectIndex + 1);
        }

		timeUntilNextUpgrade -= Time.deltaTime;
	}

	/// <summary>
	/// Upgrades a bullet to use the effect denoted by the nextEffectIndex. 
	/// Ex: if the current effect is the 2x damage bullet and the piercing bullet index is passed in then the bullet becomes the piercing bullet.
	/// </summary>
	/// <param name="nextEffectIndex">The index of the effect to upgrade the bullet to</param>
    void Upgrade(int nextEffectIndex)
    {
		if (nextEffectIndex >= 0 && nextEffectIndex < effects.Length)
		{
			Debug.Log("Bullet: Upgrading from " + currentEffectIndex + " to " + nextEffectIndex);
			// The effects are stored as IBulletEffect so I need to cast them as MonoBehaviour to get at their gameObject
			((MonoBehaviour)effects[currentEffectIndex]).gameObject.SetActive(false);
			((MonoBehaviour)effects[nextEffectIndex]).gameObject.SetActive(true);
			timeUntilNextUpgrade = effects[nextEffectIndex].TimeUntilNextUpgrade;
			currentEffectIndex = nextEffectIndex;
			OnBulletUpgraded();
		}
		else
		{
			Debug.LogError("Bullet: Attempting to upgrade the bullet to effect " + nextEffectIndex + " which doesn't exist!");
		}
    //    if (!fired)
    //    {
    //        Debug.Log("Bullet: Upgrading from " + prevEffectIndex + " to " + nextEffectIndex);
    //        if (prevEffectIndex >= 0 && prevEffectIndex < effects.Length)
    //        {
    //            effects[prevEffectIndex].isActive = false;
    //        }

    //        if (nextEffectIndex >= 0 && nextEffectIndex < effects.Length)
    //        {
    //            effects[nextEffectIndex].isActive = true;
    //            timeOfNextUpgrade = DateTime.Now.AddSeconds(effects[nextEffectIndex].TimeUntilNextUpgradeInSecs);
				//currentEffectIndex = nextEffectIndex;
				//OnBulletUpgraded();
    //        }
    //    }
    //    else if (fired)
    //    {
    //        Debug.Log("Bullet: Trying to upgrade when out of the magazine");
    //    }
    //    else
    //    {
    //        Debug.Log("Bullet: At max upgrade.");
    //    }
    }

    public void Fire(Vector3 firingLocation, Vector3 direction, Vector3 fxLocation)
    {
        Debug.Log("Bullet: Firing!");
        Debug.Log("Bullet: Activating effect " + effects[currentEffectIndex].ToString());
        fired = true;
        effects[currentEffectIndex].Fire(firingLocation, direction, fxLocation);
    }

	public Sprite GetIcon()
	{
		return effects[currentEffectIndex].Icon;
	}

    protected virtual void OnBulletUpgraded()
    {
		if(BulletUpgraded != null)
		{
			BulletUpgraded();
		}
		//magazine.OnMagazineChanged();
    }

	public void SubscribeToHit(BulletHitEventHandler bulletHitEvent)
	{
		for (int i = 0; i < effects.Length; i++)
		{
			effects[i].BulletHit += bulletHitEvent;
		}
	}

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("The bullet is hitting something");
    //    IDamageComponent target = other.gameObject.GetComponent<IDamageComponent>();
    //    if(target != null)
    //    {
    //        Debug.Log("Turns out it was the IDamageComponent of " + other.name);
    //        effects[currentEffectIndex].OnHit(target);
    //    }
    //}
}
