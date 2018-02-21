using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	
    void OnCollisionEnter (Collision collision) {
        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
        if (pc != null)
            pc.TakeDamage(10);

	Destroy(gameObject);
    }
}
