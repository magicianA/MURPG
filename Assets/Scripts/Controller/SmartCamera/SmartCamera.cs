using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// My Smart Camera，挂载到Camera上
/// </summary>
[RequireComponent(typeof(Camera))]
public class SmartCamera : MonoBehaviour
{
    private Camera cam;

    public class SmartCameraPoint
    {
        public Vector2 anchor;
        public Vector2 offset;
        public float distance;
        public float fov;
        public float weight;

        public SmartCameraPoint(Vector2 anchor, Vector2 offset, float distance, float fov)
        {
            this.anchor = anchor;
            this.offset = offset;
            this.distance = distance;
            this.fov = fov;
            this.weight = 0;
        }
    }

    public Transform target;
    private Vector2 targetPos { get => target.position; }

    public static List<SmartCameraPoint> cameraPoints = new List<SmartCameraPoint>();

    //如果把Camera改写成系统，这几个配置应该放到类似CameraSystemSetting的配置中去
    public float reactDistance = 0.1f;
    [Range(0.01f, 0.95f)]
    public float movingRate = 0.1f;

    /// <summary>
    /// 后坐力效果
    /// </summary>
    public void React(Vector3 direction)
    {
        transform.Translate(direction * reactDistance);
    }


    private void Awake()
    {
        cameraPoints.Clear();
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        float fullWeight = 0;
        for (int i = 0; i < cameraPoints.Count; i++)
        {
            float d = Vector2.Distance(targetPos, cameraPoints[i].anchor);
            float weight = 1.0f / (d + 0.001f);
            cameraPoints[i].weight = weight;
            fullWeight += weight;
        }
        Vector2 target2D = targetPos;
        float targetZ = 0;
        float targetFov = 0;
        for (int i = 0; i < cameraPoints.Count; i++)
        {
            float factor = cameraPoints[i].weight / fullWeight;
            target2D += cameraPoints[i].offset * factor;
            targetZ += cameraPoints[i].distance * factor;
            targetFov += cameraPoints[i].fov * factor;
        }
        Vector3 target3D = new Vector3(target2D.x, target2D.y,-10f);

        float t = 1.0f - Mathf.Pow(1.0f - movingRate, Time.deltaTime / Time.timeScale);
        transform.position = Vector3.Lerp(transform.position, target3D, t);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFov, t);
    }
}
