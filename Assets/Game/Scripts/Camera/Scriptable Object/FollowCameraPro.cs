using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Follow Pro", menuName = "Utilities/Camera/Follow Pro", order = 0)]
public class FollowCameraPro : BaseCameraState
{
    [InfoBox("Follow Target and Look At given offset by offset")] [SerializeField] [Range(0.1f, 5)]
    float followSpeed = 4;

    [Range(0.1f, 30)] public  float lookSpeed = 10;

    Vector3 dir;

    public override void UpdateCamera(Transform target, Transform camera, Transform pivot, float deltaTime)
    {
        pivot.localPosition = Vector3.Lerp(pivot.localPosition, posOffset, followSpeed * deltaTime);
        dir = target.position - camera.position;
        dir.Normalize();
        camera.position = Vector3.Lerp(camera.position, target.position - dir, followSpeed * deltaTime);
        camera.FollowRotation(target, lookOffset, lookSpeed);
        UpdateFOV(camera);
    }
}