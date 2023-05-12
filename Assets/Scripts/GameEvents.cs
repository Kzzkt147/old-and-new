using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action OnGamePlay;
    public static event Action OnGamePause;
    public static event Action OnGameCutscene;

    public static void ResumeGame()
    {
        OnGamePlay?.Invoke();
    }

    public static void PauseGame()
    {
        OnGamePause?.Invoke();
    }

    public static void StartCutscene()
    {
        OnGameCutscene?.Invoke();
    }
}
