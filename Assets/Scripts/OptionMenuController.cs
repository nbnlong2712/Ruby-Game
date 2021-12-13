using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionMenuController : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] float volume;
    [SerializeField] Slider sliderMusic;
    [SerializeField] Toggle toggleFullScreen;

    public static OptionMenuController instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }
    void Start()
    {
        string path = Path.Combine(Application.persistentDataPath, "music.txt");
        if (File.Exists(path))
        {
            string volume1 = File.ReadAllText(path);
            volume = float.Parse(volume1);
            print(volume);
        }

        ChangeVolume(volume);
    }

    public void ChangeVolume(float volume)
    {
        sliderMusic.value = volume;
        string path = Path.Combine(Application.persistentDataPath, "music.txt");
        audioMixer.SetFloat("Volume", volume);
        File.WriteAllText(path, volume.ToString());
    }

    public void FullScreen()
    {
        
    }

    public void DropDownChange(int value)
    {
        if (value == 0)
            Screen.SetResolution(3840, 2160, toggleFullScreen.isOn);
        else if (value == 1)
            Screen.SetResolution(2560, 1440, toggleFullScreen.isOn);
        else if (value == 2)
            Screen.SetResolution(1366, 768, toggleFullScreen.isOn);
    }
}
