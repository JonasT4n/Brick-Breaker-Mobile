using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject PauseScreen;
    public GameObject GameOverScreen;
    public bool isPaused = false;
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private TextMeshProUGUI ManyBall;
    public int ScoreInt = 0;
    public int ballCount = 2;
    public int round = 1;

    void Update()
    {
        ScoreText.text = "Score : " + ScoreInt.ToString();
        ManyBall.text = "Balls : " + ballCount.ToString();
    }

    public void GUIButtonPlayAgain()
    {
        SceneManager.LoadScene("GamePlay", LoadSceneMode.Single);
    }

    public void GUIButtonQuitButton()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
