using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

        private Camera cam;

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
                Fire();
	}

        private void Fire() {
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 6, ForceMode.VelocityChange);
            Destroy(bullet, 2.0f);
        }
}
