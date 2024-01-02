using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 12.0f;

    private Vector2 _moveDirection;
    private Rigidbody2D _rigidbody2D;
    private Camera _mainCamera;
    private PlayerGun _playerGun;

    [SerializeField] private Transform playerBody;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;
        _playerGun = GetComponentInChildren<PlayerGun>();
        if (_mainCamera == null)
        {
            Debug.LogError("Can't find main camera, please check it!!!");
        }
    }

    private void Update()
    {
        //�ƶ�
        _moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _moveDirection *= moveSpeed;

        animator.SetFloat("Move Speed", Mathf.Abs(_moveDirection.x) + Mathf.Abs(_moveDirection.y));

        //ת�� ��׼
        Vector3 direction = Input.mousePosition - _mainCamera.WorldToScreenPoint(playerBody.position);
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        playerBody.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);

        //����
        if (Input.GetMouseButton(0))
        {
            //�۶����
            _playerGun.OnTriggerHold();
        }

        if (Input.GetMouseButtonUp(0))
        {
            //�ɿ����
            _playerGun.OnTriggerRelease();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //����װ��
            _playerGun.Reload();
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.AddForce(_moveDirection, ForceMode2D.Impulse);
    }

    public void KnockBack()
    {
        _rigidbody2D.AddRelativeForce(-playerBody.up * 180, ForceMode2D.Force);
    }

    public void SetShootingState(bool isShooting)
    {
        animator.SetBool("IsShooting", isShooting);
    }
}
