using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawning : MonoBehaviour {

    private int collisionQunatity;
    private float blocksDistance;
    private float positionX = -9;
    private float positionY = 5.5f;

    [SerializeField]
    private Vector3 vec;

    public List<GameObject> spawnedBlocks;

    private void Awake()
    {
        vec = new Vector3(positionX,positionY, 0);
        spawnedBlocks = new List<GameObject>();
        collisionQunatity = 10;
        blocksDistance = -0.5f;
    }

	private void Start ()
    {       
        for(int i = 0;i<13; i++)
        {          
            for(int j = 0; j<5;j++)
            {
                spawnedBlocks.Add(Pooling.Instance.SpawnFromPool("block", vec));
                vec.y -= 0.5f;       
            }
            vec.y = 5.5f;
            vec.x += 1.5f;
        }
        vec = new Vector3(positionX, positionY, 0);
    }

	private void Update () {

        if(BallCollision.collisionCounter == collisionQunatity)
        {
            MoveDownBlocks();
            SpawnNewBlocks();
            BallCollision.collisionCounter = 0;
        }
        if(BallCollision.powerfulPaddleCollision == true)
        {
            foreach(var item in spawnedBlocks)
            {
                item.GetComponent<Collider>().isTrigger = true;
            }
        }
        if (BallCollision.powerfulPaddleCollision == false)
        {
            foreach (var item in spawnedBlocks)
            {
                item.GetComponent<Collider>().isTrigger = false;
            }
        }

    }
    
    public void MoveDownBlocks()
    {
        foreach(var item in spawnedBlocks)
        {
            Vector3 templ = item.transform.position;
            templ.y += blocksDistance;
            item.transform.position = templ;
        }
    }

    public void SpawnNewBlocks()
    {
        for(int i = 0;i<13; i++)
        {
            spawnedBlocks.Add(Pooling.Instance.SpawnFromPool("block", vec));
            vec.x += 1.5f;
        }
        vec = new Vector3(positionX, positionY, 0);
    }
}
