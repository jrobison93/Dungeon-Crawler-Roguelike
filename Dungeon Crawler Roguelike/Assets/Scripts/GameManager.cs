using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public LevelGenerator[] levelGenerators;
    public GameObject[] playerClasses;
    public bool levelUp;
    public static GameManager instance = null;

    private int level = 1;
    private GameObject player;
    private List<Enemy> enemies;
    private LevelGenerator currentLevel;
    private GameObject startScreen;
    private Text gameOverText;


	void Awake()
    {
        if (instance == null)
        {
            instance = this;
            player = Instantiate(playerClasses[Random.Range(0, playerClasses.Length)]) as GameObject;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        enemies = new List<Enemy>();

        InitGame();

    }

    private void OnLevelWasLoaded(int index)
    {
        level++;

        InitGame();
        player.GetComponent<Player>().LevelUp();
    }

    private void InitGame()
    {
        startScreen = GameObject.Find("StartScreen");
        GameObject.Find("Title").GetComponent<Text>().enabled = false;
        GameObject.Find("Instructions").GetComponent<Text>().enabled = false;
        gameOverText = GameObject.Find("GameOverText").GetComponent<Text>();
        startScreen.SetActive(false);
        levelUp = false;
        enemies.Clear();
        currentLevel = levelGenerators[Random.Range(0, levelGenerators.Length)];
        currentLevel.CreateLevel(50, 50, 60, player);
    }

    public void GameOver()
    {
        startScreen.SetActive(true);
        gameOverText.text = "You died on floor " + level;
        gameOverText.enabled = true;
        enabled = false;
        Destroy(player);
        currentLevel.GameOver();
    }
}
