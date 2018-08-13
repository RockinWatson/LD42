using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

    [SerializeField]
    private float _timePerDay = 3.0f;
    private float _currentTime = 0.0f;

    [SerializeField]
    private int _totalDays = 110;
    private int _currentDay = 0;
    private float _dayTimer = 0.0f;
    [SerializeField]
    private TextMesh _daysText = null;

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
    public bool IsAboveDeck(Vector3 pos)
    {
        return (pos.y > _deckEdge.position.y);
    }
    public bool IsAboveDeck() {
        return IsAboveDeck(PlayerInventory.GetPlayer().transform.position);
    }
    public bool IsBelowDeck(Vector3 pos)
    {
        return !IsAboveDeck(pos);
    }
    public bool IsBelowDeck()
    {
        return !IsAboveDeck();
    }
    public bool IsLeftDeck(Vector3 pos) {
        return (pos.x < _deckEdge.position.x);
    }
    public bool IsLeftDeck()
    {
        return IsLeftDeck(PlayerInventory.GetPlayer().transform.position);
    }
    public bool IsRightDeck(Vector3 pos) {
        return !IsLeftDeck(pos);
    }
    public bool IsRightDeck()
    {
        return !IsLeftDeck();
    }

    private int _score = 0;
    public void AddScore(int score) {
        _score += score;
    }
    public int GetScore() {
        return _score;
    }
    [SerializeField]
    private TextMesh _scoreText = null;

    [SerializeField]
    private TextMesh _loadText = null;

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
        //_currentTime += Time.deltaTime;
        //if(_currentTime >= _timePerDay) {
        //    StartNewDay();
        //}
        UpdateScoreText();
        UpdateLoadText();
        UpdateDaysLeft();
	}

    //private void StartNewDay() {
    //    _currentTime = 0.0f;
    //}

    private void UpdateScoreText() {
        _scoreText.text = "Score: " + _score;
    }

    private void UpdateLoadText()
    {
        PlayerInventory inventory = PlayerInventory.GetPlayer();
        int weight = (int)inventory.GetTotalWeight();
        int capacity = (int)inventory.GetWeightCapacity();
        _loadText.text = "Load: " + weight + " / " + capacity;
    }

    private void UpdateDaysLeft()
    {
        _dayTimer += Time.deltaTime;
        _currentDay = (int)(_dayTimer / _timePerDay);
        _daysText.text = "Days Left: " + (_totalDays - _currentDay);
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
