using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
 using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string userName;
    public string bestUser;
    public int score = 0;
    public int highScore = 0;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public string bestUser;
        public int HighScore;
    }

    public void SaveInfos()
    {
        SaveData data = new SaveData();
        data.bestUser = bestUser;
        data.HighScore = highScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadInfos()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestUser = data.bestUser;
            highScore = data.HighScore;
        }
    }
}
