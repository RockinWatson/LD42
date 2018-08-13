using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameControl : MonoBehaviour
    {
        public static GameControl control;

        public Dictionary<int, ANIMAL_TYPE> PenInfo = new Dictionary<int, ANIMAL_TYPE>(); 

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

        public void GetRandomTwoAnimals()
        {

        }

        public void AddToPenOne(int pen, ANIMAL_TYPE animal)
        {

        }
        public void AddToPenTwo(int pen, ANIMAL_TYPE animal)
        {

        }
        public void AddToPenThree(int pen, ANIMAL_TYPE animal)
        {

        }
        public void AddToPenFour(int pen, ANIMAL_TYPE animal)
        {

        }
        public void AddToPenFive(int pen, ANIMAL_TYPE animal)
        {

        }
        public void AddToPenSix(int pen, ANIMAL_TYPE animal)
        {

        }

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
            COUNT,
        };

    }
}
