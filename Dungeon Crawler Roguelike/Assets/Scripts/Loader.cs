using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

    public GameObject gameManager;

    // Use this for initialization
    void Awake()
    {
        if (GameManager.instance == null)
        {

            Invoke("StartGame", 2);
        }

    }

    private void StartGame()
    {
        Instantiate(gameManager);
    }

}
