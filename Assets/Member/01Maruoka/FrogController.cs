using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;
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

    [Header("効果音")]
    [SerializeField] private AudioClip _jumpSE;
    [SerializeField] private AudioClip _landingSE;
    [SerializeField] private AudioClip _collisionSE;
    [SerializeField] private AudioClip _goalSE;

    [Header("アニメーション")]
    [SerializeField] private Animator _animator;

    private float _initialScaleZ;
    private bool IsGrounded => _groundChecker.IsGrounded;

    public Vector2 JumpPower => _jumpPower;

    private void Start()
    {
        _initialScaleZ = transform.localScale.z;
        _rb = GetComponent<Rigidbody2D>();
        if (_animator)
        {
            _groundChecker.OnLandingSE += () => { _animator.Play("Idle"); if (AudioManager.Instance) AudioManager.Instance.PlaySE(_landingSE); };
            _groundChecker.OnJumped += () => { _animator.Play("Jump"); if (AudioManager.Instance) AudioManager.Instance.PlaySE(_jumpSE); };
        }
    }

    private void Update()
    {
        // 移動処理
        float xVelocity = 0f;
        // 移動処理
        // ジャンプキーが押されてなければ水平方向移動可能。
        if (!Input.GetButton("Jump"))
        {
            xVelocity = Input.GetAxisRaw("Horizontal") * _walkSpeed;
        }
        // 空中にいるときは水平方向の制御が不可能。
        if (IsGrounded && _rb.velocity.y < 0.01f)
        {
            _rb.velocity = new Vector2(xVelocity, _rb.velocity.y);
        }

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


        if (IsGrounded)
        {
            _rb.sharedMaterial = _groundedPhysicsMaterial2D;
        }
        else
        {
            _rb.sharedMaterial = _jumpingPhysicsMaterial2D;
        }

        if (_rb.velocity.x > 0.1f)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, _initialScaleZ);
        }
        else if (_rb.velocity.x < -0.1f)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -_initialScaleZ);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            if (AudioManager.Instance) AudioManager.Instance.PlaySE(_collisionSE);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Goal")
        {
            if (AudioManager.Instance) AudioManager.Instance.PlaySE(_goalSE);
        }
    }
}
