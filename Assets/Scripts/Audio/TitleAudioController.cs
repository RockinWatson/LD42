using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleAudioController : MonoBehaviour {

    private bool _select() { return (Input.GetKeyDown(KeyCode.Return)); }
    public AudioSource titleMusic;
    public AudioSource select;
    private float selectVolume = .5f;

    bool isTitle = true;
    bool isControls = false;

	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(this);
        InitAudio();
	}
	
	// Update is called once per frame
	void Update () {

        if (_select() && (isTitle))
        {
            StartCoroutine(WaitForNextScene());

        }

        if (_select() && (isControls))
        {
            StartCoroutine(WaitForGameStart());
            
        }
    }

    private void InitAudio()
    {
        AudioSource[] audio = GetComponents<AudioSource>();
        titleMusic = audio[0];
        select = audio[1];
        select.playOnAwake = false;
        select.volume = selectVolume;
        titleMusic.Play();
        titleMusic.loop = true;

    }

    IEnumerator WaitForNextScene()
    {
        isTitle = false;
        select.Play();
        //titleMusic.Stop();
        Debug.Log("Waiting");
        yield return new WaitForSeconds(.5f);
        Debug.Log("Loading Scene");
        SceneManager.LoadScene("Controls01");

        isControls = true;
    }

    IEnumerator WaitForGameStart()
    {
        isControls = false;
        select.Play();
        titleMusic.Stop();
        Debug.Log("Waiting");
        yield return new WaitForSeconds(3f);
        Debug.Log("Loading Scene");
        SceneManager.LoadScene("JoshScene");
    }
}
