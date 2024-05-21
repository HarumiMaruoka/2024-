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
    [SerializeField] private float _groundCollisionFriction = 0f;
    [SerializeField] private float _groundCollisionBounciness = 0f;
    [SerializeField] private float _wallCollisionFriction = 0.2f;
    [SerializeField] private float _wallCollisionBounciness = 0.2f;

    private PhysicsMaterial2D _physicsMaterial2D;

    private bool IsGrounded => _groundChecker.GameObjectsCount > 0;

    public Vector2 JumpPower => _jumpPower;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _physicsMaterial2D = _rb.sharedMaterial;
    }

    private void Update()
    {
        // ジャンプ処理
        if (Input.GetButtonDown("Jump"))
        {
            _jumpPower.y = _minJumpPowerY;
            _jumpPower.x = 0f;
        }

        if (Input.GetButton("Jump"))
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
            _physicsMaterial2D.friction = _groundCollisionFriction;
            _physicsMaterial2D.bounciness = _groundCollisionBounciness;
        }
        else
        {
            _physicsMaterial2D.friction = _wallCollisionFriction;
            _physicsMaterial2D.bounciness = _wallCollisionBounciness;
        }
    }
}
