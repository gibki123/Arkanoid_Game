using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSpawning : MonoBehaviour
{
    public static bool pause;
    private bool spawningCoroutine;


    private int maxTimeSpawn;
    private int minTimeSpawn;

    private GameObject upgradeObj;

    private enum Upgrades {forceUpgrade = 1,widthUpgrade,stickUpgrade}

   private void Awake()
    {
        spawningCoroutine = false;
        upgradeObj = null;
        minTimeSpawn = 5;
        maxTimeSpawn = 10;
    }

    private void Update()
    {
        if (upgradeObj != null && upgradeObj.transform.position.y <= -10f)
        {
            Pooling.Instance.DisableFromPool(upgradeObj);
        }
        if(BallCollision.firstBallShot == true && spawningCoroutine == true)
        {
            spawningCoroutine = false;
            StopCoroutine(SphereSpawn());            
        }
        if (BallCollision.firstBallShot == false && spawningCoroutine == false)
        {
            spawningCoroutine = true;
            StartCoroutine(SphereSpawn());            
        }
    }

    private IEnumerator SphereSpawn()
    {
        while(true)
        {
            int time_for_spawn = Random.Range(minTimeSpawn, maxTimeSpawn + 1);
            int random = Random.Range(3,4);
            yield return new WaitForSeconds(time_for_spawn);
            Upgrades upgrade = (Upgrades)random;
            switch(upgrade)
            {
                case Upgrades.forceUpgrade:
                    {
                        upgradeObj = Pooling.Instance.SpawnFromPool("forceUpgrade", new Vector3(Random.Range(-9.5f, 9.5f), 14));
                    }
                    break;
                case Upgrades.widthUpgrade:
                    {
                        upgradeObj = Pooling.Instance.SpawnFromPool("widthUpgrade", new Vector3(Random.Range(-9.5f, 9.5f), 14));
                    }
                    break;
                case Upgrades.stickUpgrade:
                    {
                        upgradeObj = Pooling.Instance.SpawnFromPool("stickUpgrade", new Vector3(Random.Range(-9.5f, 9.5f), 14));
                    }
                    break;
            }          
            upgradeObj.GetComponent<Rigidbody>().velocity = new Vector3(0, -10, 0);        
        }
    }

}
