using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartController : MonoBehaviour {

    private bool _restart() { return (Input.GetKeyDown(KeyCode.R)) || (Input.GetKeyDown(KeyCode.Joystick1Button9)); }

    private static bool created = false;

    // Use this for initialization
    void Awake () {
        if (!created)
        {
            DontDestroyOnLoad(this);
            created = true;
        }
		
	}
	
	// Update is called once per frame
	void Update () {

        if (_restart())
        {
            SceneManager.LoadScene("Title01");
        }

    }
}
