using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;

    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;

    public delegate void StartTapScreen(Vector2 pos);
    public event StartTapScreen OnStartTap;

    public delegate void EndTapScreen();
    public event EndTapScreen OnEndTap;
    #endregion

    private PlayerInputs playerControls;
    public Camera mainCam;

    private void Awake()
    {
        playerControls = new PlayerInputs();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        playerControls.Player.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        playerControls.Player.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);

        playerControls.Player.Tap.started += ctx => StartTap(ctx);
        playerControls.Player.Tap.canceled += ctx => EndTap(ctx);
    }

    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnStartTouch != null)
        {
            OnStartTouch(Utils.ScreenToWorld(mainCam, playerControls.Player.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
        }
    }

    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null)
        {
            OnEndTouch(Utils.ScreenToWorld(mainCam, playerControls.Player.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
        }
    }

    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(mainCam, playerControls.Player.PrimaryPosition.ReadValue<Vector2>());
    }

    public void StartTap(InputAction.CallbackContext context)
    {
        OnStartTap?.Invoke(playerControls.Player.PrimaryPosition.ReadValue<Vector2>());
    }

    public void EndTap(InputAction.CallbackContext context)
    {
        OnEndTap?.Invoke();
    }
}
