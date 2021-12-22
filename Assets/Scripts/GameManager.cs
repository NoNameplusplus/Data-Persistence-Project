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
    public static string UserName;
    public static int HighScore = 0;
    public static GameManager Instance;
    public Text UsersName;

    private void Start()
    {
        UsersName.text = "Best Score: " + UserName + " : " + HighScore;
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadInfos();
        UsersName.text = "Best Score: " + UserName + " : " + HighScore;
    }

    public void SubmitName(InputField input)
    {
        UserName = input.text;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        SaveInfos();

        Debug.Log("Saving Part Works");
    }

    [System.Serializable]
    class SaveData
    {
        public string UserName;
        public int HighScore;
    }

    public void SaveInfos()
    {
        SaveData data = new SaveData();
        data.UserName = UserName;
        data.HighScore = HighScore;

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

            UserName = data.UserName;
            HighScore = data.HighScore;
        }
    }
}
