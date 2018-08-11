using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class RainRando : MonoBehaviour
    {
        //Collect Houses
        public GameObject[] Rains;

        //Object Pooling
        private List<GameObject> _RainPool;
        public int RainAmountTotal;
        public int RainAmountSingle;

        //Time to spawn
        public float waitForNextMax;
        public float countDown;

        //X Range
        public float xMin;
        public float xMax;

        //Y Range
        public float yMin;
        public float yMax;

        [SerializeField]
        private float _RainSpeed = 3.0f;
        public float GetRainSpeed()
        {
            return _RainSpeed;
        }

        static private RainRando _singleton = null;
        static public RainRando Get()
        {
            return _singleton;
        }

        private void Awake()
        {
            _singleton = this;
        }

        void Start()
        {
            _RainPool = new List<GameObject>();
            foreach (var Rain in Rains)
            {
                for (int j = 0; j < RainAmountSingle; j++)
                {
                    GameObject obj = Instantiate(Rain);
                    obj.SetActive(false);
                    _RainPool.Add(obj);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            countDown -= Time.deltaTime;
            if (countDown <= 0)
            {
                SpawnRain();
                countDown = waitForNextMax;
            }
        }

        void SpawnRain()
        {
            if (_RainPool != null)
            {
                GameObject gameObj = _RainPool[Random.Range(0, _RainPool.Count)];
                if (!gameObj.activeInHierarchy)
                {
                    gameObj.transform.position = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), -5);
                    gameObj.SetActive(true);
                }
            }
            else
            {
                Debug.Log("Add Rain Prefabs to the RainRando Script you DumbAss!!!!");
            }
        }
    }
}
