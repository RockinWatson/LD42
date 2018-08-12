using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poo : MonoBehaviour {

    [SerializeField]
    private float _weight = 1.0f;
    public float GetWeight() {
        return _weight;
    }

    public HoldingPin Pin { get; set; }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
