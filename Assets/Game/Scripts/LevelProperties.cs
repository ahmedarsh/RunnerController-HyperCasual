using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelProperties : MonoBehaviour
{ 
    [Header("Runner Stuff")]
   public List<Transform> path =new List<Transform>();
   public bool isRun ;
   
   public List<ItemSpawner> itemSpawners=new List<ItemSpawner>();
   public int amountToSpawninLevel=10;
  
    public float pMax, pMin;
   
   public Transform playerPos;
   public List<GameObject> cakeItems = new List<GameObject>();
   
  
   public GameObject confettie;
   
   


    void Start ()
    {
        List<ItemSpawner> tems = new List<ItemSpawner>();
        foreach (var obj in itemSpawners)
        {
            tems.Add(obj);
        }
        if(tems.Count>0)
        {
            for (int i = 0; i < amountToSpawninLevel; i++)
            {
                var num = Random.Range(0, tems.Count);
                tems[num].gameObject.SetActive(true);
                tems.RemoveAt(num);
            }
            
        }

        LevelManager.Instance.Player.transform.position = playerPos.position;
        LevelManager.Instance.Player.transform.rotation = playerPos.rotation;
        LevelManager.Instance.Player.gameObject.SetActive(true);

        
       Invoke("changeMps",0.5f);

    }


   
}



