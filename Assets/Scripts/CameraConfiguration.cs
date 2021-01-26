using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraConfiguration
{
    public float yaw = 0.0f;
    public float pitch = 0.0f;
    public float roll = 0.0f;
    public Vector3 pivot = new Vector3(0.0f, 1.0f, -10.0f);
    public float distance = 0.0f;
    public float fieldOfView = 60.0f;

    public CameraConfiguration()
    {
        yaw = 0.0f;
        pitch = 0.0f;
        roll = 0.0f;
        distance = 0.0f;
        fieldOfView = 0.0f;
        pivot = Vector3.zero;
    }

    public Quaternion GetRotation()
    {
        return Quaternion.Euler(pitch, yaw, roll);
    }

    public Vector3 GetPosition()
    {
        Vector3 offset = GetRotation() * (Vector3.back * distance);
        return pivot + offset;
    }

    public void DrawGizmos(Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(pivot, 0.25f);
        Vector3 position = GetPosition();
        Gizmos.DrawLine(pivot, position);
        Gizmos.matrix = Matrix4x4.TRS(position, GetRotation(), Vector3.one);
        Gizmos.DrawFrustum(Vector3.zero, fieldOfView, 0.5f, 0f, Camera.main.aspect);
        Gizmos.matrix = Matrix4x4.identity;
    }
}
