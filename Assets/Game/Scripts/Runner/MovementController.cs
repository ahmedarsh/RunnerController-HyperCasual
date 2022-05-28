
using UnityEngine;

public class MovementController : MonoBehaviour
{
    PlayerController _playerController;
    private LevelProperties _levelProperties;
    [Header("Player Movement")]
    public float speedModifier = -0.04f;
    private float _offset = 0.01f;
    private bool isFirstRun;

    private void Start()
    {
        MTouch.TapBegan = OnTapBegan;
        MTouch.TapHold = OnTapHold;
        MTouch.TapEnd=OnTapEnd;
        _playerController = LevelManager.Instance.Player;
        _levelProperties = LevelManager.Instance.levelProperties;
        

        targetPos = transform.localPosition;
    }

    private void OnTapBegan(Vector2 pos)
    {
      
    }

    private Vector3 smoothDemp,targetPos;
    private void OnTapHold(Vector2 pos)
    {
        if (!LevelManager.Instance.stopMovment)
        {
            if (isFirstRun && LevelManager.Instance.tapAndHold && !_levelProperties.isRun)
            {
                _levelProperties.isRun = true;
                _playerController.Run();
            }

            if (_levelProperties.isRun)
            {
             
             targetPos= new Vector3(targetPos.x + pos.x * speedModifier
                        , targetPos.y
                        , targetPos.z);
                    
                  

                targetPos.x = Mathf.Clamp(targetPos.x, _levelProperties.pMin ,  _levelProperties.pMax  );
               

                if (!isFirstRun)
                {
                    isFirstRun = true;
                }
            }


            var val = Mathf.Abs(pos.x);
            if (val > 30)
            {
                if (val > 200)
                {
                    _playerController.nodeContainer.lerpTime = 25;
                }
               
            }

            else if (val < 30)
            {
                var Sspeed =PlayerPrefs.GetFloat("SSpeed");
                _playerController.nodeContainer.lerpTime = Sspeed;
                    ;
            }
        }
    }

    public float smoothTime = 0.1f;

    private void Update()
    {
        if (!LevelManager.Instance.stopMovment)
        {

            transform.localPosition =
                Vector3.SmoothDamp(transform.localPosition, targetPos, ref smoothDemp, smoothTime);
        }
    }

    private void OnTapEnd(Vector2 pos)
    {
        if (!LevelManager.Instance.endCol)
        {
            if (!LevelManager.Instance.stopMovment)
            {
                if (isFirstRun && LevelManager.Instance.tapAndHold && _levelProperties.isRun)
                {
                    _levelProperties.isRun = false;
                    _playerController.Idle();
                    _playerController.nodeContainer.DoBounceWave();
                }
            }
        }
    }

  

    private void OnDisable()
    {
        MTouch.TapBegan-= OnTapBegan;
        MTouch.TapHold-= OnTapHold;
        MTouch.TapEnd-=OnTapEnd;
    }
}