using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {
    
    [SerializeField]
    private float _pickUpRadius = 3.0f;

    [SerializeField]
    private float _weightCapacity = 100.0f;

    [SerializeField]
    private float _vacuumTime = 0.75f;

    [SerializeField]
    private float _vacuumSpeed = 0.05f;

    private List<Poo> _poos = null;
    //private Dictionary<Poo, float> _vacuumList = null;

    private List<Poo> _vacuumList = null;
    //private List<VacuumItem> _vacuumList null;

	private float _totalWeight = 0.0f;
	float GetTotalWeight()
	{
		return _totalWeight;
	}

    private void Awake()
    {
        _poos = new List<Poo>();
        //_vacuumList = new Dictionary<Poo, float>();
        _vacuumList = new List<Poo>();
    }

    // Use this for initialization
    void Start () {
		_totalWeight = 0.0f;
	}

	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.E)) {
            GameState gameState = FindObjectOfType<GameState>();
            if (gameState.GetDeckEdge().position.y >= this.transform.position.y)
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
        colliders = Physics2D.OverlapCircleAll(transform.position, _pickUpRadius);
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
        if (!_vacuumList.Contains(poo))
        {
            _vacuumList.Add(poo);
        }
        //_vacuumList.Add(new VacuumItem(poo, _vacuumTime));
    }

    class VacuumItem {
        public Poo poo;
        public float vacuumTime;
        public VacuumItem(Poo shit, float time) {
            poo = shit;
            vacuumTime = time;
        }
    }

    private void UpdateVacuumList() {
        List<Poo> removeList = new List<Poo>();
        for (int i = 0; i < _vacuumList.Count - 1; ++i) {
            Poo poo = _vacuumList[i];
            Vector3 pos = poo.transform.position;
            poo.transform.position = Vector3.MoveTowards(pos, poo.transform.position, _vacuumSpeed);
            //poo.transform.position = Vector3.Lerp(pos, this.transform.position, 0.5f);
            if(Mathf.Epsilon >= (poo.transform.position - pos).sqrMagnitude) {
                _poos.Add(poo);
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
            poo.gameObject.SetActive(false);
        }
    }

    private void ThrowPoo() {
        //@TODO: Calculate velocity
        Vector3 velocity = this.GetComponent<Rigidbody2D>().velocity;
        //float speed = velocity.magnitude;

        //@TODO: set object pos, Activate
        foreach(Poo poo in _poos) {
            poo.transform.position = this.transform.position;
            poo.gameObject.SetActive(true);

            //@TODO: , send them flying 
            poo.GetComponent<Rigidbody2D>().AddForce(velocity * 50.0f);
        }
        _poos.Clear();
    }

    private void OnBecameInvisible()
    {
        GameObject.Destroy(this.gameObject);
    }
}
