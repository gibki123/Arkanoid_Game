using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    private Vector3 playerPosition;

    [SerializeField]
    [Range(0,100)]
    private float speed;

    private float posx;

	void Update ()
    {
       posx = (Input.GetAxis("Horizontal") * Time.deltaTime * speed) + transform.position.x;
       playerPosition = new Vector3(Mathf.Clamp(posx,-8.75f,8.75f),-6.5f,0);
       transform.position = playerPosition;
    }
}
