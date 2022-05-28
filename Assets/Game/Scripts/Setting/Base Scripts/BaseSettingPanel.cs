using UnityEngine;

public abstract class BaseSettingPanel : MonoBehaviour
{
    public GameObject panel;
    public PanelIDs _panelIDs;

    protected abstract void ResetValues();

    private void OnEnable()
    {
        GameManager.Instance._setting += Setting;
        panel.SetActive(false);
    }

    private void OnDisable()
    {
        GameManager.Instance._setting -= Setting;
    }

    private void Setting(bool value, string settingname)
    {
        GameManager.Instance.settingBG.SetActive(!value);
        if (settingname == _panelIDs.ToString())
            panel.SetActive(value);
    }

    public void OnReset()
    {
        ResetValues();
    }

    public void Close()
    {
        GameManager.Instance._setting?.Invoke(false, _panelIDs.ToString());
    }
}