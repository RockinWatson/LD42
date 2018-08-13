using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameControl : MonoBehaviour
    {
        public static GameControl control;

        public static Dictionary<int, Animal.ANIMAL_TYPE> PenInfo = new Dictionary<int, Animal.ANIMAL_TYPE>();
        public static Dictionary<int, Animal.ANIMAL_TYPE> KillList = new Dictionary<int, Animal.ANIMAL_TYPE>();

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
        }

        private void Update()
        {
            var currentScene = SceneManager.GetActiveScene();
            var sceneName = currentScene.name;
            if (sceneName == "Pen1Select")
            {
                //Todo Display Two Random Animals
                //Add to Dict
                //Add to KillList
            }
            else if (sceneName == "Pen2Select")
            {
                //Todo Display Two Random Animals
            }
            else if (sceneName == "Pen3Select")
            {
                //Todo Display Two Random Animals
            }
            else if (sceneName == "Pen4Select")
            {
                //Todo Display Two Random Animals
            }
            else if (sceneName == "Pen5Select")
            {
                //Todo Display Two Random Animals
            }
            else if (sceneName == "Pen6Select")
            {
                //Todo Display Two Random Animals
            }
        }

        public void GetRandomTwoAnimals()
        {

        }

        public void AddToPenOne(int pen, Animal.ANIMAL_TYPE animal)
        {
            PenInfo.Add(0, animal);
        }
        public void AddToPenTwo(int pen, Animal.ANIMAL_TYPE animal)
        {
            PenInfo.Add(1, animal);
        }
        public void AddToPenThree(int pen, Animal.ANIMAL_TYPE animal)
        {
            PenInfo.Add(2, animal);
        }
        public void AddToPenFour(int pen, Animal.ANIMAL_TYPE animal)
        {
            PenInfo.Add(3, animal);
        }
        public void AddToPenFive(int pen, Animal.ANIMAL_TYPE animal)
        {
            PenInfo.Add(4, animal);
        }
        public void AddToPenSix(int pen, Animal.ANIMAL_TYPE animal)
        {
            PenInfo.Add(5, animal);
        }

    }
}
