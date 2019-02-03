using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour {

    public static Pooling Instance;
    [System.Serializable]
    class Pool
    {
        public GameObject prefab;
        public string tag;
        public int size;
    }

    [SerializeField]
    private List<Pool> pools;

    [SerializeField]
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    public void Awake()
    {
        Instance = this;
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objects = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objects.Enqueue(obj);
                //Debug.Log("Spawn");
            }
            poolDictionary.Add(pool.tag, objects);
        }
    }
    //void start()
    //{
    //    poolDictionary = new Dictionary<string, Queue<GameObject>>();
    //    foreach (Pool pool in pools)
    //    {
    //        Queue<GameObject> objects = new Queue<GameObject>();

    //        for (int i = 0; i < pool.size; i++)
    //        {
    //            GameObject obj = Instantiate(pool.prefab);
    //            obj.SetActive(false);
    //            objects.Enqueue(obj);
    //            Debug.Log("Spawn");
    //        }
    //        poolDictionary.Add(pool.tag, objects);
    //    }
    //}

    public GameObject SpawnFromPool(string tag, Vector3 position)
    {
        if (poolDictionary.ContainsKey(tag))
        {
            if(poolDictionary.Count != 0)
            {
                GameObject spawningObject = poolDictionary[tag].Dequeue();
                spawningObject.SetActive(true);
                spawningObject.transform.position = position;
                return spawningObject;
            }
            else
            {
                Debug.LogWarning("poolDistionary contains 0 objects");
                return null;
            } 
        } 
        else
        {
            Debug.LogWarning("Pool with this tag:" + tag + "doesn't exists");
            return null;
        }
        
    }
    

    

    public void DisableFromPool(GameObject obj)
    {
        obj.SetActive(false);
        poolDictionary[obj.transform.tag].Enqueue(obj);
    }








}
