
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Rotator", menuName = "Utilities/Camera/Rotator", order = 0)]
public class RotatorCamera : BaseCameraState
{
    [InfoBox("Rotate around at current point ignoring following")]
    [SerializeField]
    [Range(0, 50)]
    float rotateSpeed = 1;
    [SerializeField]
    [Range(0, 50)]
    float angleLerpSpeed = 10;
    [SerializeField]
    Vector3 rotateOffset;

    Vector3 offsetLerping;
    float xValue = 0;

    public override void UpdateCamera(Transform target, Transform camera, Transform pivot, float deltaTime)
    {
        xValue = Mathf.Lerp(camera.localEulerAngles.x, rotateOffset.x, angleLerpSpeed * deltaTime);
        offsetLerping = new Vector3(xValue, rotateOffset.y, rotateOffset.z);
        camera.ObjectRotator(rotateSpeed, Utility.Axis.Y, offsetLerping);
        UpdateFOV(camera);
    }
}
