using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string playerName;
    public string highestScorePlayerName;
    public int highestScore;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    public class SaveData {
        public string playerName;
        public int highestScore;
    }

    public void SaveHighestScore() {
        SaveData data = new SaveData();
        data.playerName = playerName;
        data.highestScore = highestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savedata.json", json);
    }

    public void LoadHighestScore() {
        string path = Application.persistentDataPath + "/savedata.json";
        if (File.Exists(path)) {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highestScorePlayerName = data.playerName;
            highestScore = data.highestScore;
        }
    }
}
