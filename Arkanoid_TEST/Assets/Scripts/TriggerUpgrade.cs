using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerUpgrade : MonoBehaviour
{
    public static bool forceUpgrade;

    private void Start()
    {
        forceUpgrade = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "upgrade")
        {
            Pooling.Instance.DisableFromPool(other.gameObject);
            forceUpgrade = true;
        }
        
    }
}
