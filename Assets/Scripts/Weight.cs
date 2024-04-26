using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class Weight : MonoBehaviour
{
    [SerializeField]
    public float objWeight;
    public TextMeshPro objText;
    public Rigidbody2D rb;
    public GameObject woodenCollider;
    private AudioSource audioSource;
    public AudioClip woodenClip;
    public AudioClip stoneClip;
    public AudioClip scaleClip;
    public AudioClip terrainClip;
    public enum objType { box, ball };
    public objType type;
    private bool isPlaying;

    float dampingSize = 0.5f;
    private void Awake()
    {
        //objText.text = objWeight.ToString() + "\nkg";
        //rb.mass = objWeight;
        //transform.localScale = Vector3.one * (objWeight/ 5)
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            audioSource = GameObject.FindWithTag("SFX").GetComponent<AudioSource>();
        }
    }

    private void OnValidate()
    {
        //objText.text = objWeight.ToString() + "\nkg";
        rb.mass = objWeight;
        if (type == objType.box)
        {
            transform.localScale = Vector3.one * (objWeight / 2) * dampingSize;
        }
    }

    public void SetWeight(float w)
    {
        objWeight = w;
        ///objText.text = objWeight.ToString() + "\nkg";
        rb.mass = objWeight;
        if (type == objType.box)
        {
            transform.localScale = Vector3.one * (objWeight / 2) * dampingSize;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wooden") && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(woodenClip);
        }
        if (collision.collider.CompareTag("Stone") && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(stoneClip);
        }
        else if (collision.collider.CompareTag("Scale") && !isPlaying)
        {
            audioSource.PlayOneShot(scaleClip);
        }
        else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            audioSource.PlayOneShot(terrainClip);
        }
    }


}
