using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pyramid : MonoBehaviour
{
    [SerializeField] private Vector3[] vector;
    [SerializeField] private Vector3 pyrOffset;
    [SerializeField] private Vector3[] pyramid;

    [SerializeField] private float facesArea;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDrawGizmos()
    {
        vector[1] = GetPerpendicular(vector[0]);

        vector[2] = CrossProduct(vector[0], vector[1]);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(Vector3.zero, vector[0]);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(Vector3.zero, vector[1]);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(Vector3.zero, vector[2]);

        float shortestVec = GetShortest(vector);

        for (int i = 0; i < pyramid.Length; i++)
            pyramid[i] = SetLengh(vector[i], shortestVec);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(Vector3.zero + pyrOffset, pyramid[0] + pyrOffset);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(Vector3.zero + pyrOffset, pyramid[1] + pyrOffset);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(Vector3.zero + pyrOffset, pyramid[2] + pyrOffset);

        CalculateArea();
    }

    Vector3 GetPerpendicular(Vector3 vec)
    {
        return new Vector3(vec.y, -vec.x, vec.z);
    }

    Vector3 CrossProduct(Vector3 v1, Vector3 v2)
    {
        Vector3 cross;

        cross.x = ((v1.y * v2.z) - (v1.z * v2.y));
        cross.y = ((v1.z * v2.x) - (v1.x * v2.z));
        cross.z = ((v1.x * v2.y) - (v1.y * v2.x));

        return cross;
    }

    float GetShortest(Vector3[] vec)
    {
        float length = vec[0].magnitude;

        for (int i = 0; i < vec.Length; i++)
        {
            if (vec[i].magnitude < length)
                length = vec[i].magnitude;
        }

        return length;
    }

    Vector3 SetLengh(Vector3 vec, float length)
    {
        return (vec.normalized * length);
    }

    void CalculateArea()
    {
        float area1 = GetTriangleArea(Vector3.zero, pyramid[0], pyramid[1]);
        float area2 = GetTriangleArea(Vector3.zero, pyramid[0], pyramid[2]);
        float area3 = GetTriangleArea(Vector3.zero, pyramid[1], pyramid[2]);

        facesArea = area1 + area2 + area3;
    }

    private float GetTriangleArea(Vector3 a, Vector3 b, Vector3 c)
    {
        float side1 = (b - a).magnitude;
        float side2 = (c - b).magnitude;
        float side3 = (a - c).magnitude;

        float semiperimeter = (side1 + side2 + side3) / 2;
        float area = (float)Math.Sqrt(semiperimeter * (semiperimeter - side1) * (semiperimeter - side2) * (semiperimeter - side3));

        return area;
    }
}