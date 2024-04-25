using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class WeightCheck : MonoBehaviour
{
    float currentWeight = 0f;
    float currentTime=0;
    public float maxWeight;
    public TextMeshPro weightText;
    public Button next;
    public Animator animator;
    //public SpriteRenderer proceed;
    public AudioSource sfx;
    public AudioClip complete;
    bool win;

    // Update is called once per frame
    void Update()
    {
        if (currentWeight == maxWeight && !win && Time.time-currentTime<=2)
        {
            StartCoroutine("Winlevel");
        }
        //else {next.gameObject.SetActive(false);  /* proceed.enabled = false; */ animator.SetBool("scaleDown", false); /*coll.enabled = true; */}
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        currentTime = Time.time;
        if (other.gameObject.GetComponent<Weight>().objWeight != 0)
        {
            currentWeight = other.GetComponent<Weight>().objWeight;
            weightText.text = "PASS";
        }
        sfx = GameObject.FindWithTag("SFX").GetComponent<AudioSource>();

    }

    void OnTriggerExit2D(Collider2D other)
    {
        currentTime = 0;
        if (other.gameObject.GetComponent<Weight>().objWeight != 0)
        {
            currentWeight = 0f;
            weightText.text = "ERROR";
        }
    }

    IEnumerator Winlevel()
    {
            win = true;
            animator.SetBool("scaleDown",true);
            yield return new WaitForSeconds(0.7f);
            //coll.GetComponent<Collider2D>().enabled = false;
            next.transform.parent.gameObject.SetActive(true);
            next.gameObject.SetActive(true);
            Time.timeScale = 0f;
            sfx.PlayOneShot(complete, 0.1f);
            //Time.timeScale = 0f;

    }
}
