using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    Vector3 vector;

    [SerializeField]
    [Range(0,100)]
    float speed;
    Rigidbody rig;
	// Use this for initialization
	void Start () {
        rig = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKey("left"))            
            gameObject.transform.position += Vector3.left * speed * Time.deltaTime;
        if (Input.GetKey("right"))
            gameObject.transform.position += Vector3.right * speed * Time.deltaTime;
    }

}
