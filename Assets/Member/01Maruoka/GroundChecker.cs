using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _checkDistance = 0.5f;

    private bool _isGrounded;

    public bool IsGrounded => _isGrounded;

    private void Update()
    {
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, _checkDistance, _groundLayer);
    }
}