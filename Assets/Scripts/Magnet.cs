using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float magnetForce = 0f;
    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<Rigidbody2D>().AddForce(-transform.right * magnetForce, ForceMode2D.Force);
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        Vector2 v = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
        v.x = 0;
        //collision.gameObject.GetComponent<Rigidbody2D>().velocity = v;

    }
    public void ChangeMagnetForce(string str)
    {
        magnetForce = float.Parse(str);
    }

}
