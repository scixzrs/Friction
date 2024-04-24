using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GM : MonoBehaviour
{ Color color = new Color(0.9294118f, 0.9647059f, 0.4901961f, 1f);
    public GameObject currentObject;
    float volume;
    int availableHints = 0;
    int usedHints;
    bool toggle;
    // Start is called before the first frame update
    void Start()
    {
        volume = PlayerPrefs.GetFloat("masterVolume");
        PlayerPrefs.SetInt("availableHints", availableHints);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

    }

    public void AddHint()
    {
        availableHints++;
        PlayerPrefs.SetInt("availableHints", availableHints);
    }
    public void UseHint()
    {
        availableHints--;
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
}
