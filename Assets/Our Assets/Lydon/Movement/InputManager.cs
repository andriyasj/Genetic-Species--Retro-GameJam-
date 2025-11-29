using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;

public class InputManager : MonoBehaviour
{
    private PlayerControls _playerControls; // This is the actual input mappings
    private PlayerControls.OnFootActions _onFootActions; // Collection of onFoot inputs
    private PlayerInteract _playerInteract; // Script that attempts to takeover another enemy
    private PlayerMotors _motor; // Movement script
    private PlayerLook _playerLook; // Looking script
    private PlayerAttack _playerAttack; // Attack script
    private PlayerActions _playerActions; // General Player Script
    void Awake()
    {
        _playerControls = new PlayerControls();
        _onFootActions = _playerControls.OnFoot;
        _motor = GetComponent<PlayerMotors>();
        _playerInteract = GetComponent<PlayerInteract>();
        _playerLook = GetComponent<PlayerLook>();
        _playerAttack = GetComponent<PlayerAttack>();
        _playerActions = GetComponent<PlayerActions>();
        
        AssignInputs();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void AssignInputs()
    {
        _onFootActions.Look.performed += ctx => _playerLook.ProcessLook(ctx.ReadValue<Vector2>());
        _onFootActions.Interact.performed += ctx => _playerInteract.AttemptTakeover();
        _onFootActions.Attack.performed += ctx => _playerAttack.Attack();
        _onFootActions.InfiniteAmmo.performed += cts => _playerActions.GetAmmo(int.MaxValue);
    } 
    void FixedUpdate()
    {
        _motor.ProcessMove(_onFootActions.Movement.ReadValue<Vector2>()); 
    }

//    private void LateUpdate() 
//    {
//        _playerLook.ProcessLook(_onFootActions.Look.ReadValue<Vector2>());
//    }

    private void Update()
    {
        
    }

    private void OnEnable()
    {
        _onFootActions.Enable();
    }

    private void OnDisable()
    {
        _onFootActions.Disable();
    }
}
