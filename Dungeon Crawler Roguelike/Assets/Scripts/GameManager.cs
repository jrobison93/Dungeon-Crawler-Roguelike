using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public LevelGenerator[] levelGenerators;
    public static GameManager instance = null;

    private int level = 1;


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

        InitGame();
    }

    private void InitGame()
    {
        levelGenerators[Random.Range(0, levelGenerators.Length)].CreateLevel(50, 50, 60);
    }
}
