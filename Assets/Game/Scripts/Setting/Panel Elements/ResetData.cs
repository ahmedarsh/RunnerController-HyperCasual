using UnityEngine;

public class ResetData : MonoBehaviour
{
    private Settings settings;

    private void Start()
    {
        settings = GetComponentInParent<Settings>();
    }

    public void ResetFullGame()
    { 
        PlayerPrefs.DeleteAll();
        SaveLoad.DeleteProgress();
        SaveLoad.Save(SaveKeys._level.Key, 1);
        SaveLoad.SaveProgress();
        settings.hasToReload = true;
        
    }
}