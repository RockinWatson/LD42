using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingPin : MonoBehaviour {

    [SerializeField]
    private float _pooCapacity = 10.0f;

    private List<Animal> _animals;
    public List<Animal> GetAnimals() {
        return _animals;
    }

    public void SetAnimal(Animal.ANIMAL_TYPE type) {
        //@TODO: Generate the animal prefabs.

        //@TODO: Assign them to our animals list.
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
