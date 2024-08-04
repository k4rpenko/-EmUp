using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData
{
    public int MaxCube;
}

public class Triger : MonoBehaviour
{
    public TMP_Text MaxNumberCube;
    PutCube _Put = new PutCube();
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Clone")
        {
            Debug.Log(other.name);
            EndGame();
        }
    }


    int LoadDataCube()
    {
        string path = Application.persistentDataPath + "/savedata.json";
        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            GameData data = JsonUtility.FromJson<GameData>(json);
            return data.MaxCube;
        }
        return 0;
    }

    public void SaveData()
    {
        GameData data = new GameData();
        data.MaxCube = _Put.Number_cube;
        string json = JsonUtility.ToJson(data);
        Debug.Log(json);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/savedata.json", json);
    }

    void EndGame()
    {
        if (_Put.Number_cube > LoadDataCube()) { SaveData(); }
        SceneManager.LoadScene("SampleScene");

        /*
        Time.timeScale = 0;
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        */
    }

    void Start()
    {
        _Put = FindObjectOfType<PutCube>();
        MaxNumberCube.text = LoadDataCube().ToString();
    }
}
