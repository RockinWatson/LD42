using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAudioController : MonoBehaviour
{

    public AudioSource music;
    public AudioSource select;
    public AudioSource fart;

    bool _selectCheck = false;

    // Use this for initialization
    void Awake()
    {
        InitAudio();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitAudio()
    {
        AudioSource[] audio = GetComponents<AudioSource>();
        music = audio[0];
        select = audio[1];
        fart = audio[2];

        fart.loop = false;
        select.loop = false;
        music.Play();
        music.loop = false;

        StartCoroutine(WaitForFart());

    }

    IEnumerator WaitForFart()
    {
        yield return new WaitForSeconds(7f);
        fart.Play();
    }
}
