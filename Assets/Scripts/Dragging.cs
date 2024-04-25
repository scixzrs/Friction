using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Dragging : MonoBehaviour
{
    Vector3 Pos;
    public Transform platform;
    private bool isClicked;
    float clickTime;
    public SpriteRenderer rotate;
    public SpriteRenderer move;
    public Collider2D[] mycoll;
    public Collider2D[] dragcoll;
    public Canvas mycanvas;
    [SerializeField] TMP_InputField myInput;
    private float f;
    private int availableHints;
    private GM GMScript;
    private Quaternion ogRotation;

    private void Start()
    {
        f = Random.Range(0, 360);
        platform.Rotate(0,0,f);
        myInput.text = f.ToString();
        GMScript = FindObjectOfType<GM>().GetComponent<GM>();        ogRotation = gameObject.transform.rotation;
    }
    // Update is called once per frame

    void Update()
    {
        if (isClicked)
        {
            Pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f));
            Pos.z = 11.0f;
            //transform.position = Pos;
            /*if (Time.realtimeSinceStartup - clickTime > 0.2f && !rotate.enabled)
            {
                move.enabled = true;
            }*/
        }

    }

    private void OnMouseDown()
    {
        isClicked = true;
        clickTime = Time.realtimeSinceStartup;
        if (!mycanvas.enabled)
        {
            mycanvas.enabled = !mycanvas.enabled;
        }
        else if (mycanvas.enabled) { mycanvas.enabled = !mycanvas.enabled; }

        GameObject.FindWithTag("GM").SendMessage("SetObject",this.gameObject);
     }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (PlayerPrefs.GetInt("availableHints") > 0)
            {
                GMScript.UseHint();
                platform.rotation = ogRotation;
            }
        }

    }

    private void OnMouseUp()
    {
        isClicked = false;
        float t = Time.realtimeSinceStartup - clickTime;

        /*if (t < 0.3f)
        {
            platform.Rotate(0,0,-45);
            StartCoroutine(ShowRotate());
        }*/
        /*move.enabled = false;*/
    }

    /*IEnumerator ShowRotate()
    {
        rotate.enabled = true;
        yield return new WaitForSecondsRealtime(0.2f);
        rotate.enabled = false;
    }*/
    public void Activate(bool yeawhat)
    {
        foreach (Collider2D cc in mycoll) {
            cc.enabled = yeawhat;
            cc.gameObject.GetComponent<CreateOutline>().enabled = true;
        }
        foreach (Collider2D dc in dragcoll) {
            dc.enabled = !yeawhat;
        }

    }
    public void ChangeAngle(string str)
    {
        platform.eulerAngles = new Vector3(0, 0, int.Parse(str));
    }

    public void Deactivate()
    {
        mycanvas.enabled = false;
    }
}
