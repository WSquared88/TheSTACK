using UnityEngine;
using System;
using System.Collections;

public class Spawner : MonoBehaviour
{
	[Serializable]
	public struct Wave
	{
		public float spawnDelay;
		public Transform[] enemies;
	}

	public enum SpawnState
	{
		SPAWNING,
		WAITING,
	}

    public Wave[] waves;
	int currentWave = 0;
    public int timeUntilNextWave;
	SpawnState state = SpawnState.WAITING;
	[SerializeField]
	Transform[] availableSpawnPoints;
    float timer;
	public float Timer
	{
		get
		{
			return timer;
		}
	}

	// Use this for initialization
	void Start ()
    {
        if(waves.Length == 0)
        {
            Debug.LogError("Spawner: There are no waves to use for spawning!");
        }

        if(timeUntilNextWave <= 0)
        {
            timeUntilNextWave = 15;
            Debug.LogWarning("Spawner: The timer is set incorrectly. Setting it to "+ timeUntilNextWave + ".");
        }

		if(availableSpawnPoints.Length == 0)
		{
			availableSpawnPoints = new Transform[1];
			availableSpawnPoints[0] = new GameObject().transform;
			Debug.LogWarning("Spawner: There weren't any spawn points set for the " + gameObject.name + " to spawn enemies. Using " + availableSpawnPoints[0].transform.position + " instead.");
		}

        timer = timeUntilNextWave;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(timer <= 0 && state != SpawnState.SPAWNING)
        {
			StartCoroutine(SpawnWave());
        }

		timer -= Time.deltaTime;
	}

	IEnumerator SpawnWave()
	{
		Debug.Log("Spawner: Spawning an enemy");
		state = SpawnState.SPAWNING;

		for (int i = 0; i < waves[currentWave].enemies.Length; i++)
		{
			Instantiate(waves[currentWave].enemies[i], availableSpawnPoints[UnityEngine.Random.Range(0,availableSpawnPoints.Length)].position, Quaternion.identity);
			yield return new WaitForSeconds(waves[currentWave].spawnDelay);
		}

		timer = timeUntilNextWave;
		currentWave = (currentWave + 1) % waves.Length;
		state = SpawnState.WAITING;
	}
}
