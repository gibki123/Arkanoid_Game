using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour {

    Rigidbody rd;

	// Use this for initialization
	void Start () {
        rd = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "racket" )
        rd.velocity = new Vector3(0, 8, 0);
        if(collision.transform.tag == "block" )
        rd.velocity = new Vector3(0, -8, 0);
    }
}
