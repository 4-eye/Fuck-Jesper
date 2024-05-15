using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetVolume : MonoBehaviour 
{
    [SerializeField] Slider volumeSlider;

    void start()
    {
        if (!PlayerPrefs.HasKey("MusicVol"))
        {
            PlayerPrefs.SetFloat("MusicVol", 1);
            Load();
        }

        else
        {
            Load();
        }

    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("MusicVol");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("MusicVol", volumeSlider.value);
    }

}