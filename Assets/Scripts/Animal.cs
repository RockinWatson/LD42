using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Animal : MonoBehaviour {

    [Serializable]
    public enum ANIMAL_TYPE {
        BAT = 0,
        CAT = 1,
        DOG = 2,
        DRAGON = 3,
        GECKO = 4,
        GOOSE = 5,
        GRIFFIN = 6,
        LLAMA = 7,
        PENGUIN = 8,
        PHOENIX = 9,
        RABBIT = 10,
        SABERTOOTH = 11,
        UNICORN = 12,
        COUNT,
    };

    [SerializeField]
    private float _pooRate = 1.0f;
    private float _pooTimer = 0.0f;

    [SerializeField]
    private GameObject _pooPrefab = null;

    [SerializeField]
    private uint _keepAliveScore = 10;

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
	virtual public void Update () {
        if(IsAlive()) {
            //@TODO: Do alive behavior.
            UpdatePoo();
        } else {
            //@TODO: Do dead behavior.
        }
	}

    private void UpdatePoo() {
        if(_pooTimer >= _pooRate) {
            TakeADump();
            _pooTimer = 0.0f;
        }
        _pooTimer += Time.deltaTime;
    }

    private void TakeADump() {
        //@TODO: Generate a new poo prefab based on the animal's individual properties (weight, etc.)
        if(_pooPrefab) {
            GameObject pooGO = Instantiate(_pooPrefab, this.transform.position, this.transform.rotation);
            Poo poo = pooGO.GetComponent<Poo>();
            _holdingPin.AddPoo(poo);
        }
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
