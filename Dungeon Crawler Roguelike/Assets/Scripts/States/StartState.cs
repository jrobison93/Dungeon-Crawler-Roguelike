using UnityEngine;
using System.Collections;
using System;

public class StartState : GameState
{
    protected GameManager gameManager;

    public StartState(GameManager gameManager)
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
        return;
    }

    public void StartGame()
    {
        gameManager.SetState(gameManager.GetPlayingState());
    }
}
