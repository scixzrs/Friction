using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceAnim : MonoBehaviour
{
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D()
    {
        animator.SetBool("isTriggered", true);
    }
}