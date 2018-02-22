using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    private Camera cam;
    private Canvas canvas;

    public Health health;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    // Use this for initialization
    void Awake () {
	cam = GetComponentInChildren<Camera>();
        canvas = GetComponentInChildren<Canvas>();
        health = GetComponent<Health>();

        cam.enabled = false;
        canvas.enabled = false;
    }

    public override void OnStartLocalPlayer()
    {
        cam.enabled = true;
        canvas.enabled = true;
        GetComponent<Renderer>().material.color = Color.blue;
    }
	
    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer)
            return;
        if (Input.GetButtonDown("Fire1"))
            CmdFire();
    }

    public void TakeDamage(int amount) {
        if (isServer)
            health.TakeDamage(amount); 
    }

    [ClientRpc]
    public void RpcRespawn() {
        if (!isLocalPlayer)
            return;
        health.health = health.maxHealth;
        transform.position = Vector3.zero;
    } 

    [Command]
    private void CmdFire() {
        GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;
        NetworkServer.Spawn(bullet);

        Destroy(bullet, 2.0f);
    }
}
