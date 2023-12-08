using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    public Text highScoreText;
    public InputField nameInput;

    private void Start()
    {
        GameManager.Instance.LoadInfos();
        highScoreText = GameObject.FindWithTag("HighScore").GetComponent<Text>();

        if (highScoreText != null)
        {
            highScoreText.text = $"Highscore: {GameManager.Instance.bestUser} : {GameManager.Instance.highScore}";
        }
    }

    public void StartGame()
    {
        SubmitName(nameInput);

        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        GameManager.Instance.SaveInfos();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit()
#endif
    }

    public void SubmitName(InputField input) => GameManager.Instance.userName = input.text;
}
