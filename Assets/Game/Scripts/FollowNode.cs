
using System;


using DG.Tweening;

using UnityEngine;



public class FollowNode : MonoBehaviour
{

    public Transform target;

    public Rigidbody rigidbody;
    private NodeContainer _nodeContainer;

    
    public bool isBaseNode;
    

    public GameObject _personModel;


    public bool isActiveNode = true;
    
    public Transform raycastPos;

    public LayerMask layerMask;
    
    public bool isTrigger;
    
    

    void Start()
    {
        _nodeContainer = LevelManager.Instance.Player.nodeContainer;


        if (!rigidbody)
        {
            rigidbody = GetComponent<Rigidbody>();
        }
        
    }

    

    private void LateUpdate()
    {
        if (LevelManager.Instance.levelProperties.isRun)
        {
            if (!isBaseNode)
            {
                if (_nodeContainer.isfollowNode)
                {
                    var val = new Vector3(target.position.x, CheckDistanceOverGround(), target.position.z);

                    var targetP= Vector3.Lerp(transform.position, val,
                        _nodeContainer.lerpTime * Time.deltaTime);
                    targetP.z = target.transform.position.z + _nodeContainer.offSet.z;
                    transform.position = targetP;



                }

            }
            else
            {
                var val = new Vector3(transform.position.x, CheckDistanceOverGround(), transform.position.z);
                transform.position = Vector3.Lerp(transform.position, val,
                    _nodeContainer.lerpTime * Time.deltaTime);

            }
        }
    }


    public void BounceSpacific()
    {
        isActiveNode = false;

        var _initvalue = _personModel.transform.localPosition.y;

        var valueup = _initvalue + 0.75f;

        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(_personModel.transform.DOLocalMoveY(valueup, 0.2f));
        mySequence.Append(_personModel.transform.DOLocalMoveY(_initvalue, 0.2f));

        mySequence.OnComplete((() =>
        {
            isActiveNode = true;
            _personModel.transform.localPosition = Vector3.zero;

        }));
    }

    

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.layer == 10)
        {
            if (isActiveNode)
            {
                var pickupItem = col.GetComponent<PicupItems>();
                StartCoroutine(LevelManager.Instance.Player.nodeContainer.AddNodes(pickupItem));
            }
        }
        
        if (col.gameObject.layer == 7)
        {
            if (!isBaseNode)
            {
                int index = LevelManager.Instance.Player.nodeContainer.AllNodes.IndexOf(this);

                if (index < LevelManager.Instance.Player.nodeContainer.AllNodes.Count - 1)
                {
                    for (int i = LevelManager.Instance.Player.nodeContainer.AllNodes.Count - 1; i > index; i--)
                    {

                        LevelManager.Instance.Player.nodeContainer.AllNodes[i].gameObject.SetActive(false);
                        LevelManager.Instance.Player.nodeContainer.AllNodes.RemoveAt(i);
                    }
                }
                else
                {
                    LevelManager.Instance.Player.nodeContainer.AllNodes.Remove(this);
                    gameObject.SetActive(false);
                }

                LevelManager.Instance.Player.speed = 0;
                LevelManager.Instance.Player.transform
                    .DOMoveZ(LevelManager.Instance.Player.transform.position.z - LevelManager.Instance.Player.backOffse,
                        1f).OnComplete((() =>
                    {
                        var rspeed = PlayerPrefs.GetFloat("RSpeed");
                        LevelManager.Instance.Player.speed = rspeed;
                    }));
                Vibration.VibrateMedium();
            }

        }
        if (col.gameObject.layer == 17)
        {
            if (!isBaseNode)
            {
                int index = LevelManager.Instance.Player.nodeContainer.AllNodes.IndexOf(this);

                if (index < LevelManager.Instance.Player.nodeContainer.AllNodes.Count - 1)
                {
                    for (int i = LevelManager.Instance.Player.nodeContainer.AllNodes.Count - 1; i > index; i--)
                    {
                        LevelManager.Instance.Player.nodeContainer.AllNodes[i].gameObject.SetActive(false);
                      
                        LevelManager.Instance.Player.nodeContainer.AllNodes.RemoveAt(i);
                        print(name);
                    }
                }
                else
                {

                    LevelManager.Instance.Player.nodeContainer.AllNodes.Remove(this);
                    gameObject.SetActive(false);
          
                }
                
                Vibration.VibrateMedium();
            }

        }
        else if (col.gameObject.layer == 13)
        {
            if (!isEnd)
            {
                LevelManager.Instance.stopMovment = true;
                LevelManager.Instance.Player.mPlayer.transform.DOLocalMoveX(0, 0.5f);
                isEnd = true;
                    LevelManager.Instance.levelProperties.confettie.SetActive(true);
                    LevelManager.Instance.ChangeCam(1);
                    
                    LevelManager.Instance.Player.speed =20f; ;
                   
            }
        }

    }


private bool isEnd = false;
    private int date,engage,singl= 0;




    float CheckDistanceOverGround()
    {
  
        RaycastHit hit;
  
        if (Physics.Raycast(raycastPos.position, raycastPos.forward, out hit, 10,layerMask))
        {
            Vector3 targetLocation = hit.point;

            targetLocation = new Vector3(transform.position.x, targetLocation.y+_nodeContainer.rOffest, transform.position.z);
        
             return targetLocation.y;
      
        }
       
        return transform.position.y;
        

    }

}
