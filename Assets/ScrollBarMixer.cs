using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

public class ScrollBarMixer : MonoBehaviour {

    public AudioMixer mixer;

    public ScrollType scrollType;

    public enum ScrollType { Music, Sound }

    private Scrollbar scrollbar;

	// Use this for initialization
	void Start () {
        scrollbar = GetComponent<Scrollbar>();
	}
	
	// Update is called once per frame
	void Update () {
        if(mixer != null) {
            switch (scrollType) {
            case ScrollType.Music:
                mixer.SetFloat("Music", (scrollbar.value - 1) * 60f);
                break;
            case ScrollType.Sound:
                mixer.SetFloat("Sound", (scrollbar.value - 1) * 60f);
                break;
            default:
                break;
            }
        }
	}
}
