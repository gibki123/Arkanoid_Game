using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWidthUpgrade : MonoBehaviour
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
            UpgradesHandling.widthUpgrade = true;
            other.gameObject.GetComponent<Renderer>().material.color = new Color32(0, 130, 44, 255);
            UpgradesHandling.paddleCollided = false;
        }
    }
}
