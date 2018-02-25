using UnityEngine;
using System.Collections;

public interface IDamageComponent
{
    int MaxHealth { get; }
    int CurrentHealth { get; set; }

    void TakeDamage(int amountOfDamage);
    void CheckIfDead();
    void Die();
}
