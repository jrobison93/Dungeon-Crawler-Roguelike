using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public LevelGenerator[] levelGenerators;
    public GameObject[] playerClasses;
    public static GameManager instance = null;

    private int level = 1;
    private GameObject player;
    private List<Enemy> enemies;


	void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        player = playerClasses[Random.Range(0, playerClasses.Length)];
        enemies = new List<Enemy>();

        InitGame();
    }

    private void InitGame()
    {
        enemies.Clear();
        levelGenerators[Random.Range(0, levelGenerators.Length)].CreateLevel(50, 50, 60, player);
    }
}
