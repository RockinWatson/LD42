using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class CloudRando : MonoBehaviour
    {
        //Collect Houses
        public GameObject[] Clouds;

        //Object Pooling
        private List<GameObject> _CloudPool;
        public int CloudAmountTotal;
        public int CloudAmountSingle;

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
        private float _Cloudspeed = 3.0f;
        public float GetCloudspeed()
        {
            return _Cloudspeed;
        }

        static private CloudRando _singleton = null;
        static public CloudRando Get()
        {
            return _singleton;
        }

        private void Awake()
        {
            _singleton = this;
        }

        void Start()
        {
            _CloudPool = new List<GameObject>();
            foreach (var Rain in Clouds)
            {
                for (int j = 0; j < CloudAmountSingle; j++)
                {
                    GameObject obj = Instantiate(Rain);
                    obj.SetActive(false);
                    _CloudPool.Add(obj);
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
            if (_CloudPool != null)
            {
                GameObject gameObj = _CloudPool[Random.Range(0, _CloudPool.Count)];
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
