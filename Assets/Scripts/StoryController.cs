using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryController : MonoBehaviour {

    private bool _select() { return (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton4)); }
    public AudioSource select;
    public AudioSource storyMusic;

    private float selectVolume = .5f;

    bool isControls = false;

    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(this);
        InitAudio();

        Scene currentScene = SceneManager.GetActiveScene();

    }

    // Update is called once per frame
    void Update()
    {

        if (_select() && (isControls == false))
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
                    StartCoroutine(WaitForGameStart());
                    break;
            }
        }

    }

    private void InitAudio()
    {
        AudioSource[] audio = GetComponents<AudioSource>();
        select = audio[0];
        storyMusic = audio[1];
        storyMusic.playOnAwake = true;
        storyMusic.loop = true;
        select.playOnAwake = false;
        select.volume = selectVolume;


    }

    IEnumerator WaitForGameStart()
    {
        isControls = true;
        select.Play();
        storyMusic.Stop();
        Debug.Log("Waiting");
        yield return new WaitForSeconds(3f);
        Debug.Log("Loading Scene");
        SceneManager.LoadScene("JoshScene");
    }
}
