using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSpawning : MonoBehaviour
{
    public static bool pause;

    private int maxTimeSpawn;
    private int minTimeSpawn;

    private GameObject obj;

   private void Start()
    {
        obj = null;
        minTimeSpawn = 10;
        maxTimeSpawn = 20;
        StartCoroutine(SphereSpawn());
    }

    private void Update()
    {
        if (obj != null && obj.transform.position.y <= -10f)
        {
            Pooling.Instance.DisableFromPool(obj);
        }
    }

    private IEnumerator SphereSpawn()
    {
        while(true)
        {
            int time_for_spawn = Random.Range(minTimeSpawn, maxTimeSpawn);
            yield return new WaitForSeconds(time_for_spawn);
            obj = Pooling.Instance.SpawnFromPool("upgrade", new Vector3(Random.Range(-9.5f, 9.5f), 14));
            obj.GetComponent<Rigidbody>().velocity = new Vector3(0, -10, 0);
        }
    }

}
