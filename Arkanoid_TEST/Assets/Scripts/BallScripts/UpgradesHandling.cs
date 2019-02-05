using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesHandling : MonoBehaviour
{
    private GameObject obj;

    private GameObject particle;

    public static UpgradesHandling Instance;

    public static bool forceUpgrade;
    public static bool bonusWallUpgrade; //TODO Add Another Upgrades 
    public static bool bonusBallsUpgrade; //

    private bool paddleCollided;

    private void Awake()
    {
        Instance = this;
        particle = gameObject.transform.GetChild(0).gameObject;
        particle.SetActive(false);
        obj = GameObject.FindGameObjectWithTag("MainCamera");
        paddleCollided = false;
        forceUpgrade = false;
        bonusBallsUpgrade = false;
        bonusBallsUpgrade = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "racket")
        {
            if (forceUpgrade == false && paddleCollided == true)
            {
                ForceUpgradeEnd();
            }
            if (forceUpgrade == true)
            {
                ForceUpgrade();
                forceUpgrade = false;
                paddleCollided = true;
                collision.transform.GetComponent<Renderer>().material.color = Color.white;
            }
        }  
    }

    public void ForceUpgrade()
    {
        particle.SetActive(true);
        foreach (var item in BlockSpawning.spawnedBlocks)
        {
            item.GetComponent<Collider>().isTrigger = true;
            Debug.Log(item.name);
        }
        obj.GetComponent<CameraShake>().enabled = true;
    }

    public void ForceUpgradeEnd()
    {
        foreach (var item in BlockSpawning.spawnedBlocks)
        {
            item.GetComponent<Collider>().isTrigger = false;
            Debug.Log(item.name);
        }
        obj.GetComponent<CameraShake>().enabled = false;
        particle.SetActive(false);
    }

    public void AllUpgradesEnd()
    {
        ForceUpgradeEnd();
    }



}
