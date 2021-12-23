using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GamePause
{
    public static void PauseGame(bool isPaused)
    {
        switch (isPaused)
        {
            case true:
            Time.timeScale = 0;
            break;
            case false:
            Time.timeScale = 1;
            break;
            default:
        }
    }
}
