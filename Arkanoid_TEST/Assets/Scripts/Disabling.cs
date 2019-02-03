using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disabling : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        Pooling.Instance.DisableFromPool(gameObject);
    }
}
