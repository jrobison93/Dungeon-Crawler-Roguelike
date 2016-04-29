using UnityEngine;
using System.Collections;
using System;

public class PausedState : GameState
{
    protected GameManager gameManager;

    public PausedState(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void PauseButtonPressed()
    {
        gameManager.isPaused = false;
        gameManager.SetState(gameManager.GetPlayingState());
        gameManager.UnPauseGame();
    }

    public void PlayerKilled()
    {
        return;
    }

    public void SetUpLevel()
    {
        return;
    }

    public void StartGame()
    {
        return;
    }
}
