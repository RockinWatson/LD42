using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class EndScore : MonoBehaviour
    {
        [SerializeField]
        private TextMesh _scoreGui;

        public int score;

        private void Start()
        {
            _scoreGui.text = PlayerPrefs.GetInt("highscore").ToString();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton9))
            {
                SceneManager.LoadScene("JoshScene");
            }
        }

    }
}
