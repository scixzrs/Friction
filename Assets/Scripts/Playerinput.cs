using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Playerinput : MonoBehaviour
{
    [SerializeField] private Button resetButton,stopButton;
    public Transform ballTransform;
    public GameObject cannonballPrefab;
    private GameObject cannonball;
    public Transform spawnPoint;
    private Vector2 launchVelocity;
    private bool isSpawned = false;
    private float speed;
    private float speed2;
    private Vector3 shootDir;
    public AudioSource thinkMusic;
    public AudioSource playMusic;
    public AudioSource sfx;
    public AudioClip tap;
    public AudioClip shootClip;
    [SerializeField] private Dragging script;
    private float angle;
    bool playMusicOn = false;
    
    void Awake()
    {   //start level with timescale 0
        Time.timeScale = 0f;
        thinkMusic.Play();
        playMusic.GetComponent<AudioSource>().Stop();
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        double currentTime = Time.time;
        //store objects with "Draggable" tag to an array
        angle = script.platform.eulerAngles.z;
        GameObject[] g  = GameObject.FindGameObjectsWithTag("Draggable");

        foreach (GameObject gg in g) {  //loop through gameobjects in the array
            gg.SendMessage("Activate",true);    //call Activate() in 'Dragging' script
            gg.SendMessage("Deactivate", false);
        }
        stopButton.gameObject.SetActive(true);  //show edit button
       
        if (!playMusicOn)
        {
            playMusic.Play();
            playMusicOn=true;
        }
        thinkMusic.GetComponent<AudioSource>().Stop();
        sfx.PlayOneShot(shootClip,1.5f);
        if (!isSpawned)
        {
            Fire();
            print(angle);
        }
        else
        {
            Destroy(cannonball);
            isSpawned = false;
            Fire();
        }
    }

    public void Replay()
    {
        sfx.PlayOneShot(tap,2f);
        Resume();
    }

    public void Fire()
    {
        cannonball = Instantiate(cannonballPrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = cannonball.GetComponent<Rigidbody2D>();
        Vector2 shootDir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        rb.AddForce(speed2 * shootDir, ForceMode2D.Impulse);
        isSpawned = true;
        print(shootDir);
    }

    public void ChangeVelocity(string str)
    {
        speed = float.Parse(str)/1.5f;
        speed2 = speed;
    }


}
