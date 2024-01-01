using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 12.0f;

    private Vector2 _moveDirection;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private Camera _mainCamera;
    private PlayerGun _playerGun;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _mainCamera = Camera.main;
        _playerGun = GetComponentInChildren<PlayerGun>();
        if (_mainCamera == null)
        {
            Debug.LogError("Can't find main camera, please check it!!!");
        }
    }

    private void Update()
    {
        //移动
        _moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _moveDirection *= moveSpeed;

        _animator.SetFloat("Move Speed", Mathf.Abs(_moveDirection.x) + Mathf.Abs(_moveDirection.y));

        //转向 瞄准
        Vector3 direction = Input.mousePosition - _mainCamera.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);

        //输入
        if (Input.GetMouseButton(0))
        {
            //扣动扳机
            _playerGun.OnTriggerHold(_mainCamera.ScreenToWorldPoint(Input.mousePosition));
        }

        if (Input.GetMouseButtonUp(0))
        {
            //松开扳机
            _playerGun.OnTriggerRelease();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //重新装弹
            _playerGun.Reload();
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.AddForce(_moveDirection, ForceMode2D.Impulse);
    }

    public void SetShootingState(bool isShooting)
    {
        _animator.SetBool("IsShooting", isShooting);
    }
}
