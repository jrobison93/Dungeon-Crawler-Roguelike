using UnityEngine;
using System.Collections;
using System;

public class PlayingState : GameState
{
    protected GameManager gameManager;

    public PlayingState(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void PauseButtonPressed()
    {
        gameManager.isPaused = true;
        gameManager.SetState(gameManager.GetPausedState());
        gameManager.PauseGame();
    }

    public void PlayerKilled()
    {
        gameManager.SetState(gameManager.GetGameOverState());
    }

    public void SetUpLevel()
    {
        gameManager.isPaused = true;
        gameManager.SetState(gameManager.GetStartState());
    }

    public void StartGame()
    {
        return;
    }
}
