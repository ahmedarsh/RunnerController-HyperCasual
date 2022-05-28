using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public MovementController mPlayer;
   
     LevelProperties _levelProperties;

    public MeshRenderer platformGround;
    public float speed = 1.0f;
    private float _offset = 1f;
    
    private Transform _target;
   
    public NodeContainer nodeContainer;

    public List<Transform>  cameraPos=new List<Transform>();
    public Transform cameraEndPos,camerEndShelPos;
    public Transform mCamera;
    
    
    public float backOffse;

    public bool isEndSetup;

  
    void Start()
    {
        _levelProperties = LevelManager.Instance.levelProperties;
            
        _target = _levelProperties.path[0];

        if (platformGround)
        { 
            var bound = platformGround.bounds;
            _levelProperties.pMax = bound.max.x - _offset;
            _levelProperties.pMin = bound.min.x + _offset;
        }
        
    }


    public void StartGame()
    {
        _levelProperties.isRun = true;
        nodeContainer.isfollowNode = true;
        Run();
       
    }
    public void Run()
    {
        
    }

    public void Idle()
    {
        
    }
    
    void Update ()
    {
        if (_levelProperties.isRun)
        {
            float step =  speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _target.position, step);
            
            if (Vector3.Distance(transform.position, _target.position) < 0.001f)
            {
                if (_levelProperties.path.Count > 0)
                {
                    _levelProperties.path.RemoveAt(0);
                }
                
                if (_levelProperties.path.Count > 0)
                {
                    _target = _levelProperties.path[0];
                    transform.DOLookAt(_target.position,0.6f);
                }
                else
                {
                    _levelProperties.isRun = false;
                  //  Idle();
                   
                    if (!isEndSetup)
                    {
                        LevelManager.Instance.ChangeCam(2);
                        LevelManager.CompleteCheck();
                    }
                }
            }
        } 
    }


    
}
