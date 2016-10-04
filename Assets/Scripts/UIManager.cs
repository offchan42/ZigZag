using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject mainCanvas;
    public GameObject gameOverCanvas;
    public Animator tapTextAnimator;
    public Animator panelAnimator;
//    public Text scoreText;
    public Text scoreLiteral;
    public Text highScoreText;
    public Text highScoreLiteral;

    private int currentHighScore;

    public void TriggerGameStart()
    {
        tapTextAnimator.SetBool("Appear", false);
        panelAnimator.SetBool("Appear", false);
    }

    public void TriggerGameOver()
    {
        mainCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
    }

    public void UpdateScore(int score, int highScore)
    {
//        scoreText.text = "Score: " + score;
        scoreLiteral.text = score.ToString();

        UpdateHighScore(highScore);
    }

    public void UpdateHighScore(int highScore)
    {
        if (highScore == currentHighScore)
        {
            return;
        }
        highScoreText.text = "High Score: " + highScore;
        highScoreLiteral.text = highScore.ToString();
        currentHighScore = highScore;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        mainCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}