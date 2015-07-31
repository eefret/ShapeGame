using UnityEngine;
using System.Collections;

public class RandomSoundPlayer : MonoBehaviour {

    private AudioSource audiosource;

    public AudioClip[] audios;

	// Use this for initialization
	void Start () {
        audiosource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayRandom () {
        audiosource.clip = audios[Random.Range(0, audios.Length)];
        audiosource.Play();
    }
}
