using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

    [SyncVar]
    public int maxHealth = 100;
    [SyncVar (hook="OnHealthChange")]
    public int health = 100;

    public PlayerController pcontroller;

    private Text healthText;


    void Awake () {
        healthText = transform.Find("Canvas/healthText").GetComponent<Text>();
        pcontroller = GetComponent<PlayerController>();
        OnHealthChange(health);
    }

    public void TakeDamage(int amount) {
        health -= amount;
    }

    public bool CheckIfDead() {
        return health <= 0;
    }

    public void OnHealthChange (int healthIn) {
        if (CheckIfDead())
           Debug.Log("Dead"); 
        healthText.text = healthIn.ToString();
    }
}
