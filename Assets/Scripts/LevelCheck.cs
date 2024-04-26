using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCheck : MonoBehaviour
{   
    private int i;
    [SerializeField] TextMeshProUGUI level;
    private Database db;
     
    void Start()
    {
        level.text = "Level " + SceneManager.GetActiveScene().buildIndex.ToString();
        db = FindObjectOfType<Database>();
        db.InsertLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMouseDown()
    {
        i = SceneManager.GetActiveScene().buildIndex;
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(i))
        {
            i++;
            SceneManager.LoadScene(i);
        }
        db.LevelCompleted(i);       //To Database.cs > LevelCompleted(int levelNum)
    }
}
