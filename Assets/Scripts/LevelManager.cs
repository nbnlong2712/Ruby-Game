using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static bool isNewGame = true;

    public void LoadLevel1()
    {
        isNewGame = true;
        SceneManager.LoadScene("Level 1");
        Player.Level = 1;
    }

    public void LoadLevel2()
    {
        isNewGame = true;
        Player.Level = 2;
        FindObjectOfType<Player>().ResetHealth();
        FindObjectOfType<Player>().ResetAmmo();
        SceneManager.LoadScene("Level 2");
    }

    public void LoadGame()
    {
        isNewGame = false;
        string path = Path.Combine(Application.persistentDataPath, "player.hd");
        if (File.Exists(path))
        {
            FileStream file = File.Open(path, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            PlayerData playerData = (PlayerData)formatter.Deserialize(file);
            file.Close();
            if (playerData.level == 1)
            {
                Player.Level = 1;
                SceneManager.LoadScene("Level 1");
            }
            else if (playerData.level == 2)
            {
                Player.Level = 2;
                SceneManager.LoadScene("Level 2");
            }
        }
    }

    public void LoadGameJson()
    {
        isNewGame = false;
        string path = Path.Combine(Application.persistentDataPath, "player.json");
        if (File.Exists(path))
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader reader = new StreamReader(path))
            using (JsonReader jsonReader = new JsonTextReader(reader))
            {
                PlayerData playerData = serializer.Deserialize<PlayerData>(jsonReader);
                if (playerData.level == 1)
                {
                    Player.Level = 1;
                    SceneManager.LoadScene("Level 1");
                }
                else if (playerData.level == 2)
                {
                    Player.Level = 2;
                    SceneManager.LoadScene("Level 2");
                }
                print(playerData.level + " - " + playerData.positionX + " - " + playerData.positionY + " - " + playerData.health + " - " + playerData.ammo);
            }
        }
    }

    public void LoadGameFileBinary(Player player)
    {
        string path = Path.Combine(Application.persistentDataPath, "player.hd");
        if (File.Exists(path))
        {
            FileStream file = File.Open(path, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            PlayerData playerData = (PlayerData)formatter.Deserialize(file);
            file.Close();
            Player player1 = player.GetComponent<Player>();
            player1.SetHealth(playerData.health);
            player1.SetAmmo(playerData.ammo);
            player1.transform.position = new Vector2(playerData.positionX, playerData.positionY);
        }
    }

    public void LoadGameFileJson(Player player)
    {
        string path = Path.Combine(Application.persistentDataPath, "player.json");
        if (File.Exists(path))
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader reader = new StreamReader(path))
            using (JsonReader jsonReader = new JsonTextReader(reader))
            {
                string jsonPlayer = File.ReadAllText(path);
                PlayerData playerData = JsonUtility.FromJson<PlayerData>(jsonPlayer);
                Player player1 = player.GetComponent<Player>();
                player1.SetHealth(playerData.health);
                player1.SetAmmo(playerData.ammo);
                player1.transform.position = new Vector2(playerData.positionX, playerData.positionY);
            }
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGameOver()
    {
        Player.Level = 1;
        Invoke("WaitAndLoad", 2);
    }

    public void LoadOptionMenu()
    {
        SceneManager.LoadScene("Option");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void WaitAndLoad()
    {
        SceneManager.LoadScene("Over");
    }
}