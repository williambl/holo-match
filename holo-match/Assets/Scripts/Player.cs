using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    public int maxHealth;
    public int health;

    public Player (int healthIn = 100, int maxHealthIn = 100) {
        maxHealth = maxHealthIn;
        health = healthIn;
    }

    public void TakeDamage(int amount) {
        health -= amount;
        if (CheckIfDead())
            Debug.Log("Dead!");
    }

    public bool CheckIfDead() {
        return health <= 0;
    }
}
