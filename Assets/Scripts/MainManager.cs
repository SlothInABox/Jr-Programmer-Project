using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }

    public Color teamColor; // Color selected by player

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadColor();
    }

    [System.Serializable]
    class SaveData
    {
        public Color teamColor;
    }

    public void SaveColor()
    {
        SaveData data = new SaveData(); // New save data instance
        data.teamColor = teamColor;

        string json = JsonUtility.ToJson(data); // Transform instance to JSON

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json); // Transform into save data instance

            teamColor = data.teamColor;
        }
    }
}
