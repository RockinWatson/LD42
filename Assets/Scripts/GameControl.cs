using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameControl : MonoBehaviour
    {
        public static GameControl control;
        public static GameControl Instance { get { return control; } }

        [SerializeField]
        private DictionaryOfAnimalPrefabs _animalPrefabs;
        public DictionaryOfAnimalPrefabs GetAnimalPrefabDictionary()
        {
            return _animalPrefabs;
        }

        public static List<int> AnimalList = new List<int>();
        public static List<ANIMAL_TYPE> PenInfo = new List<ANIMAL_TYPE>();
        public static List<ANIMAL_TYPE> KillList = new List<ANIMAL_TYPE>();

        private void Awake()
        {
            if (control == null)
            {
                DontDestroyOnLoad(gameObject);
                control = this;
            }
            else if (control != this)
            {
                Destroy(gameObject);
            }
            var currentScene = SceneManager.GetActiveScene();
            var sceneName = currentScene.name;
            if (sceneName == "Pen1Select")
            {
                //For Restart Purpose
                foreach (ANIMAL_TYPE animal in (ANIMAL_TYPE[])Enum.GetValues(typeof(ANIMAL_TYPE)))
                {
                    AnimalList.Add((int)animal);
                }
            }
        }

        public List<ANIMAL_TYPE> GetRandomTwoAnimals()
        {
            List<ANIMAL_TYPE> randList = new List<ANIMAL_TYPE>();
            for (int i = 0; i < 2; i++)
            {
                bool indexFound = false;
                while (!indexFound)
                {
                    int index = UnityEngine.Random.Range(0, AnimalList.Count);
                    ANIMAL_TYPE animalType = (ANIMAL_TYPE)index;
                    if (!randList.Contains(animalType))
                    {
                        indexFound = true;
                        randList.Add(animalType);
                        AnimalList.RemoveAt((int)animalType);
                    }
                }
            }
            return randList;
        }

        public void AddToPenInfo(ANIMAL_TYPE animal)
        {
            PenInfo.Add(animal);
        }
        public void AddToKillList(ANIMAL_TYPE animal)
        {
            KillList.Add(animal);
        }

        public void DisplayAnimals(List<ANIMAL_TYPE> animals)
        {
            ANIMAL_TYPE type1 = animals[0];
            ANIMAL_TYPE type2 = animals[1];
            GameControl gameControl = FindObjectOfType<GameControl>();
            DictionaryOfAnimalPrefabs prefabDict = gameControl.GetAnimalPrefabDictionary();
            GameObject prefab1 = prefabDict[type1];
            GameObject prefab2 = prefabDict[type2];

            //Add First Animal
            var prefab1SprRend = prefab1.GetComponent<SpriteRenderer>();
            var posOneSprRend = GameObject.Find("AnimalPositionSelect1").GetComponent<SpriteRenderer>();
            posOneSprRend.sprite = prefab1SprRend.sprite;

            //Add Second Animal
            var prefab2SprRend = prefab2.GetComponent<SpriteRenderer>();
            var posTwoSprRend = GameObject.Find("AnimalPositionSelect2").GetComponent<SpriteRenderer>();
            posTwoSprRend.sprite = prefab2SprRend.sprite;
        }

        [Serializable]
        public class DictionaryOfAnimalPrefabs : SerializableDictionary<ANIMAL_TYPE, GameObject> { }

        #if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(DictionaryOfAnimalPrefabs))]
        public class MyDictionaryDrawer2 : DictionaryDrawer<ANIMAL_TYPE, GameObject> { }
        #endif

        public enum ANIMAL_TYPE
        {
            BAT = 0,
            CAT = 1,
            DOG = 2,
            DRAGON = 3,
            GOOSE = 4,
            GRIFFIN = 5,
            LLAMA = 6,
            PENGUIN = 7,
            PHOENIX = 8,
            RABBIT = 9,
            SABERTOOTH = 10,
            UNICORN = 11,
        };
    }
}
