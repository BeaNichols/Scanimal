using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SwpieDetection : Singleton<SwpieDetection>
{
    #region Events
    public delegate void Swipe();
    public static event Swipe OnSwipe;

    public delegate void Tap();
    public static event Tap OnTap;
    #endregion
    [SerializeField]
    private float minDistance = 0.2f;
    [SerializeField]
    private float maxTime = 1.0f;
    [SerializeField, Range(-1,1f)]
    private float directionThresshold = 0.9f;

    private InputManager inputManager;

    private Vector2 startPosition;
    private float startTime;

    private Vector2 endPosition;
    private float endTime;

    public enum swipeDirection
    { 
        up,
        down, 
        left,
        right,
        non
    }
    public swipeDirection currentDirection;


    private void Awake()
    {
        inputManager = InputManager.Instance;
        currentDirection = swipeDirection.non;
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;

        inputManager.OnStartTap += StartTap;
        inputManager.OnEndTap += EndTap;
    }

    private void OnDisable()
    {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;

        inputManager.OnStartTap -= StartTap;
        inputManager.OnEndTap -= EndTap;
    }

    private void SwipeStart(Vector2 position, float time)
    { 
        startPosition = position;
        startTime = time;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector3.Distance(startPosition, endPosition) >= minDistance && (endTime - startTime) <= maxTime)
        {
            Debug.DrawLine(startPosition, endPosition, Color.red, 500f);
            Vector3 direction = endPosition - startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2D);
        }
    }

    private void SwipeDirection(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.up, direction) > directionThresshold)
        {
            currentDirection = swipeDirection.up;
            OnSwipe?.Invoke();
        }
        else if (Vector2.Dot(Vector2.down, direction) > directionThresshold)
        {
            currentDirection = swipeDirection.down;
            OnSwipe?.Invoke();
        }
        else if (Vector2.Dot(Vector2.left, direction) > directionThresshold)
        {
            currentDirection = swipeDirection.left;
            OnSwipe?.Invoke();
        }
        else if (Vector2.Dot(Vector2.right, direction) > directionThresshold)
        {
            currentDirection = swipeDirection.right;
            OnSwipe?.Invoke();
        }
    }

    private void StartTap(Vector2 pos)
    {
        if (pos.y <  Screen.height / 1.2)
        {
            OnTap?.Invoke();
        }
    }

    private void EndTap()
    {

    }
}
