using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class HUDSettingsScript : MonoBehaviour {

    public Button logoutButton;
    public Button musicButton;
    public Button sfxButton;

    public Slider musicSlider;
    public Slider sfxSlider;

    public AudioMixer audioMixer;
    
    public Sprite[] icons;

	
	public void SetMusicVolume (float volume) {
        audioMixer.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume(float volume) {
        audioMixer.SetFloat("sfxVolume", volume);
    }

    public void LogOut () {
        SimpleAddedFirebase.Instance.LogOut();
    }
}
