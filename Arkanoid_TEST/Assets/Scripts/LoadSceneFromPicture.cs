using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneFromPicture : MonoBehaviour
{
    private static Texture2D tex;
    private Vector3 firstBlockPosition = new Vector3(-9, 5.5f, 0);
    private Color pixelColor;
    private List<GameObject> blockList;

    public static LoadSceneFromPicture Instance;
    public static int blockQuantity;

    private void Awake()
    {
        blockQuantity = 0;
        blockList = new List<GameObject>();
        Instance = this;
        tex = Resources.Load<Texture2D>("Level1");
    }

    public List<GameObject> LoadLevel()
    {
        Vector3 currentSpawnPosition = firstBlockPosition; 
        for(int i = 0;i<tex.width;i++)
        {
            for(int j = 0;j<tex.height;j++)
            {
                
                pixelColor = tex.GetPixel(i, j);
                if(pixelColor.a != 0)
                {
                    if(Color.black == pixelColor)
                    {
                        blockList.Add(Pooling.Instance.SpawnFromPool("block", currentSpawnPosition));
                        blockQuantity++;
                    }
                    currentSpawnPosition.y -= 0.5f; 
                }                                             
            }
            if(pixelColor.a != 0)
            {
            currentSpawnPosition.y = firstBlockPosition.y;
            currentSpawnPosition.x += 1.5f;
            }         
        }
        return blockList;
    }
}
