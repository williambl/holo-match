using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    private Camera cam;
    private Canvas canvas;
    public NetworkStartPosition[] spawnPoints;

    public Health health;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    public Inventory inventory;

    // Use this for initialization
    void Awake () {
	cam = GetComponentInChildren<Camera>();
        canvas = GetComponentInChildren<Canvas>();
        health = GetComponent<Health>();
        inventory = GetComponent<Inventory>();

        cam.enabled = false;
        canvas.enabled = false;

        AssaultRifle rifle = new AssaultRifle();
        rifle.bulletPrefab = bulletPrefab;
        rifle.bulletSpawn = bulletSpawn;

        inventory.weapon0 = rifle;
    }

    public override void OnStartLocalPlayer()
    {
        cam.enabled = true;
        canvas.enabled = true;
        GetComponent<Renderer>().material.color = Color.blue;
        spawnPoints = FindObjectsOfType<NetworkStartPosition>();               
    }
	
    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer)
            return;
        switch (inventory.GetEquipped().fireType) {
            case EnumFireType.SINGLE_SHOT:
                if (Input.GetButtonDown("Fire1")) CmdFire();
                break;
            case EnumFireType.SEMI_AUTO:
                if (Input.GetButtonDown("Fire1")) CmdFire();
                break;
            case EnumFireType.AUTO:
                if (Input.GetButton("Fire1")) CmdFire();
                break;
        }
    }

    public void TakeDamage(int amount) {
        if (isServer)
            health.TakeDamage(amount); 
    }

    [ClientRpc]
    public void RpcRespawn() {
        if (!isLocalPlayer)
            return;
        Vector3 spawnPoint = Vector3.zero;

        if (spawnPoints != null && spawnPoints.Length > 0) {
            //Shuffle the spawnpoint array so that we can have a random one.
            //Knuth shuffle algorithm, posted by harvesteR on unity forums.
            for (int t = 0; t < spawnPoints.Length; t++ ) {
                NetworkStartPosition tmp = spawnPoints[t];
                int r = Random.Range(t, spawnPoints.Length);
                spawnPoints[t] = spawnPoints[r];
                spawnPoints[r] = tmp;
            }

            foreach (NetworkStartPosition point in spawnPoints) {
                if (CheckSpawnPoint(point)) {
                    spawnPoint = point.transform.position;
                    break;
                }
            }
        }
        transform.position = spawnPoint;
    }

    private bool CheckSpawnPoint (NetworkStartPosition point) {
        foreach (Collider coll in Physics.OverlapSphere(point.transform.position, 5f)) {
            if (coll.tag == "Player") {
                return false;            
            }
        }
        return true;
    }

    [Command]
    private void CmdFire() {
        Weapon weapon = inventory.GetEquipped();

        Debug.Log("Time: " + Time.time);
        Debug.Log("Next time: " + weapon.nextFireTime);

        if (Time.time < weapon.nextFireTime)
            return;

        weapon.nextFireTime = Time.time + weapon.fireCooldown;
        Debug.Log("New next time: " + weapon.nextFireTime);

        Debug.Log("Weapon: " + weapon.type);
        weapon.Fire();
        inventory.UpdateEquipped(weapon, inventory.equippedWeapon);
    }
}
