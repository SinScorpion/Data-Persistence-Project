#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class MenuUIHandler : MonoBehaviour
{
    public static MenuUIHandler Instance;
    public static string PlayerNameValue;


    public TMP_InputField playerName;
    public TMP_Text bestScore;

    private void Start()
    {
        LoadUser();
    }

    private void Awake()
    {
        if (Instance !=null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayerName()
    {
        PlayerNameValue = playerName.text;
    }

    public void StartMain()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    [System.Serializable]
    class SaveData
    {
        public string name;
        public int bestScore;
    }
    public void LoadUser()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScore.text = $"Best Score: {data.name}: {data.bestScore}";
        }
        else
        {
            bestScore.text = $"Best Score: 0";
        }
    }
}
