using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GM : MonoBehaviour
{ Color color = new Color(0.9294118f, 0.9647059f, 0.4901961f, 1f);
    public GameObject currentObject;
    int availableHints = 0;
    int usedHints;
    bool toggle;
    public TextMeshProUGUI hintText;
    public AudioSource thinkMusic;
    public AudioSource playMusic;
    public Button muteButton;
    public Button unmuteButton;
 
    // Start is called before the first frame update
    void Start()
    {
        availableHints = PlayerPrefs.GetInt("availableHints");

    }
    private void Awake()
    {
        toggle = Convert.ToBoolean(PlayerPrefs.GetInt("sound"));
        
        SetVolume();
    }

    // Update is called once per frame
    void Update()
    {
        hintText.text = availableHints.ToString();
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        SetVolume();

    }
    void SetVolume()
    {
        if (toggle)
        {
            thinkMusic.volume = 0f;
            playMusic.volume = 0f;
            muteButton.gameObject.SetActive(!toggle);
            unmuteButton.gameObject.SetActive(toggle);
        }
        else
        {
            thinkMusic.volume = 0.7f;
            playMusic.volume = 0.7f;
            muteButton.gameObject.SetActive(!toggle);
            unmuteButton.gameObject.SetActive(toggle);

        }
        PlayerPrefs.Save();
    }
    public void AddHint()
    {
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            availableHints++;
            availableHints++;
        } else
        {
            availableHints++;
        }

        PlayerPrefs.SetInt("availableHints", availableHints);
    }
    public void UseHint()
    {
        if (availableHints > 0)
        {
            availableHints--;
        }
        else availableHints = 0;
        
        PlayerPrefs.SetInt("availableHints", availableHints);
    }

    public void SetObject(GameObject g) {
        if (g != currentObject && currentObject != null)
        {
            currentObject.SendMessage("Deactivate");
            currentObject.GetComponentInChildren<LineRenderer>().startColor = new Color(0.006026864f,1,0);
            currentObject.GetComponentInChildren<LineRenderer>().endColor = new Color(0, 0.6901961f, 0.08035907f);

        }
        currentObject = g;
        currentObject.GetComponentInChildren<LineRenderer>().startColor = color;
        currentObject.GetComponentInChildren<LineRenderer>().endColor = color;

        
    }

    public void MusicToggle()
    {
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            PlayerPrefs.SetInt("sound", 1);
            toggle = true;
        }
        else if (PlayerPrefs.GetInt("sound") == 1)
        {
            PlayerPrefs.SetInt("sound", 0);
            toggle = false;
        }
        else
        {
            PlayerPrefs.SetInt("sound", 1);
            toggle = true;
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("availableHints", 0);
    }
}
