using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]

public class PierceBulletEffect : MonoBehaviour, IBulletEffect
{
    [SerializeField]
    float maxDistance;
    [SerializeField]
    float lineViewTime = 0.15f;
	[SerializeField]
	int damage;
    WaitForSeconds lineDuration;
    LineRenderer line;

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

	// Use this for initialization
	void Start ()
    {
	    if(maxDistance <= 0)
        {
            maxDistance = 1000;
            Debug.LogWarning("PierceBulletEffect: The max distance wasn't set. Setting to " + maxDistance + ".");
        }

        if(lineViewTime <= 0)
        {
            lineViewTime = .15f;
            Debug.LogWarning("PierceBulletEffect: The view time for the pierce effect wasn't set. Setting to " + lineViewTime);
        }

        lineDuration = new WaitForSeconds(lineViewTime);

        line = GetComponent<LineRenderer>();
        if(!line)
        {
            line = new LineRenderer();
            Debug.LogWarning("PierceBulletEffect: Couldn't find the line renderer! Creating a new one");
        }
	}

    public void Fire(Vector3 firingLocation, Vector3 direction, Vector3 fxLocation)
    {
        Debug.Log("PierceBulletEffect: Firing!");
        RaycastHit[] targets = Physics.RaycastAll(firingLocation, direction, maxDistance);
        for (int i = 0; i < targets.Length; i++)
        {
            IDamageComponent target = targets[i].collider.GetComponent<IDamageComponent>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }

		if (targets.Length > 0)
		{
			if (BulletHit != null)
			{
				BulletHit();
			}
		}

        Debug.Log("PierceBulletEffect: Firing line from " + firingLocation + " to " + firingLocation + direction.normalized * maxDistance);
        line.SetPosition(0, fxLocation);
        line.SetPosition(1, firingLocation + direction.normalized * maxDistance);
        StartCoroutine(StartFireEffect());
    }

    IEnumerator StartFireEffect()
    {
        line.enabled = true;
        yield return lineDuration;
        line.enabled = false;
    }
}
