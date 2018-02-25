using UnityEngine;
using System.Collections;

public class PierceBulletEffect : BulletEffect
{
    [SerializeField]
    float maxDistance;
    [SerializeField]
    float lineViewTime;
    LineRenderer line;

    // Use this for initialization
    void Start ()
    {
	    if(maxDistance <= 0)
        {
            maxDistance = 1000;
            Debug.LogWarning("The max distance wasn't set. Setting to " + maxDistance + ".");
        }

        if(lineViewTime <= 0)
        {
            lineViewTime = .5f;
            Debug.LogWarning("The view time for the pierce effect wasn't set. Setting to " + lineViewTime);
        }

        line = GetComponent<LineRenderer>();
        if(!line)
        {
            line = new LineRenderer();
            Debug.LogWarning("Couldn't find the line renderer! Creating a new one");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public override void Fire(Vector3 firingLocation, Vector3 direction, Vector3 fxLocation)
    {
        Debug.Log("Firing pierce bullet");
        RaycastHit[] targets = Physics.RaycastAll(firingLocation, direction, maxDistance);
        for (int i = 0; i < targets.Length; i++)
        {
            IDamageComponent target = targets[i].collider.GetComponent<IDamageComponent>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }

        Debug.Log("Firing line from " + firingLocation + " to " + firingLocation + direction.normalized * maxDistance);
        line.sortingOrder = 1;
        line.SetVertexCount(2);
        Debug.Log("Starting color: " + bulletColor);
        line.SetColors(bulletColor, bulletColor);
        line.SetPosition(0, fxLocation);
        line.SetPosition(1, firingLocation + direction.normalized * maxDistance);
        line.enabled = true;
        StartCoroutine("StartDelayLineTurnOff");
    }

    protected override void OnHit(IDamageComponent target)
    {
        target.TakeDamage(damage);
    }

    IEnumerator StartDelayLineTurnOff()
    {
        float timeLeft = lineViewTime;
        while(timeLeft > 0)
        {
            //Debug.Log("Time left for turn off: " + timeLeft);
            timeLeft -= Time.deltaTime;
            yield return null;
        }

        line.enabled = false;
    }
}
