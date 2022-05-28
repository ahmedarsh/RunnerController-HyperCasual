using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class NodeContainer : MonoBehaviour
{
    public FollowNode baseNode;
    public bool isfollowNode=false;
    public Vector3 offSet;
    public float lerpTime=1.5f;
    public float timeInWave = 0.01f;
    public List<FollowNode> AllNodes = new List<FollowNode>();


    public float rOffest = 0.5f;
    
    public float endScale=1;
    
    private PlayerController _playerController;

    [HideInInspector]public float _initvalue, _newValue;
    
    private bool isText = false;

    private bool _isStopScale = false;
    
    
   

    
    
    private void Start()
    {
        _playerController = LevelManager.Instance.Player;
        _initvalue = baseNode.transform.localScale.x;
        
        _newValue=_initvalue+(_initvalue/5);
        
        AllNodes.Add(baseNode);
    }

    public void DoBounceWave()
    {
        StartCoroutine(BounceCor());
    }

    IEnumerator BounceCor()
    {
       
        yield return new WaitForSeconds(timeInWave);
        
        foreach (var obj in AllNodes)
        {
            if (obj.isActiveNode && obj.isActiveAndEnabled)
            {
                obj.BounceSpacific();
            }
           
            yield return new WaitForSeconds(timeInWave);
        }
    }
    
    
    
     private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 10)
        { 
            var pickupItem = col.GetComponent<PicupItems>();
            
            StartCoroutine(AddNodes(pickupItem));
            
        }
    }


    public IEnumerator AddNodes(PicupItems pickupItem)
    {

        pickupItem.collider.enabled = false;
        
        pickupItem.powerUp.SetActive(true);

        isText = true;

             
        if (AllNodes.Count <= 0)
        { 
            pickupItem.followNode.target = baseNode.transform;
        }
        else
        {
            pickupItem.followNode.target = AllNodes[AllNodes.Count - 1].transform;

        }

        pickupItem.itemSpawner.items.Remove(pickupItem);
               
        AllNodes.Add(pickupItem.followNode);
        pickupItem.followNode.name =  "Hat"+AllNodes.Count;
        pickupItem.followNode.enabled = true;
        
        pickupItem.followNode.isActiveNode = true;
        pickupItem.followNode.gameObject.SetActive(true);

        pickupItem.followNode.transform.parent = null;
               
              
        Vibration.VibrateMedium();
            
        
        yield return new WaitForSeconds(0.01f);
        tempCount=AllNodes.Count-1;
        ScaleNodes();   
        
        yield return new WaitForSeconds(2f);
     
        Destroy(pickupItem.gameObject);
           
    }
    
    private int tempCount;
   
    public void ScaleNodes()
    { 
        
        var tcount = tempCount;
     
       AllNodes[tcount].transform.DOScale(_newValue, 0.05f).OnComplete((() =>
        {
            AllNodes[tcount].transform.DOScale(_initvalue, 0.05f);
               
            tempCount--;
         
            if (tempCount >0)
            {
                ScaleNodes();
            }
            else
            {
                tempCount =  AllNodes.Count-1;
             
            }
                
        }));
    }

 
}
