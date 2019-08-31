using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeashController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    //array isn't working and I don't know why
    //public Transform[] points;

    public Transform origin;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, origin.position);
        lineRenderer.SetPosition(1, target.position);

        //for (int i = 0; i < points.Length; ++i)
        //{
        //    lineRenderer.SetPosition(i, points[i].position);
        //}
    }
}
