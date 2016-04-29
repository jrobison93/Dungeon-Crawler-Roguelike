using UnityEngine;
using System.Collections;

public interface GameState
{

    void StartGame();
    void PauseButtonPressed();
    void SetUpLevel();
    void PlayerKilled();
}
