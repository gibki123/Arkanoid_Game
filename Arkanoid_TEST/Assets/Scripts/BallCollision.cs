using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour {

    [SerializeField]
    [Range(0,1000)]
    private float added_force;

    Rigidbody rb;
    private bool firstclick;

    public static int collisionCounter;
	// Use this for initialization
    void Awake ()
    {
        collisionCounter = 0;
        firstclick = true;
        rb = GetComponent<Rigidbody>();
    }
	void Start () {
        
      
	}
	
	// Update is called once per frame
	void Update () {
		if(firstclick && Input.GetButtonDown("Fire1"))
        {         
            transform.parent = null; 
            firstclick = false;
            rb.isKinematic = false;
            rb.AddForce(new Vector3(added_force, added_force / 2, 0));
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "racket")
        {
            collisionCounter++;
            Debug.Log(collisionCounter);
        }
    }
}
