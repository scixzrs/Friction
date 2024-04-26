using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartAgain : MonoBehaviour
{
    private void Start()
    {
       
    }
    public void GoBack()
    {
        StartCoroutine(StartDelay());
    }
    IEnumerator StartDelay()
    {
        float pauseTime = Time.realtimeSinceStartup - 5f;
        while (Time.realtimeSinceStartup < pauseTime)
            yield return 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
