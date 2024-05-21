using System;
using System.Threading;
using UnityEditorInternal;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    [Header("基本設定")]
    [SerializeField] private float _walkSpeed = 4f;
    [SerializeField] private GroundChecker _groundChecker;

    [Header("ジャンプ設定")]
    [SerializeField] private Vector2 _jumpPowerAddSpeed = new Vector2(20f, 20f);
    [SerializeField] private float _minJumpPowerY = 1f;
    [SerializeField] private float _maxJumpPowerY = 24f;

    [SerializeField] private float _maxJumpPowerX = 40f;

    [Header("ジャンプ力確認用")]
    [SerializeField] private Vector2 _jumpPower;
    private Rigidbody2D _rb;

    [Header("Physics Material 設定")]
    [SerializeField] private PhysicsMaterial2D _groundedPhysicsMaterial2D;
    [SerializeField] private PhysicsMaterial2D _jumpingPhysicsMaterial2D;

    [SerializeField] private Animator _animator;

    private bool IsGrounded => _groundChecker.IsGrounded;

    public Vector2 JumpPower => _jumpPower;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _groundChecker.OnGrounded += () => _animator.Play("Idle");
        _groundChecker.OnJumped += () => _animator.Play("Jump");
    }

    private void Update()
    {
        // ジャンプ処理
        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            _jumpPower.y = _minJumpPowerY;
            _jumpPower.x = 0f;
        }

        if (Input.GetButton("Jump") && IsGrounded)
        {
            _jumpPower.y += _jumpPowerAddSpeed.y * Time.deltaTime;
            if (_jumpPower.y > _maxJumpPowerY) _jumpPower.y = _maxJumpPowerY;

            if (Input.GetAxisRaw("Horizontal") > 0.1f)
                _jumpPower.x += _jumpPowerAddSpeed.x * Time.deltaTime;
            if (Input.GetAxisRaw("Horizontal") < -0.1f)
                _jumpPower.x -= _jumpPowerAddSpeed.x * Time.deltaTime;

            _jumpPower.x = Mathf.Clamp(_jumpPower.x, -_maxJumpPowerX, _maxJumpPowerX);
        }

        if (Input.GetButtonUp("Jump") && IsGrounded)
        {
            _rb.velocity = _jumpPower;

            _jumpPower.y = _minJumpPowerY;
            _jumpPower.x = 0f;
        }

        float xVelocity = 0f;

        // 移動処理
        // ジャンプキーが押されてなければ水平方向移動可能。
        if (!Input.GetButton("Jump"))
        {
            xVelocity = Input.GetAxisRaw("Horizontal") * _walkSpeed;
        }
        // 空中にいるときは水平方向の制御が不可能。
        if (IsGrounded && _rb.velocity.y < 0)
        {
            _rb.velocity = new Vector2(xVelocity, _rb.velocity.y);
        }

        if (IsGrounded)
        {
            _rb.sharedMaterial = _groundedPhysicsMaterial2D;
        }
        else
        {
            _rb.sharedMaterial = _jumpingPhysicsMaterial2D;
        }
    }
}
