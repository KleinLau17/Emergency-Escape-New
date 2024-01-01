using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayController : MonoBehaviour
{
    public float moveSpeed = 12.0f;

    private Vector2 _moveDirection;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _moveDirection *= moveSpeed;

        _animator.SetFloat("Move Speed", Mathf.Abs(_moveDirection.x) + Mathf.Abs(_moveDirection.y));

    }

    private void FixedUpdate()
    {
        _rigidbody2D.AddForce(_moveDirection, ForceMode2D.Impulse);
    }

}
