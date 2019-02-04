using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSpawning : MonoBehaviour
{
    public static bool pause;
    private int maxTimeSpawn;
    private int minTimeSpawn;

   private void Start()
    {       
        minTimeSpawn = 10;
        maxTimeSpawn = 20;
        StartCoroutine(SphereSpawn());
    }

    private IEnumerator SphereSpawn()
    {
        while(true)
        {
            int time_for_spawn = Random.Range(minTimeSpawn, maxTimeSpawn);
            yield return new WaitForSeconds(time_for_spawn);
            GameObject obj = Pooling.Instance.SpawnFromPool("upgrade", new Vector3(Random.RandomRange(-9.5f, 9.5f), 14));
            obj.GetComponent<Rigidbody>().AddForce(new Vector3(0, -400));
        }
    }

}
