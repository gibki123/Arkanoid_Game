using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawning : MonoBehaviour {

    [SerializeField]
    Vector3 vec;

    // Use this for initialization
	void Start ()
    {
        for(int i = 0;i<13; i++)
        {          
            for(int j = 0; j<5;j++)
            {
                Pooling.Instance.SpawnFromPool("block", vec);
                vec.y -= 0.5f;       
            }
            vec.y = 5.5f;
            vec.x += 1.5f;
        }
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
