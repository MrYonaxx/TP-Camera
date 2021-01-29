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
        if (children == null)
            return;
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
        if (children.Length == 0)
            return Vector3.zero;
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











    public Vector3 GetNearestPoint(Transform target)
    {
        if (children.Length == 0)
            return Vector3.zero;

        int nearestPoint = 0;
        float nearestDistance = Mathf.Infinity;
        for (int i = 1; i < children.Length; i++)
        {
            float d = Vector3.SqrMagnitude(children[i].position - target.position);
            if(d < nearestDistance)
            {
                nearestDistance = d;
                nearestPoint = i;
            }
        }

        /*Vector3 nodeToTarget = target.position - children[nearestPoint].position;
        Vector3 nodeATonodeB = children[nearestPoint].position - children[nearestPoint+1].position;
        float angle = Vector3.Angle(nodeToTarget, nodeATonodeB);

        Vector3 nodeAToN = Mathf.Cos(angle * Mathf.Deg2Rad) * nodeToTarget;

        float distanceN = Vector3.Distance(children[nearestPoint].position, children[nearestPoint].position + nodeAToN);

        float finalDistance = 0;
        for (int i = 1; i < nearestPoint; i++)
        {
            finalDistance += Vector3.Distance(children[i].position, children[i + 1].position);
        }
        finalDistance += distanceN;*/
        int nextPoint = nearestPoint + 1;
        int previousPoint = nearestPoint - 1;
        if (isLoop == true)
        {
            nextPoint = nearestPoint % children.Length;
            if (previousPoint <= 0)
                previousPoint = children.Length-1;
        }
        else
        {
            if (nextPoint >= children.Length)
                nextPoint = nearestPoint;
            if (previousPoint <= 0)
                previousPoint = nearestPoint;
        }



        // C'est full pas opti tout ça désolé
        float distance1 = GetDistance(target, nearestPoint, nextPoint);
        float distance2 = GetDistance(target, previousPoint, nearestPoint);
        Vector3 finalPoint1 = GetPosition(distance1);
        Vector3 finalPoint2 = GetPosition(distance2);

        Debug.DrawRay(target.transform.position, finalPoint1 - target.position);
        Debug.DrawRay(target.transform.position, finalPoint2 - target.position);

        float finalDistance1 = (finalPoint1 - target.position).sqrMagnitude;
        float finalDistance2 = (finalPoint2 - target.position).sqrMagnitude;

        if (finalDistance1 <= finalDistance2)
            return finalPoint1;
        return finalPoint2;

    }

    public float GetDistance(Transform target, int childID, int childID2)
    {
        Vector3 nodeToTarget = target.position - children[childID].position;
        Vector3 nodeATonodeB = children[childID].position - children[childID2].position;
        float angle = Vector3.Angle(nodeToTarget, nodeATonodeB);

        Vector3 nodeAToN = Mathf.Cos(angle * Mathf.Deg2Rad) * nodeToTarget;

        //Vector3 N = children[childID].position + nodeAToN;

        //Debug.DrawRay(target.transform.position, N - target.position);
        //return Vector3.Distance(target.position, N);

  
        float distanceN = Vector3.Distance(children[childID].position, children[childID].position + nodeAToN);

        float finalDistance = 0;
        for (int i = 1; i < childID; i++)
        {
            finalDistance += Vector3.Distance(children[i].position, children[i + 1].position);
        }
        finalDistance += distanceN;
        return finalDistance;

       // return Vector3.Distance(target.position, N);
    }




}
