using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

    [SerializeField]
    private float _timePerDay = 60.0f;
    private float _currentTime = 0.0f;

    [SerializeField]
    private uint _totalDays = 40;
    private uint _currentDay = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	private void Update () {
        _currentTime += Time.deltaTime;
        if(_currentTime >= _timePerDay) {
            StartNewDay();
        }
	}

    private void StartNewDay() {
        _currentTime = 0.0f;
    }
}
