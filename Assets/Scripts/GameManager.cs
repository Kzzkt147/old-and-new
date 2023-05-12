using UnityEngine;

public class GameManager : Singleton
{
    public void ResumeGame() => GameEvents.ResumeGame();
    public void PauseGame() => GameEvents.PauseGame();
    public void StartCutscene() => GameEvents.StartCutscene();
}
