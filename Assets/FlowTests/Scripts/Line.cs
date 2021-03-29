using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public Vector3 p0, p1;
    public Color lineColor = Color.white;
    public GameObject[] points;
    public bool drawLines = true;

    private void OnDrawGizmos()
    {

        if (points == null || !drawLines)
            return;

        for (int i = 0; i < points.Length; i++)
        {
            if (i < points.Length - 1)
            {
                Debug.DrawLine(points[i].transform.position, points[i + 1].transform.position, lineColor);
            }
        }
    }
}
