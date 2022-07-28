using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class UIMainManager : MonoBehaviour
{
    public string playerName;
    public InputField playerNameField;
    public TextMeshProUGUI playerNameDisplay;
    public GameObject welcomeText;
    public GameObject startMenuDisplay;
    public Button closeHighScoreTextButton;
    public Button resetHighScoreButton;
    public TextMeshProUGUI highScoreText;

    public static UIMainManager Instance;
    public int highScore;
    public string highScorePlayerName;

    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScore();
        LoadHighScorePlayerName();
        highScoreText.text = "HIGH SCORE\n" + highScorePlayerName + ": " + highScore;
    }

    public void ShowHighScores()
    {
        highScoreText.enabled = true;
        startMenuDisplay.SetActive(false);
        resetHighScoreButton.gameObject.SetActive(true);
        closeHighScoreTextButton.gameObject.SetActive(true);
    }

    public void HideHighScores()
    {
        highScoreText.enabled = false;
        startMenuDisplay.SetActive(true);
        resetHighScoreButton.gameObject.SetActive(false);
        closeHighScoreTextButton.gameObject.SetActive(false);
    }
    public void StorePlayerName()
    {
        welcomeText.SetActive(true);
        playerName = playerNameField.text;
        playerNameDisplay.text = "Welcome " + playerName + "!!";
       
        Debug.Log(playerName);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        SaveHighScore();
        SaveHighScorePlayerName();
#if UNITY_EDITOR
        SaveHighScore();
        SaveHighScorePlayerName();
        EditorApplication.ExitPlaymode();
#else
Application.Quit();
#endif
    }

    public void ResetHighScore()
    {
        highScore = 0;
        highScorePlayerName = "You Soon!";
        highScoreText.text = "HIGH SCORE\n" + highScorePlayerName + ": " + highScore;
    }

    [System.Serializable]
    class SaveData
    {
        public int highScore;
        public string highScorePlayerName;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.highScore = highScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            highScore = data.highScore;
        }
    }

    public void SaveHighScorePlayerName()
    {
        SaveData data1 = new SaveData();
        data1.highScorePlayerName = highScorePlayerName;
        string json1 = JsonUtility.ToJson(data1);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json1", json1);
    }
    public void LoadHighScorePlayerName()
    {
        string path = Application.persistentDataPath + "/savefile.json1";
        if (File.Exists(path))
        {
            string json1 = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json1);
            highScorePlayerName = data.highScorePlayerName;
        }
    }

}
