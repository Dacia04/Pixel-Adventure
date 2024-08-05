
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{


    public Vector2 RawMovementInput {  get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }

    public bool JumpInput { get; private set; }

    public bool WallClimb { get; private set; }

    [SerializeField] private float _inputHoldTime = 0.2f;
    private float _jumpInputStartTime;
    public float WallClimbStartTime;

    private PlayerInput _playerInput;

    private void Awake() {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {

        CheckJumpInputHoldTime();


    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();
        NormInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormInputY = Mathf.RoundToInt(RawMovementInput.y);
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            JumpInput = true;
            _jumpInputStartTime = Time.time;
        }
        
        else if(context.canceled)
        {
            JumpInput = false;
        }
    }

    public void OnWallClimbInput(InputAction.CallbackContext context)
    {
        if(context.started) 
        {
            WallClimb = true;

        }
        else if(context.canceled)
        {
            WallClimb = false;
        }
    }

    public void UseJumpInput() => JumpInput = false;

    private void CheckJumpInputHoldTime()
    {
        if(Time.time > _jumpInputStartTime + _inputHoldTime)
        {
            JumpInput = false;
        }
    }

    public void EnableControl()
    {
        _playerInput.currentActionMap.Enable();
    }

    public void DisableControl()
    {
        _playerInput.currentActionMap.Disable();
    }



}