using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingPin : MonoBehaviour {

    [SerializeField]
    private float _pooCapacityKill = 15.0f;

    private List<Animal> _animals;
    public List<Animal> GetAnimals() {
        return _animals;
    }

    private List<Poo> _poos = null;
    private float _totalPooWeight = 0.0f;

    [SerializeField]
    private float _timeBetweenAnimalKills = 10.0f;
    private float _killAnimalTimer = 0.0f;

    private void Awake()
    {
        _animals = new List<Animal>();
        _poos = new List<Poo>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //@TODO: Verify we aren't over capacity, if we are, kill animals.
        UpdatePooCapacityState();
	}

    public void AddPoo(Poo poo) {
        _poos.Add(poo);
        UpdateTotalPooWeight();
    }

    public Poo RemovePoo() {
        Poo poo = _poos[0];
        _poos.RemoveAt(0);
        UpdateTotalPooWeight();
        return poo;
    }

    private float UpdateTotalPooWeight() {
        _totalPooWeight = 0;
        foreach (Poo poo in _poos) {
            _totalPooWeight += poo.GetWeight();
        }
        Debug.Log("Total Poo Weight: " + _totalPooWeight);
        return _totalPooWeight;
    }

    private void UpdatePooCapacityState() {
        if(_totalPooWeight >= _pooCapacityKill) {
            //@TODO: Start killing animals.
            Debug.Log("WE AT POO CAPACITY!");
            if (_killAnimalTimer >= _timeBetweenAnimalKills)
            {
                Debug.Log("KILL TIMER REACHED!");
                foreach (Animal animal in _animals)
                {
                    if (animal.IsAlive())
                    {
                        Debug.Log("KILL AN ANIMAL!");
                        animal.Kill();
                        //@TODO: We only want to kill one at a time for a while. Setup a timer.
                        _killAnimalTimer = 0.0f;
                        break;
                    }
                }
            }
            _killAnimalTimer += Time.deltaTime;
        } else {
            Debug.Log("WE UNDER POO CAPACITY NOW!");
            _killAnimalTimer = 0.0f;
        }
    }

    public void AddAnimal(Animal.ANIMAL_TYPE type)
    {
        //@TODO: Retrieve appropriate prefab for animal type.
        GameState gameState = FindObjectOfType<GameState>();
        DictionaryOfAnimalPrefabs prefabDict = gameState.GetAnimalPrefabDictionary();
        GameObject prefab = prefabDict[type];
        
        //@TODO: Generate the animal prefabs.
        GameObject animalGO = Instantiate(prefab, this.transform.position, this.transform.rotation);
        Animal animal = animalGO.GetComponent<Animal>();

        //@TODO: Assign them to our animals list.
        _animals.Add(animal);
        animal.SetHoldingPin(this);
    }

    public void AddAnimalPair(Animal.ANIMAL_TYPE type)
    {
        AddAnimal(type);
        AddAnimal(type);
    }
}
