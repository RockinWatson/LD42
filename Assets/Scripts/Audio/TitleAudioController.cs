using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleAudioController : MonoBehaviour {

    private bool _select() { return (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton4)); }
    public AudioSource titleMusic;
    public AudioSource select;

    private float selectVolume = .5f;

    bool isTitle = true;

	// Use this for initialization
	void Awake () {

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if ((sceneName == "Title01") && (GameObject.Find("StoryController")))

        {
            Destroy(GameObject.Find("StoryController"));
        }

        InitAudio();
                          
    }

    // Update is called once per frame
    void Update () {


        if (_select() && (isTitle))
        {
            StartCoroutine(LoadStory());

        }

        //if (_select() && (isControls))
        //{
        //    StartCoroutine(WaitForGameStart());
            
        //}
    }

    private void InitAudio()
    {
        AudioSource[] audio = GetComponents<AudioSource>();
        titleMusic = audio[0];
        select = audio[1];
        select.playOnAwake = false;
        select.volume = selectVolume;
        titleMusic.Play();
        titleMusic.loop = false;

    }

    IEnumerator LoadStory()
    {
        isTitle = false;
        select.Play();
        titleMusic.Stop();
        Debug.Log("Waiting");
        yield return new WaitForSeconds(4f);
        Debug.Log("Loading Scene");
        SceneManager.LoadScene("CINY1");
    }

}
