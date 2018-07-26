using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
	public enum GunMode
	{
		TimedReloadAndUpgrade,
		HitReloadAndUpgrade,
		AmmoPickupAndHitUpgrade,
		GunModeCount,
	}

	public static GameState instance;
	public GunMode mode = GunMode.TimedReloadAndUpgrade;

	private void Awake()
	{
		if(instance)
		{
			Debug.LogWarning("GameState: More than one GameState is trying to be made!");
			return;
		}

		instance = this;
	}

	void Update()
	{
		for(int i = 0;i<(int)GunMode.GunModeCount;i++)
		{
			if(Input.GetButtonDown("Num" + (i + 1)))
			{
				mode = (GunMode)i;
			}
		}
		//if(Input.GetKeyDown("Num" + GunMode.TimedReloadAndUpgrade + 1))
		//{
		//	mode = GunMode.TimedReloadAndUpgrade;
		//}
		//else if (Input.GetKeyDown("Num" + GunMode.HitReloadAndUpgrade + 1))
		//{
		//	mode = GunMode.HitReloadAndUpgrade;
		//}
		//else if (Input.GetKeyDown("Num" + GunMode.AmmoPickupAndHitUpgrade + 1))
		//{
		//	mode = GunMode.AmmoPickupAndHitUpgrade;
		//}
		
		if(Input.GetKeyDown(KeyCode.Joystick1Button4))
		{
			mode--;
		}
		else if(Input.GetKeyDown(KeyCode.Joystick1Button5))
		{
			mode++;
		}

		mode = (GunMode)(((int)mode + (int)GunMode.GunModeCount) % (int)GunMode.GunModeCount);

		Debug.Log("GameState: The current gun state is " + mode);
	}
}
