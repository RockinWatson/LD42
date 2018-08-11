using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour {

    public float dayTimer;
    [SerializeField]
    private TextMesh _timerGui = null;
	
	// Update is called once per frame
	void Update () {

        dayTimer -= Time.deltaTime;
        _timerGui.text = Mathf.Round(dayTimer).ToString();
		
	}
}
