using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
    public static WorldManager instance;
    public BallController player;
    public PlatformSpawnner platformSpawnner;

    private ScoreManager scoreManager;

    [HideInInspector]
    public bool GameOver { get; private set; }

    public void StartGame()
    {
        UIManager.instance.TriggerGameStart();
        scoreManager.StartIncrement();
        platformSpawnner.InitSpawn();
    }

    public void CheckGameOver()
    {
        if (!player.OnGround())
        {
            GameOver = true;
            UIManager.instance.TriggerGameOver();
            scoreManager.StopIncrement();
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            scoreManager = player.GetComponent<ScoreManager>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!GameOver)
        {
            CheckGameOver();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}