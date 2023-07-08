// @author Mohammad T. Sarker
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocks : MonoBehaviour {

	// Use this for initialization
	float speed = 2.0f;
	private Rigidbody rb;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.velocity = transform.up*speed*-1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.up * speed);
		checkBoundries ();
	}

	void checkBoundries(){
		if (transform.localPosition.y > 13.0f || transform.localPosition.y < -13.0f) {
			Destroy (gameObject);

		}
		if (transform.localPosition.x > 13.0f || transform.localPosition.x < -13.0f) {
			Destroy (gameObject);
		}
	}


}
