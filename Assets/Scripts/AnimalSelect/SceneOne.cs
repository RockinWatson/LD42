using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneOne : MonoBehaviour {

    //private List<GameControl.ANIMAL_TYPE> _animals;

    // Use this for initialization
    void Awake()
    {
        //Get Two from Master List
        var _animals = GameControl.Instance.GetRandomTwoAnimals();
        //Display Both
        GameControl.Instance.DisplayAnimals(_animals);
    }

    private void FixedUpdate()
    {
        
    }

    private void Update()
    {
        var currentScene = SceneManager.GetActiveScene();
        var sceneName = currentScene.name;
        if (sceneName == "Pen1Select")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("Pen2Select");
            }
        }
        if (sceneName == "Pen2Select")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("Pen3Select");
            }
        }
        if (sceneName == "Pen3Select")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("Pen4Select");
            }
        }
        if (sceneName == "Pen4Select")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("Pen5Select");
            }
        }
        if (sceneName == "Pen5Select")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("Pen6Select");
            }
        }
        if (sceneName == "Pen6Select")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("JoshScene");
            }
        }
    }
}
