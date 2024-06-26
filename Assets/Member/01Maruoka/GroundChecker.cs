using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundCheckRadius = 0.1f;
    [SerializeField] private Vector2 _groundCheckOffset = new Vector2(0f, -0.5f);

    public bool IsGrounded => Physics2D.OverlapCircle((Vector2)transform.position + _groundCheckOffset, _groundCheckRadius, _groundLayer);

    private bool _wasGrounded;

    public event Action OnLandingSE;
    public event Action OnJumped;

    private void Update()
    {
        if (_wasGrounded != IsGrounded)
        {
            if (IsGrounded) OnLandingSE?.Invoke();
            else OnJumped?.Invoke();
        }

        _wasGrounded = IsGrounded;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = IsGrounded ? Color.red : Color.green;
        Gizmos.DrawWireSphere((Vector2)transform.position + _groundCheckOffset, _groundCheckRadius);
    }
}