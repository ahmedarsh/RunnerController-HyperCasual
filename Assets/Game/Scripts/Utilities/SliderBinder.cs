using System;
using UnityEngine;
using UnityEngine.UI;

public class SliderBinder : MonoBehaviour
{
    Slider slider;
    public Text nameText, valueText;
    Action<float> changeValue;

    /// <summary>
    /// Initialize slider
    /// </summary>
    /// <param name="name">name of slider</param>
    /// <param name="initValue">initial value</param>
    /// <param name="range">min max value of slider</param>
    /// <param name="changeValue">Action when value change occur</param>
    public void Init(string name, float initValue, Vector2 range, Action<float> changeValue)
    {
        slider = GetComponentInChildren<Slider>();
        this.changeValue = changeValue;
        nameText.text = name;
        valueText.text = initValue.ToString("F1");
        slider.minValue = range.x;
        slider.maxValue = range.y;
        slider.value = initValue;
    }

    /// <summary>
    /// Calls on Value Change in Unity
    /// </summary>
    /// <param name="value">given value</param>
    public void ValueChange(float value)
    {
        valueText.text = value.ToString("" + "00.00");
        changeValue?.Invoke(value);
    }

    /// <summary>
    /// Update slider by given value
    /// </summary>
    /// <param name="value">value to write</param>
    public void UpdateGivenValue(float value)
    {
        slider.value = value;
        valueText.text = value.ToString("" + "00.00");
        changeValue?.Invoke(value);
    }
}