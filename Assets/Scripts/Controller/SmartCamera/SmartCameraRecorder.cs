using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 摄像机机位，应该放在场景中
/// TODO：可以给SmartCameraPoint加上weight属性
/// </summary>
public class SmartCameraRecorder : MonoBehaviour
{
    public Vector2 offset;
    public float fov = 60;

    private void Start()
    {
        SmartCamera.cameraPoints.Add(new SmartCamera.SmartCameraPoint((Vector2)transform.position + offset, -offset, transform.position.z, fov));
    }

    private void OnDrawGizmos()
    {
        if (!Camera.current) return;
        Gizmos.color = Color.gray;
        float theta = fov * Mathf.PI / 360f;
        float height = -transform.position.z * Mathf.Tan(theta);
        float width = height * Camera.current.aspect;

        Gizmos.DrawWireSphere((Vector3)((Vector2)transform.position + offset), 0.25f);

        Gizmos.DrawLine(new Vector3(transform.position.x - width, transform.position.y + height), new Vector3(transform.position.x + width, transform.position.y + height));
        Gizmos.DrawLine(new Vector3(transform.position.x - width, transform.position.y + height), new Vector3(transform.position.x - width, transform.position.y - height));
        Gizmos.DrawLine(new Vector3(transform.position.x + width, transform.position.y - height), new Vector3(transform.position.x + width, transform.position.y + height));
        Gizmos.DrawLine(new Vector3(transform.position.x + width, transform.position.y - height), new Vector3(transform.position.x - width, transform.position.y - height));

        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + width, transform.position.y + height));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + width, transform.position.y - height));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x - width, transform.position.y - height));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x - width, transform.position.y + height));

    }
}
