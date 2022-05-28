using System.Collections.Generic;

using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    

    public List<Transform> itemsPos=new List<Transform>();

    public List<PicupItems> items = new List<PicupItems>();
    
    GameObject obj;
    private void Start()
    {
        int turns = itemsPos.Count;

        for (int i = 0; i < turns; i++)
        {

            if (itemsPos.Count > 0)
            {
               
                var index = Random.Range(0, LevelManager.Instance.levelProperties.cakeItems.Count);
                obj = Instantiate(LevelManager.Instance.levelProperties.cakeItems[index]);

             

                int numPos = Random.Range(0, itemsPos.Count);
                
                obj.transform.position = itemsPos[numPos].position;
                obj.transform.rotation = itemsPos[numPos].rotation;
                
                obj.transform.parent = transform;

                var pI = obj.GetComponent<PicupItems>();
                if (pI)
                {
                    items.Add(pI);
                    pI.itemSpawner = this;
                }
               
                itemsPos.RemoveAt(numPos);

            }
           
        }
    }
    
}
