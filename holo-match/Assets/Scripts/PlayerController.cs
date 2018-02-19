using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

        private Camera cam;

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

            var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
            var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

            transform.Rotate(0, x, 0);
            transform.Translate(0, 0, z);        
	}
}
