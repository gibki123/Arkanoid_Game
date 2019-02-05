using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerUpgrade : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "racket")
        {
            Pooling.Instance.DisableFromPool(gameObject);
            UpgradesHandling.forceUpgrade = true;
            other.gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
