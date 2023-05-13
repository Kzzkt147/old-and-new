using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}

    private PlayerInputActions _playerInputActions;
    
    public void ResumeGame() => GameEvents.ResumeGame();
    public void PauseGame() => GameEvents.PauseGame();
    public void StartCutscene() => GameEvents.StartCutscene();

    public void ChangeToFuture() => TimeSwitchController.ChangeTimePeriodToFuture();
    public void ChangeToPast() => TimeSwitchController.ChangeTimePeriodToPast();

    private void Quit(InputAction.CallbackContext ctx)
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    
    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        
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

    private void OnEnable()
    {
        _playerInputActions.Utility.Enable();

        _playerInputActions.Utility.Quit.performed += Quit;
    }

    private void OnDisable()
    {
        _playerInputActions.Utility.Disable();

        _playerInputActions.Utility.Quit.performed -= Quit;
    }
}
