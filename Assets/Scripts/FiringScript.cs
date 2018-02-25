using UnityEngine;
using System.Collections;

public class FiringScript : MonoBehaviour
{
    public Transform FiringLocation;
    public Transform FxLocation;
    public float force;
    public BulletManager magazine;

	// Use this for initialization
	void Start ()
    {
        if (!FiringLocation)
        {
            FiringLocation = GetComponent<Camera>().transform;
            Debug.LogWarning("No firing location provided for bullet to be fired from. Using camera's location.");
        }

        if(!FxLocation)
        {
            FxLocation = GetComponent<Camera>().transform;
            Debug.LogWarning("There isn't a place to spawn the FX! Spawning them at the camera");
        }

        magazine = GetComponent<BulletManager>();
        if(!magazine)
        {
            Debug.LogError("Couldn't make the magazine for the gun.");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Firing in FiringScript");
            magazine.FireNextBullet(FiringLocation.position, FiringLocation.forward, FxLocation.position);
        }
	}
}
