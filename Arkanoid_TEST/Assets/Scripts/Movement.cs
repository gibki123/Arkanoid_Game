using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    private Vector3 playerPosition = new Vector3 (0,-5,0);

    [SerializeField]
    [Range(0,100)]
    float speed;
    float posx;
	// Use this for initialization
	void Start () {
	}

	
	// Update is called once per frame
	void Update ()
    {
       posx = (Input.GetAxis("Horizontal") * Time.deltaTime * speed) + transform.position.x;
       playerPosition = new Vector3(Mathf.Clamp(posx,-8.75f,8.75f),-5,0);
       transform.position = playerPosition;
    }

}
