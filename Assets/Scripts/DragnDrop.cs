using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragnDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //public GameObject target;
    GameObject dragObject;
    //public Text itemCountText;
    public Canvas b1,b2;
    RaycastHit2D hit;
    Vector3 Pos;
    public type myType;
    public int itemCount;
    private int itemsUsed=0;
    [SerializeField] public Button resetButton;
    [SerializeField] public Button replayButton;
    [SerializeField] public Transform ballTransform;
    private float x;
    private float y;
    [SerializeField] private Button stopButton;
    public enum type {ball10, ball1, fan, box, platform};

    public void Start()
    {
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemsUsed < itemCount)
        {
            Debug.Log("Begin Dragging");
            //Instantiate<GameObject>(target, parentAfterDrag);
            //dragObject = Instantiate(target, transform.position, transform.rotation);
            b1.enabled = false;
            itemCount--;
            switch (myType)
            {
                case type.ball10:
                    dragObject.GetComponent<Weight>().SetWeight(10f);
                    break;
                case type.ball1:
                    dragObject.GetComponent<Weight>().SetWeight(1f);
                    break;
                case type.fan:
                    break;
                case type.box:
                    break;
                case type.platform:
                    break;
            }
        }
        else if (itemsUsed == itemCount)
        {
            gameObject.SetActive(false);
        }
       
    }
    public void OnDrag(PointerEventData eventData)
    {
        //dragObject.transform.position = Camera.main.WorldToScreenPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f));
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       

       switch (myType)
        {
            case type.ball10:
                dragObject.GetComponent<Rigidbody2D>().simulated = true;
                dragObject.GetComponent<Collider2D>().enabled = true;
                break;

            case type.ball1:
                dragObject.GetComponent<Rigidbody2D>().simulated = true;
                dragObject.GetComponent<Collider2D>().enabled = true;
                break;
            case type.fan:
                dragObject.GetComponent<Collider2D>().enabled = true;
                break;
            case type.box:
                 dragObject.GetComponent<Rigidbody2D>().simulated = true;
                 dragObject.GetComponent<Collider2D>().enabled = true;
                break;
            case type.platform:
                dragObject.GetComponent<Dragging>().enabled = true;
                break;

        }
        dragObject = null;
        b1.enabled = true;


    }
   
    void Update()
    {
        if (dragObject != null)
        {
            Pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f));
            Pos.z = 11.0f;
            dragObject.transform.position = Pos;
            //print(Pos);
        }

        //itemCountText.GetComponent<Text>().text = "x" + itemCount.ToString();
    }
}
