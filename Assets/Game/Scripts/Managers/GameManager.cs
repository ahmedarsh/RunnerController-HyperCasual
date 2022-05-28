using UnityEngine;

public class GameManager
{
    static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GameManager();

            return instance;
        }
    }

    public bool isPlaying = false;
    public GameObject settingBG;

    public delegate void BoolDelegate(bool value);

    public delegate void Vector3Delegate(Vector3 value);

    public delegate void SettingDelegate(bool value, string settingName);

    public SettingDelegate _setting;
}