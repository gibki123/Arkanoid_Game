using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerForceUpgrade : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.y < -10)
        {
            Pooling.Instance.DisableFromPool(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "racket")
        {
            Pooling.Instance.DisableFromPool(gameObject);
            UpgradesHandling.forceUpgrade = true;
            other.gameObject.GetComponent<Renderer>().material.color = Color.red;
            UpgradesHandling.paddleCollided = false;
            if(transform.position.y < -10)
            {
                Pooling.Instance.DisableFromPool(gameObject);
            }
        }
    }
}
