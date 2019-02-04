using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disabling : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        Pooling.Instance.DisableFromPool(gameObject);
    }
}
