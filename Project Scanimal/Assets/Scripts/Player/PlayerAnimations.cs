using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public Animator m_Animator;

    private void OnEnable()
    {
        PlayerController.OnMove += Walk;
        PlayerController.OnStopMove += StopWalk;
    }

    private void OnDisable()
    {
        PlayerController.OnMove -= Walk;
        PlayerController.OnStopMove -= StopWalk;
    }

    private void Start()
    {
        m_Animator = GetComponentInChildren<Animator>();
    }

    private void Walk(float vel)
    {
        //m_Animator.SetBool("IsWalking", true);
        m_Animator.SetFloat("Velocity", vel);
    }

    private void StopWalk(float vel)
    {
        m_Animator.SetFloat("Velocity", vel);
    }

}
