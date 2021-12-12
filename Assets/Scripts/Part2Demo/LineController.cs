using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lr;
    private Transform[] points;
    
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void SetUpLine(Transform[] points, Color color)
    {
        lr.positionCount = points.Length;
        this.points = points;
        lr.SetColors(color, color);
    }
    void Update()
    {
        for(int i = 0; i < points.Length; i++)
        {
            lr.SetPosition(i, points[i].position);
        }
        // width is the width of the line
        float width = lr.startWidth;
        lr.material.mainTextureScale = new Vector2(1f / width, 1.0f);
        // 1/width is the repetition of the texture per unit (thus you can also do double
        // lines)
    }
}
