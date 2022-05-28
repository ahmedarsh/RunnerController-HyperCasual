using System;

using DG.Tweening;
using UnityEngine;
public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform Levels;
    public PlayerController Player;
    
    [Range(1, 30), SerializeField] private int startLevel = 1;
    int currentLevel = 1;
    

    public static LevelManager Instance;
    [HideInInspector] public LevelProperties levelProperties;
    
    
    public static Action CompleteCheck;

    public bool tapAndHold,stopMovment;


    public bool endCol;
    
    public FollowCameraPro fcp;
    
  

    void Awake()
    {
        if (!Instance) Instance = this;
        Time.timeScale = 1f;
        
        Vibration.Init();

        currentLevel = (startLevel - 1) % Levels.childCount;
   

        ActivateLevel(currentLevel);

        GameManager.Instance.isPlaying = true;
        
        
        currentLevel = (startLevel - 1) % Levels.childCount;
 
        ActivateLevel(currentLevel);

        GameManager.Instance.isPlaying = true;

        if (PlayerPrefs.GetInt("firsTime") == 0)
        {
            PlayerPrefs.SetInt("firsTime",1);
            firstTime();
        }
        var rspeed =PlayerPrefs.GetFloat("RSpeed");
        Player.speed = rspeed;
        var mpeed =PlayerPrefs.GetFloat("MSpeed");
        Player.mPlayer.speedModifier = mpeed;  
        var Sspeed =PlayerPrefs.GetFloat("SSpeed");
        Player.nodeContainer.lerpTime = Sspeed;  
        var cGap =PlayerPrefs.GetFloat("CGap");
        Player.nodeContainer.offSet = new Vector3(Player.nodeContainer.offSet.x,Player.nodeContainer.offSet.y,cGap);

        
       fcp.PosOffset = new Vector3(fcp.PosOffset.x,fcp.PosOffset.y,-15f);
        fcp.lookSpeed = 30f;   
        Invoke("ChangeSpeed",1f);
    }

    private bool isCamBack = false;
 

    void ChangeSpeed()
    {
    
        fcp.lookSpeed = 0.1f;    
    }
    public void StartGame()
    {
        Player.StartGame();
    }
    void ActivateLevel(int levelNumber)
    {
        for (int x = 0; x < Levels.childCount; x++)
        {
            Levels.GetChild(x).gameObject.SetActive(false);
        }

        Levels.GetChild(levelNumber).gameObject.SetActive(true);
        levelProperties = Levels.GetChild(levelNumber).GetComponent<LevelProperties>();
    }
    
    
    public void firstTime()
    {
        print(Player);
        print(Player.nodeContainer);
        PlayerPrefs.SetFloat("RSpeed",Player.speed);
        PlayerPrefs.SetFloat("MSpeed",Player.mPlayer.speedModifier); 
        PlayerPrefs.SetFloat("CGap",Player.nodeContainer.offSet.z);
        PlayerPrefs.SetFloat("SSpeed",Player.nodeContainer.lerpTime);
    }
    
        private int index;
        public void ChangeCam(int index)
        {
          
            print("cam Change");
            switch (index)
            {
                case 0:
                    Player.mCamera.transform.DOLocalMove(Player.cameraPos[0].localPosition, 0.5f);
                    Player.mCamera.transform.DOLocalRotateQuaternion(Player.cameraPos[0].localRotation, 0.5f);
                    print("inde x");
                    break;
                case 1:
                    Player.mCamera.transform.DOLocalMove(Player.cameraPos[1].localPosition, 0.5f);
                    Player.mCamera.transform.DOLocalRotateQuaternion(Player.cameraPos[1].localRotation, 0.5f);
                    break;
                case 2:
                    Player.mCamera.transform.DOLocalMove(Player.cameraPos[2].localPosition, 0.5f);
                    Player.mCamera.transform.DOLocalRotateQuaternion(Player.cameraPos[2].localRotation, 0.5f);
                    break;
                case 3:
                   
                    Player.mCamera.transform.DOLocalMove(Player.cameraEndPos.localPosition, 0.5f);
                    Player.mCamera.transform.DOLocalRotateQuaternion(Player.cameraEndPos.localRotation, 0.5f);
                    break;
                case 4:
                    Player.mCamera.transform.DOLocalMove(Player.camerEndShelPos.localPosition, 0.5f);
                    Player.mCamera.transform.DOLocalRotateQuaternion(Player.camerEndShelPos.localRotation, 0.5f);
                    //index = 1;
                    break;
            }
            index++;
            if (index > 2)
            {
                index = 0;
            }
            
        }

        private void OnEnable()
        {
            CompleteCheck += LevelCompleteCheck;
        }

        private void OnDisable()
        {
            CompleteCheck -= LevelCompleteCheck;
        }
        
        void LevelCompleteCheck()
        {
//            ChangeCam(1);
            DoCompleteLocket();
        }
        
       public void DoCompleteLocket()
       {
        print("Level Complete ");
       }

       private float _waitTime = 0.13f;
       
}