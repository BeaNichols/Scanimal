using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private float smoothTime = 0.05f;
    [SerializeField] private float speed;

    private CharacterController _characterController;
    private PlayerInputs m_PlayerInputs;
    private float _currentVelocity;
    private Vector2 _input;
    private Vector3 _direction;

    void Awake()
    {
        Application.targetFrameRate = 140;
        _characterController = GetComponent<CharacterController>();
        m_PlayerInputs = new PlayerInputs();
        IsGrounded();
    }

    private void OnEnable()
    {
        m_PlayerInputs.Enable();
    }

    private void OnDisable()
    {
        m_PlayerInputs.Disable();
    }

    void Update()
    {
        Movement();
        IsGrounded();
    }

    private bool IsGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 0.1f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 0.1f, Color.white);
            return false;
        }
    }

    void Movement()
    {
        float velocityY = 0;
        if (IsGrounded())
        {
            velocityY += 30f * 2f * Time.deltaTime;
        }
        else
        {
            velocityY = -8f;
        }
       
        _input = m_PlayerInputs.Player.Move.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, velocityY, _input.y);
        if (_input.sqrMagnitude == 0) return;

        var targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

        _characterController.Move(_direction * speed * Time.deltaTime);
    }
}