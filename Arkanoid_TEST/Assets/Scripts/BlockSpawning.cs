using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawning : MonoBehaviour {

    [SerializeField]
    Vector3 vec;

    private int collisionQunatity;

    private List<GameObject> spawnedBlocks;

    private float blocksDistance;

    private GameObject obj;
    // Use this for initialization
    void Awake()
    {
        collisionQunatity = 5;
        blocksDistance = 0.5f;
    }

	void Start ()
    {
        
        for(int i = 0;i<13; i++)
        {          
            for(int j = 0; j<5;j++)
            {
                obj = Pooling.Instance.SpawnFromPool("block", vec);
                if(obj!=null)
                spawnedBlocks.Add(obj);     

                vec.y -= 0.5f;       
            }
            vec.y = 5.5f;
            vec.x += 1.5f;
        }
       
	}
	
	// Update is called once per frame
	void Update () {
        if(BallCollision.collisionCounter == collisionQunatity)
        {
            MoveDownBlocks();

        }
		
	}
    
    void MoveDownBlocks()
    {
        foreach(var item in spawnedBlocks)
        {
            Vector3 templ = item.transform.position;
            templ.y += blocksDistance;
            item.transform.position = templ;
        }
    }
    void SpawnNewBlocks()
    {

    }
}
