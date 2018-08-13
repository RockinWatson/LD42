using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningLight : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer _sprite = null;

    private bool _on = false;
    private bool bounce = true;

	// Use this for initialization
	void Start () {
        Deactivate(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Activate() {
        if (!_on)
        {
            _on = true;
            Color color = _sprite.color;
            color.a = 1.0f;
            _sprite.color = color;
        }
    }

    public void Deactivate(bool force = false) {
        if (force || _on)
        {
            _on = false;
            Color color = _sprite.color;
            color.a = 0.0f;
            _sprite.color = color;
        }
    }
}
