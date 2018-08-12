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

    [SerializeField]
    private HoldingPinMgr _pinMgr = null;

    [SerializeField]
    private DictionaryOfAnimalPrefabs _animalPrefabs;
    public DictionaryOfAnimalPrefabs GetAnimalPrefabDictionary() {
        return _animalPrefabs;
    }

    [SerializeField]
    private Transform _deckEdge = null;
    public Transform GetDeckEdge() {
        return _deckEdge;
    }
    public bool IsAboveDeck() {
        return (PlayerInventory.GetPlayer().transform.position.y > _deckEdge.position.y);
    }
    public bool IsBelowDeck()
    {
        return !IsAboveDeck();
    }

    static private GameState _this;
    static public GameState Get() {
        return _this;
    }

    private bool _debugAnimalsSpawned = false;

    private void Awake()
    {
        _this = this;
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	private void Update () {
        DebugPlayerInput();
        _currentTime += Time.deltaTime;
        if(_currentTime >= _timePerDay) {
            StartNewDay();
        }
	}

    private void StartNewDay() {
        _currentTime = 0.0f;
    }

    private void DebugPlayerInput() {
        if(Input.GetKeyDown(KeyCode.Return) && !_debugAnimalsSpawned) {
            Debug.Log("SPAWN SOME ANIMALS!!!");
            DebugSpawnAnimals();
        }
    }

    private void DebugSpawnAnimals() {
        List<Animal.ANIMAL_TYPE> animalTypesPicked = new List<Animal.ANIMAL_TYPE>();
        HoldingPin[] pins = _pinMgr.GetPins();
        foreach(HoldingPin pin in pins) {
            //@TODO: Pick random animal
            bool indexFound = false;
            while (!indexFound)
            {
                int index = Random.Range(0, (int)Animal.ANIMAL_TYPE.COUNT);
                Animal.ANIMAL_TYPE animalType = (Animal.ANIMAL_TYPE)index;
                if(!animalTypesPicked.Contains(animalType)) {
                    indexFound = true;
                    animalTypesPicked.Add(animalType);
                    pin.AddAnimalPair(animalType);
                }
            }
        }
        _debugAnimalsSpawned = true;
    }
}
