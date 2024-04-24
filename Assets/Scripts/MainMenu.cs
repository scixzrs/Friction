using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private Database db;

    [SerializeField] private Button playButton;
    [SerializeField] private Button editButton;

    public void GoToScene(string sceneName)     //load sceneName
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitApp()       //quit game
    {
        Application.Quit();
    }

    public void Reset()     //reload level
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EditMode()      //function to pause the game runtime
    {
        playButton.gameObject.SetActive(true);      //show playbutton
        editButton.gameObject.SetActive(false);     //hide edit button
        GameObject[] g  = GameObject.FindGameObjectsWithTag("Draggable");
        foreach (GameObject go in g)
        {
            go.SendMessage("Activate", false);
        }

        Time.timeScale = 0f;
    }

    public void LoadSave()
    {
        db = FindObjectOfType<Database>();

        int lastLevel = db.GetCompletedLevel();
        if (lastLevel != 0)
        {
            SceneManager.LoadScene(lastLevel + 1);
        } else
        {
            SceneManager.LoadScene(1);
        }
    }
}
