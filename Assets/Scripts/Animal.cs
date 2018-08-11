using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour {

    public enum ANIMAL_TYPE {
        RABBIT = 0,
        DRAGON = 1,
        GECKO = 2,
        GRIFFIN = 3,
        DOG = 4,
        CAT = 5,
    };

    [SerializeField]
    private float _pooWeight = 1.0f;

    [SerializeField]
    private float _pooRate = 1.0f;

    [SerializeField]
    private GameObject _pooPrefab = null;

    private HoldingPin _holdingPin = null;
    public void SetHoldingPin(HoldingPin pin) {
        _holdingPin = pin;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeADump() {
        //@TODO: Generate a new poo prefab based on the animal's individual properties (weight, etc.)
    }
}
