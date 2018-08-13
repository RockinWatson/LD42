using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Animal : MonoBehaviour {
    
    public enum ANIMAL_TYPE {
        BAT = 0,
        CAT = 1,
        DOG = 2,
        DRAGON = 3,
        GOOSE = 4,
        GRIFFIN = 5,
        LLAMA = 6,
        PENGUIN = 7,
        PHOENIX = 8,
        RABBIT = 9,
        SABERTOOTH = 10,
        UNICORN = 11,
        COUNT,
    };

    [SerializeField]
    private float _pooRate = 1.0f;
    private float _pooTimer = 0.0f;

    [SerializeField]
    private GameObject _pooPrefab = null;
    [SerializeField]
    private GameObject[] _pooPrefabs = null;

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
        SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();
        sprite.color = new Color(Random.Range(0.5f, 1.0f), Random.Range(0.5f, 1.0f), Random.Range(0.5f, 1.0f), 1.0f);
        Vector3 scale = transform.localScale;
        scale.x *= ((Random.Range(-1.0f, 1.0f) > 0.0f) ? 1f : -1f);
        transform.localScale = scale;
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
        if(_pooPrefabs.Length > 0) {
            int index = (int)Random.Range(0, _pooPrefabs.Length);
            GameObject pooGO = Instantiate(_pooPrefabs[index], this.transform.position, this.transform.rotation);
            Poo poo = pooGO.GetComponent<Poo>();
            _holdingPin.AddPoo(poo);
            poo.Pin = _holdingPin;
        }
    }

    public void Kill() {
        //@TODO: Kill the animal, set animation to death state, etc.
        _isDead = true;
        SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();
        sprite.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
    }
}

[System.Serializable]
public class DictionaryOfAnimalPrefabs : SerializableDictionary<Animal.ANIMAL_TYPE, GameObject> { }

[CustomPropertyDrawer(typeof(DictionaryOfAnimalPrefabs))]
public class MyDictionaryDrawer2 : DictionaryDrawer<Animal.ANIMAL_TYPE, GameObject> { }
