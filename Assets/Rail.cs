using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour
{
    [SerializeField] Transform[] children;
    public bool isLoop = false;
    float length = 0.0f;

    private void Awake()
    {
        children = GetComponentsInChildren<Transform>();
    }

    private void Start()
    {
        for (int i = 1; i < children.Length - 1; i++)
        {
            length += Vector3.Distance(children[i].position, children[i + 1].position);
        }
        if (isLoop)
        {
            length += Vector3.Distance(children[children.Length - 1].position, children[1].position);
        }

    }

    public void DrawGizmos()
    {
        if (children.Length == 0)
            return;

        for (int i = 1; i < children.Length; i++)
        {
            if (i + 1 < children.Length)
                Gizmos.DrawLine(children[i].position, children[i + 1].position);
        }

        if (isLoop)
        {
            Gizmos.DrawLine(children[children.Length - 1].position, children[1].position);
        }
    }

    public float GetLength()
    {
        return length;
    }

    public Vector3 GetPosition(float distance)
    {
        float exempleLength = 0.0f;
        if (isLoop)
        {
            while (distance < 0)
            {
                distance += length;
            }
            distance = distance % length;

        }

        for (int i = 1; i < children.Length - 1; i++)
        {
            exempleLength = Vector3.Distance(children[i].position, children[i + 1].position);
            if(distance < exempleLength)
            {
                return Vector3.Lerp(children[i].position, children[i + 1].position, distance/exempleLength);
            }
            else
            {
                distance -= exempleLength;
            }
        }
        if (isLoop)
        {
            exempleLength = Vector3.Distance(children[children.Length - 1].position, children[1].position);
            if (distance < exempleLength)
            {
                return Vector3.Lerp(children[children.Length - 1].position, children[1].position, distance / exempleLength);
            }
            else
            {
                distance -= exempleLength;
            }
        }
        return children[children.Length - 1].position;
    }

    private void OnDrawGizmos()
    {
        DrawGizmos();
    }
}
