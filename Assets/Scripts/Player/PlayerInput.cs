using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [Header("Connected Components")]
    [SerializeField] private PlayerMove playerMove;

    //private variables
    private PlayerInputActions _playerInputActions;

    private void Jump(InputAction.CallbackContext ctx) => playerMove.Jump();

    private void Update()
    {
        if (!playerMove) return;
        playerMove.MoveInput = _playerInputActions.Player.Move.ReadValue<float>();
    }

    private void OnEnable()
    {
        _playerInputActions.Player.Enable();

        _playerInputActions.Player.Jump.performed += Jump;
        _playerInputActions.Player.Jump.started += (ctx) => playerMove.IsJumpPressed = true;
        _playerInputActions.Player.Jump.canceled += (ctx) => playerMove.IsJumpPressed = false;

        _playerInputActions.Player.TimeSwitch.performed += (ctx) => TimeSwitchController.ToggleTimePeriod();
    }

    private void OnDisable()
    {
        _playerInputActions.Player.Disable();
        
        _playerInputActions.Player.Jump.performed -= Jump;
    }

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        
    }
}
