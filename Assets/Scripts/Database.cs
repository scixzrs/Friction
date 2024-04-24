using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class Database : MonoBehaviour
{
    private string dbPath;
    private SQLiteConnection conn;
    // Start is called before the first frame update
    void Awake()
    {
        dbPath = Path.Combine(Application.persistentDataPath, "savedata.db");

        conn = new SQLiteConnection(dbPath);
        conn.CreateTable<PlayerProgress>();
    }
    

    public class PlayerProgress
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int LevelNumber { get; set; }
        public bool IsCompleted { get; set; }
    }
    
    public void LevelCompleted(int levelNum)
    {   //from LevelCheck.cs > OnMouseDown()
        var progress = conn.Table<PlayerProgress>().FirstOrDefault(p => p.LevelNumber == levelNum);
        if (progress != null)
        {
            progress.LevelNumber = levelNum;
            progress.IsCompleted = true;
            conn.Update(progress);
        }
        else
        {       //game always gives "Level 1 not found in database"
            Debug.LogWarning($"Level {levelNum} not found in the database.");
        }
    }

    public int GetCompletedLevel()      //To MainMenu.cs > LoadSave()
    {
        var lastLevel = conn.Table<PlayerProgress>().Where(p => p.IsCompleted).Max(p => p.LevelNumber);

        Debug.Log("Got level");
        return lastLevel;
    }

    public void InsertLevel(int level)
    {
        var progress = new PlayerProgress()
        {
            LevelNumber = level,
            IsCompleted = false
        };

        conn.Insert(progress);
    }
}
