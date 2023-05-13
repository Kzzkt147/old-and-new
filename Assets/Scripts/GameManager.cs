using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    
    
    public void ResumeGame() => GameEvents.ResumeGame();
    public void PauseGame() => GameEvents.PauseGame();
    public void StartCutscene() => GameEvents.StartCutscene();

    public void ChangeToFuture() => TimeSwitchController.ChangeTimePeriodToFuture();
    public void ChangeToPast() => TimeSwitchController.ChangeTimePeriodToPast();


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Found a second instance of this singleton, destroying copycat...", gameObject);
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
