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

    [SerializeField]
    private WarningLight _warning = null;

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

    public void RemovePoo(Poo poo)
    {
        _poos.Remove(poo);
        UpdateTotalPooWeight();
    }

    public Poo PopPoo() {
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
        //Debug.Log("Total Poo Weight: " + _totalPooWeight);
        return _totalPooWeight;
    }

    private void UpdatePooCapacityState() {
        if(_totalPooWeight >= _pooCapacityKill) {
            _warning.Activate();
            //@TODO: Start killing animals.
            //Debug.Log("WE AT POO CAPACITY!");
            if (_killAnimalTimer >= _timeBetweenAnimalKills)
            {
                //Debug.Log("KILL TIMER REACHED!");
                foreach (Animal animal in _animals)
                {
                    if (animal.IsAlive())
                    {
                        //Debug.Log("KILL AN ANIMAL!");
                        animal.Kill();
                        //@TODO: We only want to kill one at a time for a while. Setup a timer.
                        _killAnimalTimer = 0.0f;
                        break;
                    }
                }
            }
            _killAnimalTimer += Time.deltaTime;
        } else {
            _warning.Deactivate();
            //Debug.Log("WE UNDER POO CAPACITY NOW!");
            _killAnimalTimer = 0.0f;
        }
    }

    private Vector3 GetNewAnimalSpawnPos() {
        const float randomEdge = 1.0f;
        return this.transform.position + Vector3.right * Random.Range(-1.0f, 1.0f) * randomEdge;
    }

    public void AddAnimal(Animal.ANIMAL_TYPE type, bool cinematic = false)
    {
        //@TODO: Retrieve appropriate prefab for animal type.
        DictionaryOfAnimalPrefabs prefabDict;
        if(AnimalPicker.Get()) {
            prefabDict = AnimalPicker.Get().GetAnimalPrefabDictionary();
        } else {
            prefabDict = GameState.Get().GetAnimalPrefabDictionary();
        }
        GameObject prefab = prefabDict[type];
        
        //@TODO: Generate the animal prefabs.
        GameObject animalGO = Instantiate(prefab, GetNewAnimalSpawnPos(), this.transform.rotation);
        Animal animal = animalGO.GetComponent<Animal>();
        if(cinematic) {
            animal.SetCinematic();
        }

        //@TODO: Assign them to our animals list.
        _animals.Add(animal);
        animal.SetHoldingPin(this);
    }

    public void AddAnimalPair(Animal.ANIMAL_TYPE type, bool cinematic=false)
    {
        AddAnimal(type, cinematic);
        AddAnimal(type, cinematic);

        //@TODO: Verify distance is enough.
        int animalCount = _animals.Count;
        Animal animal0 = _animals[animalCount - 2];
        Animal animal1 = _animals[animalCount - 1];
        Vector3 animal0pos = animal0.transform.position;
        Vector3 animal1pos = animal1.transform.position;
        Vector3 dir = animal0pos  - animal1pos;
        float minPairDistance = animal0.GetScale().x * 2.0f;
        if(dir.magnitude < minPairDistance) {
            animal0.transform.position = (animal0pos + dir.normalized * minPairDistance / 2.0f);
            animal1.transform.position = (animal1pos - dir.normalized * minPairDistance / 2.0f);
        }
    }

    public void DestroyAnimals() {
        //Debug.Log("WE 'BOUT TO DESRTROY SOME ANIMALS!");
        for (int i = 0; i < _animals.Count; ++i) {
            Animal animal = _animals[i];
            GameObject.Destroy(animal.gameObject);
        }
        _animals.Clear();
    }
}
