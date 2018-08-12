using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingPinMgr : MonoBehaviour {

    [SerializeField]
	private HoldingPin[] _pins = null;
	public HoldingPin GetPinByIndex(uint index) {
		return _pins[index];
	}
    public HoldingPin[] GetPins() {
        return _pins;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
