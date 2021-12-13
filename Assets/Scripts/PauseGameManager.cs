using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PauseGameManager : MonoBehaviour
{
    public static bool gameIsPause = false;
    [SerializeField] GameObject pauseMenuUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gameIsPause)
            {
                Resume();
            }
            else Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPause = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPause = true;
    }

    public void Save()
    {
        string path = Path.Combine(Application.persistentDataPath, "player.hd");
        FileStream file = File.Create(path);
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            PlayerData playerData = new PlayerData(Player.Level, player.transform.position.x, player.transform.position.y, player.GetHealth(), player.GetAmmo());
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, playerData);
            file.Close();
            print(path);
        }
    }

    public void SaveJson()
    {
        string path = Path.Combine(Application.persistentDataPath, "player.json");
        Player player = FindObjectOfType<Player>();
        JsonSerializer serializer = new JsonSerializer();
        if (player != null)
        {
            PlayerData playerData = new PlayerData(Player.Level, player.transform.position.x, player.transform.position.y, player.GetHealth(), player.GetAmmo());
            using (StreamWriter streamWriter = new StreamWriter(path))
            using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter))
            {
                serializer.Serialize(jsonWriter, playerData);
            }
        }
    }
}
