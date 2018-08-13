using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {


    public AudioSource ambience;
    public AudioLowPassFilter lowPass;
    float startVolume = .7f;
    float cutOff = 500;

    // Use this for initialization
    void Start () {
        InitAudio();            
	}
	
	// Update is called once per frame
	void Update () {

        if (GameState.Get().IsAboveDeck())
        {
            //ambience.mute = false;
            lowPass.enabled = false;
        }
        else
        {
            //ambience.mute = true;
            lowPass.enabled = true;
        }

	}

    private void InitAudio()
    {
        AudioSource[] audio = GetComponents<AudioSource>();
        AudioLowPassFilter lowPass = GetComponent<AudioLowPassFilter>();
        ambience = audio[0];
        //ambience.mute = true;
        ambience.loop = true;
        ambience.volume = startVolume;
        ambience.Play();

        lowPass.enabled = false;
        lowPass.cutoffFrequency = cutOff;
    }
}
