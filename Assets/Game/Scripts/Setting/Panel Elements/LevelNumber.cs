using UnityEngine;

public class LevelNumber : MonoBehaviour
{
    private Settings settings;

    private void Start()
    {
        settings = GetComponentInParent<Settings>();
    }

    public void OnValueChanged(string value)
    {
        int newLevel = int.Parse(value);
        SaveLoad.Save(SaveKeys._level.Key, newLevel);
        SaveLoad.SaveProgress();
        settings.hasToReload = true;
    }
}