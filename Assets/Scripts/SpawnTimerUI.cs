using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SpawnTimerUI : MonoBehaviour
{
	[SerializeField]
	Spawner spawner;
	[SerializeField]
	Text spawnTimerText;

	// Use this for initialization
	void Start ()
	{
		if (!spawner)
		{
			GameObject spawnObject = GameObject.Find("Spawn Manager");

			if (spawnObject)
			{
				spawner = spawnObject.GetComponent<Spawner>();
			}

			if (!spawner)
			{
				Debug.LogError("SpawnTimerUI: Unable to find a spawner manager!");
			}
			else
			{
				Debug.LogWarning("SpawnTimerUI: No spawner was set to check the time with, but I found one in the scene.");
			}
		}

		if(!spawnTimerText)
		{
			spawnTimerText = GetComponent<Text>();
			Debug.LogWarning("SpawnTimerUI: The timer text wasn't set! Grabbing it from the object.");
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		spawnTimerText.text = "Next wave in " + Mathf.Clamp((int)spawner.Timer, 0, int.MaxValue) + " seconds";
	}
}
