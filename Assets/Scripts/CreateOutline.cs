using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CreateOutline : MonoBehaviour
{ 
    public GameObject colliderLine;
    LineRenderer lineRenderer;
    BoxCollider2D boxCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = Instantiate(colliderLine).GetComponent<LineRenderer>();
        lineRenderer.transform.SetParent(transform);
        lineRenderer.transform.localPosition = Vector3.zero;
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HiliteBox();

    }

   void HiliteBox()
    {
        Vector3[] positions = new Vector3[4];
        positions[0] = transform.TransformPoint(new Vector3(boxCollider2D.size.x / 2.0f, boxCollider2D.size.y / 2.0f, 0));
        positions[1] = transform.TransformPoint(new Vector3(-boxCollider2D.size.x / 2.0f, boxCollider2D.size.y / 2.0f, 0));
        positions[2] = transform.TransformPoint(new Vector3(-boxCollider2D.size.x / 2.0f, -boxCollider2D.size.y / 2.0f, 0));
        positions[3] = transform.TransformPoint(new Vector3(boxCollider2D.size.x / 2.0f, -boxCollider2D.size.y / 2.0f, 0));
        lineRenderer.SetPositions(positions);
    }

}
