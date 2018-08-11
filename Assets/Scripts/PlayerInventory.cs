using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    private List<Poo> _poos = null;

	private float _totalWeight = 0.0f;
	float GetTotalWeight()
	{
		return _totalWeight;
	}

    private void Awake()
    {
        _poos = new List<Poo>();
    }

    // Use this for initialization
    void Start () {
		_totalWeight = 0.0f;
	}

	// Update is called once per frame
	void Update () {
	}

    public void AddPoo(Poo poo) {
        _poos.Add(poo);
    }
}
