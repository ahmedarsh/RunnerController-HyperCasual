using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class CameraSlider
{
    [MinMaxSlider(-360, 360, true)] public Vector2 range;

    public SliderBinder slider;

    public void Init(string name, float startValue, Action<float> onChange)
    {
        slider.Init(name, startValue, range, onChange);
    }
}

public class CameraSetting : BaseSettingPanel
{
    [InfoBox("The state going to change by the setting"), SerializeField]
    private BaseCameraState cameraState;

    [Header("Default Values"), SerializeField]
    private Vector3 defaultOffset;

    [SerializeField] private Vector3 defaultRotation;

    [Header("Positions"), SerializeField] private CameraSlider PositionX;
    [SerializeField] private CameraSlider PositionY, PositionZ;
    [Header("Rotation"), SerializeField] private CameraSlider RotationX;
    [SerializeField] private CameraSlider RotationY;

    private Vector3 resetOffset, resetRotation;

    private SaveKeys.Keys<Vector3> _defaultOffset, _defaultRotation;

    private void Start()
    {
        _defaultOffset = new SaveKeys.Keys<Vector3>(string.Concat(_panelIDs.ToString(), "_defaultOffset"), Vector3.zero);
        _defaultRotation = new SaveKeys.Keys<Vector3>(string.Concat(_panelIDs.ToString(), "_defaultRotation"), Vector3.zero);
        
        resetOffset = defaultOffset;
        resetRotation = defaultRotation;
        
        if (SaveLoad.Fetch<Vector3>(_defaultOffset) == Vector3.zero)
        {
            SaveLoad.Save<Vector3>(_defaultOffset.Key, defaultOffset);
            SaveLoad.Save<Vector3>(_defaultRotation.Key, defaultRotation);
            SaveLoad.SaveProgress();
        }
        else
        {
            defaultOffset = SaveLoad.Fetch<Vector3>(_defaultOffset);
            defaultRotation = SaveLoad.Fetch<Vector3>(_defaultRotation);
        }

        InitializingSlidersWithCallbacks();
    }

    void InitializingSlidersWithCallbacks()
    {
        // X position slider
        PositionX.Init("Pos X", defaultOffset.x,
            (float value) =>
            {
                Vector3 targetValue = new Vector3(value, cameraState.PosOffset.y, cameraState.PosOffset.z);
                cameraState.PosOffset = targetValue;
                SaveLoad.Save(_defaultOffset.Key, targetValue);
                SaveLoad.SaveProgress();
            });

        // Y position slider
        PositionY.Init("Pos Y", defaultOffset.y,
            (float value) =>
            {
                Vector3 targetValue = new Vector3(cameraState.PosOffset.x, value, cameraState.PosOffset.z);
                cameraState.PosOffset = targetValue;
                SaveLoad.Save(_defaultOffset.Key, targetValue);
                SaveLoad.SaveProgress();
            });

        // Z position slider
        PositionZ.Init("Pos Z", defaultOffset.z,
            (float value) =>
            {
                Vector3 targetValue = new Vector3(cameraState.PosOffset.x, cameraState.PosOffset.y, value);
                cameraState.PosOffset = targetValue;
                SaveLoad.Save(_defaultOffset.Key, targetValue);
                SaveLoad.SaveProgress();
            });

        // X rotation slider
        RotationX.Init("Rot X", defaultRotation.x,
            (float value) =>
            {
                Vector3 targetValue = new Vector3(value, cameraState.LookOffset.y, cameraState.LookOffset.z);
                cameraState.LookOffset = targetValue;
                SaveLoad.Save(_defaultRotation.Key, targetValue);
                SaveLoad.SaveProgress();
            });

        // Y rotation slider
        RotationY.Init("Rot Y", defaultRotation.y,
            (float value) =>
            {
                Vector3 targetValue = new Vector3(cameraState.LookOffset.x, value, cameraState.LookOffset.z);
                cameraState.LookOffset = targetValue;
                SaveLoad.Save(_defaultRotation.Key, targetValue);
                SaveLoad.SaveProgress();
            });
    }

    protected override void ResetValues()
    {
        PositionX.slider.UpdateGivenValue(resetOffset.x);
        PositionY.slider.UpdateGivenValue(resetOffset.y);
        PositionZ.slider.UpdateGivenValue(resetOffset.z);
        RotationX.slider.UpdateGivenValue(resetRotation.x);
        RotationY.slider.UpdateGivenValue(resetRotation.y);
    }
}