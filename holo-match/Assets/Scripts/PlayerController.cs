using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    private Camera cam;

    public Player player = new Player();

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    // Use this for initialization
    void Awake () {
	cam = GetComponentInChildren<Camera>();
        cam.enabled = false;
    }

    public override void OnStartLocalPlayer()
    {
        cam.enabled = true;
        GetComponent<Renderer>().material.color = Color.blue;
    }
	
    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer)
            return;
        if (Input.GetButtonDown("Fire1"))
            CmdFire();
    }

    [Command]
    private void CmdFire() {
        GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;
        NetworkServer.Spawn(bullet);

        Destroy(bullet, 2.0f);
    }
}
