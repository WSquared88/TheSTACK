using UnityEngine;
using System;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public BulletEffect[] effects;
    int currentEffectIndex = 0;
    bool fired = false;
    DateTime timeToNextUpgrade;

	// Use this for initialization
	void Start ()
    {
        effects = GetComponents<BulletEffect>();
        Upgrade(currentEffectIndex, currentEffectIndex);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (timeToNextUpgrade <= DateTime.Now && currentEffectIndex + 1 < effects.Length)
        {
            Upgrade(currentEffectIndex, currentEffectIndex + 1);
            currentEffectIndex++;
        }
	}

    void Upgrade(int currentEffectIndex, int nextEffectIndex)
    {
        if (!fired)
        {
            Debug.Log("Upgrading from " + currentEffectIndex + " to " + nextEffectIndex);
            if (currentEffectIndex >= 0 && currentEffectIndex < effects.Length)
            {
                effects[currentEffectIndex].isActive = false;
            }

            if (nextEffectIndex >= 0 && nextEffectIndex < effects.Length)
            {
                effects[nextEffectIndex].isActive = true;
                timeToNextUpgrade = DateTime.Now.AddSeconds(effects[nextEffectIndex].TimeUntilNextUpgrade);
            }
        }
        else if (fired)
        {
            Debug.Log("Trying to upgrade when out of the magazine");
        }
        else
        {
            Debug.Log("At max upgrade.");
        }
    }

    public void Fire(Vector3 firingLocation, Vector3 direction, Vector3 fxLocation)
    {
        Debug.Log("Firing in Bullet");
        Debug.Log("Activating effect " + effects[currentEffectIndex].ToString());
        fired = true;
        effects[currentEffectIndex].Fire(firingLocation, direction, fxLocation);
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
