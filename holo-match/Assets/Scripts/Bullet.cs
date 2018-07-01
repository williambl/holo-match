using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public PlayerController playerFired;

    Rigidbody rigid;
    float offset = 0.021f; //Needed so that the raycast will not just hit the bullet itself

    void Start () {
        rigid = GetComponent<Rigidbody>();
    }
    void FixedUpdate () {
        //Allows us to move fast while still hitting things
        Ray ray = new Ray(transform.position+transform.forward*offset, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rigid.velocity.magnitude*Time.fixedDeltaTime))
            HitObject(hit.collider.gameObject);
    }
	
    void OnCollisionEnter (Collision collision) {
        HitObject(collision.gameObject);
    }

    void HitObject (GameObject objectHit) {
        if (objectHit.tag == "Bullet")
            return;
        PlayerController pc = objectHit.GetComponent<PlayerController>();
        if (pc != null) {
            if (pc == playerFired)
               return; 
            pc.TakeDamage(10);
        }

	Destroy(gameObject);
    }
}
