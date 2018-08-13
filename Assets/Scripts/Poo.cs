using System;
using UnityEngine;

public class Poo : MonoBehaviour, IComparable<Poo> {

    [SerializeField]
    private float _weight = 1.0f;
    public float GetWeight() {
        return _weight;
    }

    private Rigidbody2D _rigidBody = null;
    public Rigidbody2D GetRigidBody() {
        return _rigidBody;
    }

    private bool _thrown = false;

    public HoldingPin Pin { get; set; }

    private void Awake()
    {
        _rigidBody = this.GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {
        this.GetComponent<Rigidbody2D>().mass = _weight / 10.0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(_thrown) {
            float direction = GameState.Get().IsLeftDeck(this.transform.position) ? -1.0f : 1.0f;
            //@TODO: , send them flying 
            this.GetComponent<Rigidbody2D>().AddForce(Vector3.right * 5000.0f * Time.fixedDeltaTime * direction);
        }
	}

    public void Throw() {
        _thrown = true;
    }

    private void OnBecameInvisible()
    {
        if (_thrown)
        {
            GameState.Get().AddScore((int)_weight);
            GameObject.Destroy(this.gameObject);
        }
    }

    public int CompareTo(Poo other) {
        return (int)(other._weight - this._weight);
    }
}
