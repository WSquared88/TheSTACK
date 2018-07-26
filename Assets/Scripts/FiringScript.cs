using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BulletManager))]

public class FiringScript : MonoBehaviour
{
    public Camera playerCamera;
    public Transform FxLocation;
    [HideInInspector]
    public BulletManager magazine;

	// Use this for initialization
	void Start ()
    {
        if (!playerCamera)
        {
            playerCamera = GetComponent<Camera>();
            Debug.LogWarning("FiringScript: The camera wasn't provided for the bullet to be fired from. Grabbing it.");
        }

        if(!FxLocation)
        {
            FxLocation = GetComponent<Camera>().transform;
            Debug.LogWarning("FiringScript: There isn't a place to spawn the FX! Spawning them at the camera");
        }

        magazine = GetComponent<BulletManager>();
        if(!magazine)
        {
            Debug.LogError("FiringScript: Couldn't make the magazine for the gun.");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("FiringScript: Firing!");
            magazine.FireNextBullet(playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)), playerCamera.transform.forward, FxLocation.position);
        }
	}
}
