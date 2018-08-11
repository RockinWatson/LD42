using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Animal : MonoBehaviour {

    [Serializable]
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

    private bool _isDead = false;
    public bool IsDead() {
        return _isDead;
    }
    public bool IsAlive()
    {
        return !_isDead;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(IsAlive()) {
            //@TODO: Do alive behavior.
        } else {
            //@TODO: Do dead behavior.
        }
	}

    public void TakeADump() {
        //@TODO: Generate a new poo prefab based on the animal's individual properties (weight, etc.)
    }

    public void Kill() {
        //@TODO: Kill the animal, set animation to death state, etc.
        _isDead = true;
    }
}

[Serializable]
public class DictionaryOfAnimalPrefabs : SerializableDictionary<Animal.ANIMAL_TYPE, GameObject> { }

[CustomPropertyDrawer(typeof(DictionaryOfAnimalPrefabs))]
public class MyDictionaryDrawer2 : DictionaryDrawer<Animal.ANIMAL_TYPE, GameObject> { }
