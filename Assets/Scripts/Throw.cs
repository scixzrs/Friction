using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    [SerializeField] private Transform grabPoint;
    [SerializeField] private Transform rayPoint;
    [SerializeField] private float rayDistance;
    //[SerializeField] float launchForce = 1.5f;
    [SerializeField] float trajectoryTimestep = 0.05f;
    [SerializeField] int trajectoryStepCount = 15;

    private GameObject grabbedObject;
    private int layerIndex;
    Vector2 objVelocity, startMousePos, currentMousePos;
    private LineRenderer lineRenderer;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {//getting layer index by name 'Objects' and storing it in a variable
        layerIndex = LayerMask.NameToLayer("Objects");
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
             
    }
    void DrawTrajectory()
    {
        Vector3[] positions = new Vector3[trajectoryStepCount];
        for (int i=0; i<trajectoryStepCount; ++i)
        {
            float t = i * trajectoryTimestep;
            Vector3 pos = (Vector2)grabPoint.position + objVelocity * t + 0.5f * Physics2D.gravity * t * t;

            positions[i] = pos;
        }

        lineRenderer.positionCount = trajectoryStepCount;
        lineRenderer.SetPositions(positions);
    }

    void FireProjectile()
    {
        if (grabbedObject != null) {
            grabbedObject.GetComponent<Rigidbody2D>().velocity = objVelocity;
        }
    }
}
