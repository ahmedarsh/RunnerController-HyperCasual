using System;
using UnityEngine;
using UnityEngine.UI;

public enum PanelIDs
{
    Camera,
    Control,
    Default1,
    Default2,
    Default3,
    Default4,
    Default5
}

public class SettingPanelButton : MonoBehaviour
{
    public GameObject settingBG;
    public PanelIDs panelID = PanelIDs.Control;
    
    public Text valueTxt;
    public Slider valueSlider;


    private void Start()
    {
        if (panelID == PanelIDs.Default1)
        {
            var rspeed =PlayerPrefs.GetFloat("RSpeed");
                
            valueTxt.text = rspeed.ToString("00");
            valueSlider.value = rspeed;
        }
        else if (panelID == PanelIDs.Default2)
        {
            var mpeed =PlayerPrefs.GetFloat("MSpeed");
            valueTxt.text = mpeed.ToString("F3");
                
            valueSlider.value = mpeed;
        }
        else if (panelID == PanelIDs.Default3)
        {
            var mpeed =PlayerPrefs.GetFloat("CGap");
            valueTxt.text = mpeed.ToString("F3");
                
            valueSlider.value = mpeed;
        }  
        else if (panelID == PanelIDs.Default4)
        {
            var mpeed =PlayerPrefs.GetFloat("SSpeed");
            valueTxt.text = mpeed.ToString("F3");
                
            valueSlider.value = mpeed;
        }
    }

    public void OnClick()
    {
        GameManager.Instance.settingBG = settingBG;
        GameManager.Instance._setting?.Invoke(true, panelID.ToString());
    }
    public void RunnerSpeed(float speed)
    {
        PlayerPrefs.SetFloat("RSpeed",speed);
        LevelManager.Instance.Player.speed = speed;
        valueTxt.text = speed.ToString("00");
    }

    public void MovementSpeed(float speed)
    {
        PlayerPrefs.SetFloat("MSpeed",speed);
        LevelManager.Instance.Player.mPlayer.speedModifier = speed;
        valueTxt.text = speed.ToString("F3");
    } 
    public void CharactesGap(float speed)
    {
        PlayerPrefs.SetFloat("CGap",speed);
        LevelManager.Instance.Player.nodeContainer.offSet = new Vector3(LevelManager.Instance.Player.nodeContainer.offSet.x,LevelManager.Instance.Player.nodeContainer.offSet.y,speed);
        valueTxt.text = speed.ToString("F3");
    }
    public void CharactesStreatch(float speed)
    {
        PlayerPrefs.SetFloat("SSpeed",speed);
        print(speed+" Speed ");
        LevelManager.Instance.Player.nodeContainer.lerpTime = speed;
        print(speed+" Speed " +LevelManager.Instance.Player.nodeContainer.lerpTime);
        valueTxt.text = speed.ToString("F3");
    }
}