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

    private void Walk()
    {
        m_Animator.SetBool("IsWalking", true);
    }

    private void StopWalk()
    {
        m_Animator.SetBool("IsWalking", false);
    }

}
