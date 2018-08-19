using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimalDestroyer : MonoBehaviour {

    [SerializeField]
    HoldingPin[] _holdingPins = null;

    [SerializeField]
    SpriteRenderer _wave = null;

    private float _loadSceneTime = 6.0f;
    private float _loadSceneTimer = 0.0f;

	// Use this for initialization
	void Start () {
        SpawnAnimals();
	}

    private void SpawnAnimals() {
        const int unpickedBaseIndex = 0;
        List<Animal.ANIMAL_TYPE> unpicked = AnimalPicker.Get().GetUnpicked();
        for (int i = 0; i < _holdingPins.Length; ++i) {
            Animal.ANIMAL_TYPE type = unpicked[unpickedBaseIndex+i];
            Debug.Log(type);
            //Animal.ANIMAL_TYPE type = AnimalPicker.Get().GetPseudoRandomAvailablePick();
            _holdingPins[i].AddAnimalPair(type);
            foreach(Animal animal in _holdingPins[i].GetAnimals()) {
                animal.SetCinematic();
                animal.GetComponent<Rigidbody2D>().simulated = false;
                animal.gameObject.layer = 0;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        _wave.transform.position = _wave.transform.position + Vector3.up * Time.deltaTime * 0.75f;

        if(_loadSceneTimer > _loadSceneTime) {
            SceneManager.LoadScene("CINY6");
        }
        _loadSceneTimer += Time.deltaTime;
	}
}
