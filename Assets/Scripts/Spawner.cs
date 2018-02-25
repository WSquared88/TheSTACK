using UnityEngine;
using System;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject prototype;
    public int spawnTimer;
    DateTime timer;

	// Use this for initialization
	void Start ()
    {
        if(!prototype)
        {
            Debug.LogError("There is no prototype to use for spawning!");
        }

        if(spawnTimer <= 0)
        {
            spawnTimer = 15;
            Debug.LogWarning("The timer is set incorrectly. Setting it to "+ spawnTimer + ".");
        }

        timer = DateTime.Now.AddSeconds(spawnTimer);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(timer <= DateTime.Now)
        {
            Debug.Log("Spawning an enemy");
            Instantiate(prototype, transform.position, Quaternion.identity);
            timer = DateTime.Now.AddSeconds(spawnTimer);
        }
	}
}
