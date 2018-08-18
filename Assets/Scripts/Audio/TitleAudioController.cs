using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleAudioController : MonoBehaviour {

    private bool _select() { return (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton4)); }
    public AudioSource titleMusic;
    public AudioSource select;
    public AudioSource storyMusic;
    private float selectVolume = .5f;

    bool isTitle = true;
    bool isControls = false;

	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(this);
        InitAudio();

        Scene currentScene = SceneManager.GetActiveScene();

    }

    // Update is called once per frame
    void Update () {

        if (_select())
        {
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;

            switch (sceneName)
            {
                case "CINY1":
                    SceneManager.LoadScene("CINY2");
                    break;
                case "CINY2":
                    SceneManager.LoadScene("CINY3");
                    break;
                case "CINY3":
                    SceneManager.LoadScene("CINY4");
                    break;
                case "CINY4":
                    SceneManager.LoadScene("CINY5");
                    break;
                case "CINY5":
                    SceneManager.LoadScene("CINY5-pick");
                    break;
                case "CINY6":
                    SceneManager.LoadScene("CINY7");
                    break;
                case "CINY7":
                    SceneManager.LoadScene("CINY8");
                    break;
                case "CINY8":
                    SceneManager.LoadScene("Controls01");
                    break;
                case "Controls01":
                    StartCoroutine (WaitForGameStart());
                    break;
            }
        }

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
        storyMusic = audio[2];
        storyMusic.playOnAwake = false;
        storyMusic.loop = true;
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
        storyMusic.Play();

        isControls = true;
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
        storyMusic.Stop();
        Debug.Log("Waiting");
        yield return new WaitForSeconds(3f);
        Debug.Log("Loading Scene");
        SceneManager.LoadScene("JoshScene");
    }
}
