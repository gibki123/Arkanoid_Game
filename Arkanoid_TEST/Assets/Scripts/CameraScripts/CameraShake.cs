using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
   // private Vector3 initialPosition;

    [SerializeField]
    [Range(1, 10)]
    private int magnitude;

    private void Update()
    {
        float x = Random.Range(-0.25f, 0.25f) * magnitude;
        float y = Random.Range(-0.25f, 0.25f) * magnitude;
        transform.position = new Vector3(x, y, -10);
    }
    private void OnDisable()
    {
        transform.position = new Vector3(0, 0, -10);        
    }

}
