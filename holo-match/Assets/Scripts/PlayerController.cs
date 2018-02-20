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
	}
}
