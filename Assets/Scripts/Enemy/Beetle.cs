using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Beetle : LivingEntity
{
    [Header("AI")]
    public Transform target;
    public AIPath aiPath;

    [Header("Melee")]
    public LayerMask whatToHit;
    public float damage = 10;
    public float hitRate = 1.0f;
    private float _lastHit;

    private Animator _animator;

    protected override void Start()
    {
        base.Start();
        _animator = GetComponent<Animator>();
        aiPath = GetComponent<AIPath>();
    }

    private void Update()
    {
        if (target == null)
        {
            aiPath.destination = transform.position;
            _animator.SetBool("IsMoving", false);
            return;
        }

        aiPath.destination = target.position;
        if (aiPath.reachedDestination)
        {
            //HIt player
            _animator.SetBool("IsMoving", false);
            if (Time.time > _lastHit + 1 / hitRate)
            {
                Hit();
                _lastHit = Time.time;
            }
        }
        else
        {
            _animator.SetBool("IsMoving", true);
        }
    }

    private void Hit()
    {
        Vector3 selfPosition = transform.position;
        //Direction to target
        Vector3 targetDirection = (target.position - selfPosition).normalized;
        RaycastHit2D hit2D = Physics2D.Raycast(selfPosition, targetDirection, aiPath.endReachedDistance + 2.0f, whatToHit);
        if (hit2D.collider != null)
        {
            IDamageable damageable = hit2D.transform.GetComponent<IDamageable>();
            damageable?.TakeDamage(damage);
        }
    }
}
