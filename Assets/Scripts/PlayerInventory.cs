using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    [SerializeField]
    private Transform _tray = null;

    [SerializeField]
    private float _pickUpRadius = 3.0f;

    [SerializeField]
    private float _weightCapacity = 100.0f;
    public float GetWeightCapacity() {
        return _weightCapacity;
    }

    [SerializeField]
    private float _vacuumSpeed = 0.05f;

    [SerializeField]
    private float _throwForce = 250.0f;

    [SerializeField]
    private float _upForce = 1500.0f;

    private List<Poo> _poos = null;
    private List<Poo> _vacuumList = null;

    static private PlayerInventory _this;
    static public PlayerInventory GetPlayer() {
        return _this;
    }

    public bool HasPoo(Poo poo)
    {
        return (_poos.Contains(poo) || _vacuumList.Contains(poo));
    }

	private float _totalWeight = 0.0f;
	public float GetTotalWeight()
	{
		return _totalWeight;
	}
    private void UpdateTotalWeight() {
        _totalWeight = 0;
        foreach(Poo poo in _vacuumList) {
            _totalWeight += poo.GetRigidBody().mass;
        }
        foreach (Poo poo in _poos)
        {
            _totalWeight += poo.GetRigidBody().mass;
        }
    }

    private void Awake()
    {
        _this = this;
        _poos = new List<Poo>();
        _vacuumList = new List<Poo>();
    }

    // Use this for initialization
    void Start () {
		_totalWeight = 0.0f;
	}

    private void Update()
    {
        UpdatePooPos();
    }

    // Update is called once per frame
    void FixedUpdate () {
        if(Input.GetKeyDown(KeyCode.E)) {
            GameState gameState = FindObjectOfType<GameState>();
            if (gameState.GetDeckEdge().position.y >= _tray.position.y)
            {
                Debug.Log("PICK UP POO!!");
                VacuumUpPoo();
            } else {
                //@TODO: Throw poo
                ThrowPoo();
            }
        }
        UpdateVacuumList();
	}

    private void VacuumUpPoo() {
        Collider2D[] colliders;
        colliders = Physics2D.OverlapCircleAll(_tray.position, _pickUpRadius);
        if(colliders.Length > 0) {
            Debug.Log("FOUND SOME SHIT!");
        }
        for (int i = 0; i <= colliders.Length- 1; ++i) {
            Poo poo = colliders[i].gameObject.GetComponent<Poo>();
            if(poo) {
                Debug.Log("GETTIN POO HITS!!");
                float pooWeight = poo.GetWeight();
                if(_weightCapacity - _totalWeight - pooWeight >= 0) {
                    AddPoo(poo);
                }
            }
        }
    }

    private void AddPoo(Poo poo) {
        if(!HasPoo(poo))
        {
            SoundController.pickup.Play();
            _vacuumList.Add(poo);
            UpdateTotalWeight();
        }
    }

    private void UpdateVacuumList() {
        List<Poo> removeList = new List<Poo>();
        for (int i = 0; i < _vacuumList.Count; ++i) {   
            Poo poo = _vacuumList[i];
            if (poo)
            {
                Vector3 pos = poo.transform.position;
                poo.transform.position = Vector3.MoveTowards(pos, poo.transform.position, _vacuumSpeed * Time.fixedDeltaTime);
                //poo.transform.position = Vector3.Lerp(pos, this.transform.position, 0.5f);
                if (Mathf.Epsilon >= (poo.transform.position - pos).sqrMagnitude)
                {
                    _poos.Add(poo);
                    removeList.Add(poo);
                }
            } else {
                removeList.Add(poo);
            }
        }
        foreach (Poo poo in removeList)
        {
            _vacuumList.Remove(poo);
            if (poo.Pin)
            {
                poo.Pin.RemovePoo(poo);
            }
        }
    }

    private void ThrowPoo() {
        //@TODO: Calculate velocity
        Vector3 velocity = this.GetComponent<Rigidbody2D>().velocity;
        //float speed = velocity.magnitude;
        Vector3 dir = _tray.position - FindObjectOfType<GameState>().GetDeckEdge().position;

        Vector3 forceDir = Vector3.Slerp(velocity, dir, 0.75f);

        //@TODO: set object pos, Activate
        foreach(Poo poo in _poos) {
            poo.transform.position = _tray.position;
            poo.transform.localScale = Vector3.one;
            poo.gameObject.SetActive(true);
            poo.Throw();

            //@TODO: , send them flying 
            poo.GetComponent<Rigidbody2D>().AddForce(forceDir * _throwForce);
            poo.GetComponent<Rigidbody2D>().AddForce(Vector3.up * _upForce);
        }
        _poos.Clear();
        SoundController.toss.Play();
        UpdateTotalWeight();
    }

    private void UpdatePooPos() {
        int pooCount = _poos.Count;
        if (pooCount > 0)
        {
            const float pileHeight = 1.0f;
            Vector3 trayBase = _tray.transform.position;
            Vector3 pileTop = trayBase + (_tray.up * pileHeight);
            for (int i = 0; i < pooCount; ++i)
            {
                Poo poo = _poos[i];
                poo.transform.localScale = Vector3.one * 0.6f;
                poo.transform.position = Vector3.Lerp(trayBase, pileTop, i / (float)pooCount);
            }
        }
    }
}
