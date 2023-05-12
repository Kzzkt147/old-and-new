using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [Header("Connected Components")]
    [SerializeField] private PlayerMove playerMove;

    [SerializeField] private bool canSwitchTime = false;

    //private variables
    private PlayerInputActions _playerInputActions;
    private bool _canTakeInput = true;

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (!_canTakeInput) return;
        playerMove.Jump();
    }

    private void ToggleTimeSwitch(InputAction.CallbackContext ctx)
    {
        if (!_canTakeInput) return;
        if (!canSwitchTime) return;
        TimeSwitchController.ToggleTimePeriod();
    }

    private void DisablePlayer()
    {
        _canTakeInput = false;
        playerMove.MoveInput = 0;
    }

    private void EnablePlayer()
    {
        _canTakeInput = true;
    }

    private void Update()
    {
        if (!playerMove) return;
        if (!_canTakeInput) return;
        playerMove.MoveInput = _playerInputActions.Player.Move.ReadValue<float>();
    }

    private void OnEnable()
    {
        _playerInputActions.Player.Enable();

        _playerInputActions.Player.Jump.performed += Jump;
        _playerInputActions.Player.Jump.started += (ctx) => playerMove.IsJumpPressed = true;
        _playerInputActions.Player.Jump.canceled += (ctx) => playerMove.IsJumpPressed = false;

        _playerInputActions.Player.TimeSwitch.performed += ToggleTimeSwitch;
        

        GameEvents.OnGamePause += DisablePlayer;
        GameEvents.OnGameCutscene += DisablePlayer;
        GameEvents.OnGamePlay += EnablePlayer;
    }

    private void OnDisable()
    {
        _playerInputActions.Player.Disable();
        
        _playerInputActions.Player.Jump.performed -= Jump;
        
        GameEvents.OnGamePause -= DisablePlayer;
        GameEvents.OnGameCutscene -= DisablePlayer;
        GameEvents.OnGamePlay -= EnablePlayer;
    }

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        
    }
}
