using UnityEngine;
using System.Collections;
using System;

public class GameOverState : GameState
{
    protected GameManager gameManager;

    public GameOverState(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void PauseButtonPressed()
    {
        return;
    }

    public void PlayerKilled()
    {
        return;
    }

    public void SetUpLevel()
    {
        gameManager.SetState(gameManager.GetStartState());
    }

    public void StartGame()
    {
        return;
    }
}
