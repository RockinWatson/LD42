using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimalPicker : MonoBehaviour {

    [SerializeField]
    private DictionaryOfAnimalPrefabs _animalPrefabs;
    public DictionaryOfAnimalPrefabs GetAnimalPrefabDictionary()
    {
        return _animalPrefabs;
    }

    [SerializeField]
    private DictionaryOfAnimalNames _animalNames;
    public DictionaryOfAnimalNames GetAnimalNameDictionary()
    {
        return _animalNames;
    }

    [SerializeField]
    private HoldingPin _leftBin = null;
    [SerializeField]
    private HoldingPin _rightBin = null;

    [SerializeField]
    private TextMesh _leftText = null;
    [SerializeField]
    private TextMesh _rightText = null;
    [SerializeField]
    private GameObject _hud = null;

    private Animal.ANIMAL_TYPE _leftType;
    private Animal.ANIMAL_TYPE _rightType;

    private List<Animal.ANIMAL_TYPE> _picks = null;
    public List<Animal.ANIMAL_TYPE> GetPicks() {
        return _picks;
    }
    private List<Animal.ANIMAL_TYPE> _availablePicks = null;
    private List<Animal.ANIMAL_TYPE> _unpicked = null;
    public List<Animal.ANIMAL_TYPE> GetUnpicked()
    {
        return _unpicked;
    }
    private int _randomIndex = 0;

    private int _animalPickIndex = 0;

    private bool LeftKeyDown() {
        return Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.JoystickButton0);
    }
    private bool RightKeyDown()
    {
        return Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.JoystickButton3);
    }

    private bool _isDonePicking = false;

    static private AnimalPicker _this;
    static public AnimalPicker Get()
    {
        return _this;
    }

    private void Awake()
    {
        if (!_this)
        {
            _this = this;
            DontDestroyOnLoad(_this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }

    // called first
    void OnEnable()
    {
        _picks = new List<Animal.ANIMAL_TYPE>();
        _availablePicks = new List<Animal.ANIMAL_TYPE>();
        _unpicked = new List<Animal.ANIMAL_TYPE>();
        
        //Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("OnSceneLoaded: " + scene.name);
        //Debug.Log(mode);
        if(scene.name == "CINY5-pick") {
            SetupBoard();
        }
    }

    // Use this for initialization
    void Start () {
        //_picks = new List<Animal.ANIMAL_TYPE>();
        //_availablePicks = new List<Animal.ANIMAL_TYPE>();

        //@TODO: Setup starting state and board
        //SetupBoard();
	}

    // called when the game is terminated
    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
	
	// Update is called once per frame
	void Update () {
        if (!_isDonePicking)
        {
            ProcessInput();
        }
	}

    private void ProcessInput() {
        //KeyCode keyCode = Input.key
        //switch(Input.GetKeyDown())
        if (LeftKeyDown())
        {
            _unpicked.Add(_rightType);
            MakePick(_leftType);
        }
        else if (RightKeyDown())
        {
            _unpicked.Add(_leftType);
            MakePick(_rightType);
        }
    }

    private void MakePick(Animal.ANIMAL_TYPE type) {

        _picks.Add(type);
        _leftBin.DestroyAnimals();
        _rightBin.DestroyAnimals();
        ++_animalPickIndex;
        if(_animalPickIndex == 6) {
            FinishPicking();
            return;
        }

        PromptRandomAnimals();
    }

    private void FinishPicking() {
        //@TODO: Launch next cinematic scene.
        _isDonePicking = true;
        _hud.SetActive(false);
        SceneManager.LoadScene("CINY5-flood");
    }

    private void SetupBoard() {
        _hud.SetActive(true);
        _isDonePicking = false;
        _randomIndex = 0;
        _animalPickIndex = 0;
        _picks.Clear();
        _availablePicks.Clear();
        _unpicked.Clear();
        //@TODO: Populate Available Picks
        _availablePicks.AddRange(System.Enum.GetValues(typeof(Animal.ANIMAL_TYPE)) as IEnumerable<Animal.ANIMAL_TYPE>);
        _availablePicks.Remove(Animal.ANIMAL_TYPE.COUNT);
        for (int i = 0; i < _availablePicks.Count; i++)
        {
            Animal.ANIMAL_TYPE temp = _availablePicks[i];
            int randomIndex = Random.Range(i, _availablePicks.Count);
            _availablePicks[i] = _availablePicks[randomIndex];
            _availablePicks[randomIndex] = temp;
        }
        //_availablePicks.Sort((a, b) => (int)(1.0f - 2.0f * Random.Range(0.0f, 1.0f)));

        PromptRandomAnimals();
    }

    private void PromptRandomAnimals() {
        _leftType = GetPseudoRandomAvailablePick();
        _rightType = GetPseudoRandomAvailablePick();

        _leftText.text = _animalNames[_leftType];
        _rightText.text = _animalNames[_rightType];

        SpawnAnimals(_leftType, _rightType);
    }

    private void SpawnAnimals(Animal.ANIMAL_TYPE leftType, Animal.ANIMAL_TYPE rightType)
    {
        _leftBin.AddAnimalPair(leftType);
        _rightBin.AddAnimalPair(rightType);
        SetAnimalProperties();
    }

    private void SetAnimalProperties() {
        Animal[] animals = GameObject.FindObjectsOfType<Animal>();
        foreach(Animal animal in animals) {
            animal.SetCinematic();
            animal.GetComponent<Rigidbody2D>().simulated = false;
        }
    }

    public Animal.ANIMAL_TYPE GetPseudoRandomAvailablePick() {
        return _availablePicks[_randomIndex++];
        //Animal.ANIMAL_TYPE type = _availablePicks[0];
        //_availablePicks.RemoveAt(0);
        //return type;
    }

    private Animal.ANIMAL_TYPE GetRandomAvailablePick() {
        int randomPick = Random.Range(0, (int)Animal.ANIMAL_TYPE.COUNT);
        Animal.ANIMAL_TYPE animalType = (Animal.ANIMAL_TYPE)randomPick;
        bool indexFound = false;
        while (!indexFound) {
            if (!_picks.Contains(animalType)) {
                    indexFound = true;
                    _picks.Add(animalType);
            }
        }
        return animalType;
    }

    IEnumerator WaitForPick()
    {
        yield return new WaitForSeconds(2f);
    }
}
