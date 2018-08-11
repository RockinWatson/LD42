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

    public void SetAnimal(Animal.ANIMAL_TYPE type) {
        //@TODO: Generate the animal prefabs.

        //@TODO: Assign them to our animals list.
    }

    private void Awake()
    {
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
        return _totalPooWeight;
    }

    private void UpdatePooCapacityState() {
        if(_totalPooWeight >= _pooCapacityKill) {
            //@TODO: Start killing animals.
            if (_killAnimalTimer >= _timeBetweenAnimalKills)
            {
                foreach (Animal animal in _animals)
                {
                    animal.Kill();
                    //@TODO: We only want to kill one at a time for a while. Setup a timer.
                    _killAnimalTimer = 0.0f;
                    break;
                }
            }
            _killAnimalTimer += Time.deltaTime;
        } else {
            _killAnimalTimer = 0.0f;
        }
    }
}
