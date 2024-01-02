using System.Collections;
using System.Collections.Generic;
using TinyCore;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform shootTrail;
    [SerializeField] private Transform muzzleFlash;
    [SerializeField] private Transform hitParticle;
    [SerializeField] private TMP_Text remainingBulletText;

    private string remainingBulletCount => (clipSize - shotsNumber).ToString();
    private PlayerData playerData;

    //武器系统
    [Header("武器属性")]
    public LayerMask whatToHit;
    public FireMode FireMode = FireMode.Burst;
    public bool isReloading;
    public float nextShotTime;
    public int shotsNumber;
    public int clipSize;
    public int fireRate;
    public float reloadTime;
    public bool isTriggerReleased;

    public void Init(PlayerData pd)
    {
        playerData = pd;
        remainingBulletText.SetText(remainingBulletCount);
    }

    public void OnTriggerHold(){
        if (isReloading) return;

        if (shotsNumber >= clipSize)
        {
            StartCoroutine(ReloadCoroutine());
            return;
        }

        switch (FireMode)
        {
            case FireMode.Single:
                {
                    playerController.SetShootingState(false);
                    if (!isTriggerReleased) return;
                    Shoot();
                    isTriggerReleased = false;
                    break;
                }
            case FireMode.Burst:
                {
                    if (Time.time < nextShotTime)
                    {
                        playerController.SetShootingState(false);
                        return;
                    }
                    nextShotTime = Time.time + 1 / (float)fireRate;
                    Shoot();
                    break;
                }
        }
    }

    public void OnTriggerRelease(){
        Debug.Log("松开扳机");
        isTriggerReleased = true;
        playerController.SetShootingState(false);
    }

    #region 装弹
    public void Reload(){
        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        // TODO 装弹动画
        Debug.Log("重新装弹");
        isReloading = true;
        playerController.SetShootingState(false);
        SoundManager.Instance.PlaySound("Assault Reload");
        yield return new WaitForSeconds(reloadTime);
        //after reloading
        shotsNumber = 0;
        isReloading = false;
        remainingBulletText.SetText(remainingBulletCount);
    }

    #endregion

    #region 射击与射击效果
    private void Shoot()
    {
        shotsNumber++;
        playerController.SetShootingState(true);
        SoundManager.Instance.PlaySound("Assault Shoot");
        remainingBulletText.SetText(remainingBulletCount);


        Vector2 shootDirection = GetShootDirection();
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, shootDirection, 100, whatToHit);
        if (hit.collider != null)
        {
            ShootEffect(hit);
        }
        else
        {
            ShootEffect(shootDirection);
        }

        playerController.KnockBack();
    }

    private void ShootEffect(Vector2 shootDirection)
    {
        float trailAngle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        Quaternion trailRotation = Quaternion.AngleAxis(trailAngle, Vector3.forward);
        Transform trail = Instantiate(shootTrail, firePoint.position, trailRotation);
        Destroy(trail.gameObject, 0.05f);

        // Debug.DrawLine(firePointPosition, shootDirection * 100, Color.red);

        MuzzleFlash();
    }

    private void ShootEffect(RaycastHit2D hit)
    {
        Vector3 firePointPosition = firePoint.position;
        Transform trail = Instantiate(shootTrail, firePointPosition, Quaternion.identity);
        LineRenderer lineRenderer = trail.GetComponent<LineRenderer>();
        Vector3 endPosition = new Vector3(hit.point.x, hit.point.y, firePointPosition.z);
        lineRenderer.useWorldSpace = true;
        lineRenderer.SetPosition(0, firePointPosition);
        lineRenderer.SetPosition(1, endPosition);
        Destroy(trail.gameObject, 0.05f);

        //打击效果
        Transform sparks = Instantiate(hitParticle, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(sparks.gameObject, 0.2f);

        //制造伤害
        IDamageable damageable = hit.transform.transform.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(10);
            PopupText.Create(hit.point, Random.Range(1, 100), Random.Range(0, 100) < 30);
        }
        
        MuzzleFlash();
    }

    private Vector2 GetShootDirection()
    {
        Vector2 shotDir = transform.up;

        shotDir.x += Random.Range(-playerData.AimOffset, playerData.AimOffset);
        shotDir.y += Random.Range(-playerData.AimOffset, playerData.AimOffset);

        return shotDir.normalized;
    }

    //枪口火光
    private void MuzzleFlash()
    {
        Transform flash = Instantiate(muzzleFlash, firePoint.position, firePoint.rotation);
        flash.SetParent(firePoint);
        float randomSize = Random.Range(0.6f, 0.9f);
        flash.localScale = new Vector3(randomSize, randomSize, randomSize);
        Destroy(flash.gameObject, 0.05f);

    }
    #endregion
}
