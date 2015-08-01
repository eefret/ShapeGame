using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;
using GameStructures;

public class ScrollBarMixer : MonoBehaviour {

    // Public
    public AudioMixer mixer;

    public ScrollType scrollType;
    public enum ScrollType { Music, Sound }

    // Private
    private Scrollbar scrollbar;

	// Use this for initialization
	void Start () {
        scrollbar = GetComponent<Scrollbar>();

        if(scrollbar != null) {
            switch (scrollType) {
                case ScrollType.Music:
                    scrollbar.value = PlayerPrefs.GetFloat(DataPrefs.MusicVolume, 1);
                    break;
                case ScrollType.Sound:
                    scrollbar.value = PlayerPrefs.GetFloat(DataPrefs.SoundVolume, 1);
                    break;
                default:
                    break;
            }
        }

        float volume = PlayerPrefs.GetFloat(DataPrefs.MusicVolume, 1);
        mixer.SetFloat("Music", (volume - 1) * 60f);
        volume = PlayerPrefs.GetFloat(DataPrefs.SoundVolume, 1);
        mixer.SetFloat("Sound", (volume - 1) * 60f);
    }
    
    // Update is called once per frame
	public void OnValueChange () {        
        if(scrollbar != null && mixer != null) {
            switch (scrollType) {
                case ScrollType.Music:
                    mixer.SetFloat("Music", (scrollbar.value - 1) * 60f);
                    PlayerPrefs.SetFloat(DataPrefs.MusicVolume, scrollbar.value);
                    break;
                case ScrollType.Sound:
                    mixer.SetFloat("Sound", (scrollbar.value - 1) * 60f);
                    PlayerPrefs.SetFloat(DataPrefs.SoundVolume, scrollbar.value);
                    break;
                default:
                    break;
            }
        }
	}
}
