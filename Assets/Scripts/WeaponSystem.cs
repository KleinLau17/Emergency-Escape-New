using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform shootTrail;
    [SerializeField] private Transform muzzleFlash;

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

            MuzzleFlash();
        }
    }

    //Ç¹¿Ú»ð¹â
    private void MuzzleFlash()
    {
        Transform flash = Instantiate(muzzleFlash, firePoint.position, firePoint.rotation);
        flash.SetParent(firePoint);
        float randomSize = Random.Range(0.6f, 0.9f);
        flash.localScale = new Vector3(randomSize, randomSize, randomSize);
        Destroy(flash.gameObject, 0.05f);

    }
}
