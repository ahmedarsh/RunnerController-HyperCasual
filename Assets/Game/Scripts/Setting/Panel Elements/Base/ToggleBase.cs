using UnityEngine;
using UnityEngine.UI;


public abstract class ButtonToggleBase : MonoBehaviour
{
    public bool isOn = true;
    [SerializeField] protected Color OnColor, OffColor;

    [SerializeField] protected Text _text;

    protected abstract void OnOnSelected();
    protected abstract void OnOffSelected();

    public void Toggle()
    {
        if (isOn)
        {
            Off();
        }
        else
        {
            On();
        }

        isOn = !isOn;
    }

    void On()
    {
       // image.color = OnColor;
        _text.text = "on".ToUpper();
        OnOnSelected();
    }

    void Off()
    {
       // image.color = OffColor;
        _text.text = "off".ToUpper();
        OnOffSelected();
    }
}