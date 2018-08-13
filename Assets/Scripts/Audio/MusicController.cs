using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

    public AudioSource music;
    public AudioLowPassFilter lowPass;
    float cutOff = 500;

	// Use this for initialization
	void Awake () {
        InitAudio();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameState.Get().IsAboveDeck())
        {
            lowPass.enabled = true;
        }
        else
        {
            lowPass.enabled = false;
        }
	}

    private void InitAudio()
    {
        AudioSource music = GetComponent<AudioSource>();
        AudioLowPassFilter lowPass = GetComponent<AudioLowPassFilter>();

        music.Play();
        music.loop = true;
        music.mute = false;
        lowPass.enabled = true;
        lowPass.cutoffFrequency = cutOff;
    }
}
