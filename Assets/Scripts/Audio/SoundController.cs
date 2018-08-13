using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {


    public AudioSource ambience;
    public static AudioSource jump;
    public static AudioSource pickup;
    public static AudioSource toss;

    //public AudioLowPassFilter lowPass;
    float startVolume = .7f;
    float cutOff = 500;

    // Use this for initialization
    void Start () {
        InitAudio();            
	}
	
	// Update is called once per frame
	void Update () {


	}

    private void InitAudio()
    {
        AudioSource[] audio = GetComponents<AudioSource>();
        ambience = audio[0];
        jump = audio[1];
        pickup = audio[2];
        toss = audio[3];

        //ambience.mute = true;
        ambience.loop = true;
        ambience.volume = startVolume;
        ambience.Play();


    }
}
