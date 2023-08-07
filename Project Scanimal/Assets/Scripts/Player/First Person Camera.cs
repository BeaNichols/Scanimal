using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    [SerializeField]
    private float mouseSensitivity;
    [SerializeField]
    private float smoothRotation;

    private PlayerInputs m_PlayerInputs;
    private CharacterController m_Controller;
    private Vector2 m_CurrentDirection;
    private Vector2 m_CurrentVelocity;

    void Awake()
    {
        m_Controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        m_PlayerInputs = new PlayerInputs();
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
        float velocityY =0;
        if (IsGrounded())
        {
            velocityY += 30f * 2f * Time.deltaTime;
        }
        else
        {
            velocityY = -8f;
        }
        Vector2 targetDirection = m_PlayerInputs.Player.Move.ReadValue<Vector2>();
        targetDirection.Normalize();

        m_CurrentDirection = Vector2.SmoothDamp(m_CurrentDirection, targetDirection, ref m_CurrentVelocity, 0.1f);

        Vector3 velocity = (transform.forward * m_CurrentDirection.y + transform.right * m_CurrentDirection.x) * Speed + Vector3.up * velocityY;

        m_Controller.Move(velocity * Time.deltaTime);
    }
}