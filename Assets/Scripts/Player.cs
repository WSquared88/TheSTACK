﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, IDamageComponent
{
	[SerializeField]
	int maxHealth;
	public int MaxHealth
	{
		get
		{
			return maxHealth;
		}
	}

	int currentHealth;
	public int CurrentHealth
	{
		get
		{
			return currentHealth;
		}

		set
		{
			currentHealth = value;
			CheckIfDead();
		}
	}

	// Use this for initialization
	void Start ()
	{
		if (maxHealth <= 0)
		{
			Debug.LogError("Player: I have no health, killing!");
			Die();
		}

		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void TakeDamage(int amountOfDamage)
	{
		Debug.Log("Player: I'm taking " + amountOfDamage + " damage!");
		currentHealth -= amountOfDamage;
		CheckIfDead();
	}

	public void CheckIfDead()
	{
		if(currentHealth <= 0)
		{
			Die();
		}
	}

	public void Die()
	{
		Debug.Log("Player: Dying!");
	}
}
