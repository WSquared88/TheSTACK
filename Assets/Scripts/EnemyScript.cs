using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour, IDamageComponent
{
    [SerializeField]
    float moveSpeed;
    Transform player;

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

	[SerializeField]
	int attackDamage;

    // Use this for initialization
    void Start ()
    {
        if(maxHealth <= 0)
        {
            Debug.LogError("EnemyScript: " + gameObject.name + " has no health, killing");
            Die();
        }

        currentHealth = maxHealth;

        if(moveSpeed < 0)
        {
            moveSpeed = 10;
            Debug.LogWarning("EnemyScript: " + name + " doesn't have a move speed, setting to " + moveSpeed);
        }

        player = GameObject.FindGameObjectWithTag("Player").transform;
        if(!player)
        {
            Debug.LogError("EnemyScript: Unable to find the player");
        }

		if(attackDamage <= 0)
		{
			attackDamage = 1;
			Debug.LogWarning("EnemyScript: The damage for the " + gameObject.name + " was not set. Setting to " + attackDamage);
		}
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += (player.position - transform.position).normalized * moveSpeed * Time.deltaTime;
	}

    public void TakeDamage(int amountOfDamage)
    {
        Debug.Log("EnemyScript: " + gameObject.name + " is taking " + amountOfDamage + " damage");
        CurrentHealth -= amountOfDamage;
    }

    public void CheckIfDead()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

	void OnCollisionEnter(Collision collision)
	{
		Debug.Log("EnemyScript: I'm colliding with something");
		IDamageComponent player = collision.gameObject.GetComponent<IDamageComponent>();
		if(player != null)
		{
			Debug.Log("EnemyScript: It was the player, attacking!");
			player.TakeDamage(attackDamage);
		}
	}
}
