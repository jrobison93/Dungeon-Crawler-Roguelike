using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public LevelGenerator[] levelGenerators;
    public bool levelUp;
    public static GameManager instance = null;
    public bool[,] movingObjects;

    [HideInInspector]
    public bool isPaused;

    private int level = 1;
    private GameObject player;
    private List<Enemy> enemies;
    private LevelGenerator currentLevel;
    private GameObject startScreen;
    private GameObject pausedScreen;
    private Text gameOverText;
    private Text pausedText;
    private MovingObjectFactory playerFactory = new PlayerFactory();

    private GameState startState;
    private GameState pausedState;
    private GameState gameOverState;
    private GameState playingState;

    private GameState state;

    GameManager()
    {
        startState = new StartState(this);
        pausedState = new PausedState(this);
        gameOverState = new GameOverState(this);
        playingState = new PlayingState(this);
        state = startState;
    }

    public void SetState(GameState newState)
    {
        state = newState;
    }

    public GameState GetStartState()
    {
        return startState;
    }

    public GameState GetPausedState()
    {
        return pausedState;
    }

    public GameState GetGameOverState()
    {
        return gameOverState;
    }

    public GameState GetPlayingState()
    {
        return playingState;
    }


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            player = playerFactory.GetRandomObject();
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        enemies = new List<Enemy>();
        movingObjects = new bool[0, 0];

        InitGame();

    }

    void Update()
    {
        bool pausedPressed = Input.GetButtonDown("Pause");

        if(pausedPressed )
        {
            Debug.Log("Pressed");
            state.PauseButtonPressed();
        }
    }


    private void OnLevelWasLoaded(int index)
    {
        state.SetUpLevel();
        level++;

        InitGame();
        player.GetComponent<Player>().LevelUp();
    }

    private void InitGame()
    {
        startScreen = GameObject.Find("StartScreen");
        pausedScreen = GameObject.Find("PauseScreen");
        GameObject.Find("Title").GetComponent<Text>().enabled = false;
        GameObject.Find("Instructions").GetComponent<Text>().enabled = false;
        gameOverText = GameObject.Find("GameOverText").GetComponent<Text>();
        pausedText = GameObject.Find("PausedText").GetComponent<Text>();
        startScreen.SetActive(false);
        pausedScreen.SetActive(false);
        levelUp = false;
        enemies.Clear();
        currentLevel = levelGenerators[Random.Range(0, levelGenerators.Length)];
        currentLevel.CreateLevel(50, 50, 60, player);
        state.StartGame();
    }

    public void PauseGame()
    {
        pausedScreen.SetActive(true);
        pausedText.enabled = true;
    }

    public void UnPauseGame()
    {
        pausedScreen.SetActive(false);
    }


    public void GameOver()
    {
        state.PlayerKilled();
        startScreen.SetActive(true);
        gameOverText.text = "You died on floor " + level;
        gameOverText.enabled = true;
        enabled = false;
        Destroy(player);
        currentLevel.GameOver();
    }
}
