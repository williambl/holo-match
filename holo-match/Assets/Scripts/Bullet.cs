using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    Rigidbody rigid;
    Vector3 offset = new Vector3(0, 0, 0.021f); //Needed so that the raycast will not just hit the bullet itself

    void Start () {
        rigid = GetComponent<Rigidbody>();
    }
    void FixedUpdate () {
        //Allows us to move fast while still hitting things
        Ray ray = new Ray(transform.position+offset, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(transform.position+offset, transform.forward, out hit, rigid.velocity.magnitude*Time.fixedDeltaTime))
            HitObject(hit.collider.gameObject);
    }
	
    void OnCollisionEnter (Collision collision) {
        HitObject(collision.gameObject);
    }

    void HitObject (GameObject objectHit) {
        PlayerController pc = objectHit.GetComponent<PlayerController>();
        if (pc != null)
            pc.TakeDamage(10);

	Destroy(gameObject);
    }
}
