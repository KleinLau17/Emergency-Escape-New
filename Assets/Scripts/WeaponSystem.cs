using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform shootTrail;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool("IsShooting", false);

        if (Input.GetButton("Fire1") || Input.GetMouseButtonDown(0))
        {
            _animator.SetBool("IsShooting", true);
            Debug.Log("Shoot");

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
            Vector2 shootDirection;
            if (Vector2.Distance(mousePosition, firePointPosition) > 0.5f)
            {
                shootDirection = mousePosition - firePointPosition;
            }
            else
            {
                shootDirection = transform.up;
            }

            float trailAngle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
            Quaternion trailRotation = Quaternion.AngleAxis(trailAngle, Vector3.forward);
            Transform trail = Instantiate(shootTrail, firePointPosition, trailRotation);
            Destroy(trail.gameObject, 0.05f);

            // Debug.DrawLine(firePointPosition, shootDirection * 100, Color.red);
        }
    }
}
