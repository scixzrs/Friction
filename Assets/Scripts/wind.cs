using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class wind : MonoBehaviour
{
    [SerializeField] float windForce;
    // Start is called before the first frame update

    public void ChangeWindForce(string str)
    {
        windForce = float.Parse(str);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<Rigidbody2D>().AddForce(transform.up * windForce);

    }
}
