using System;
using System.Collections;
using System.Collections.Generic;
using TinyCore;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startHealth;
    protected float Health { get; private set; }
    protected bool IsDead;

    public ParticleSystem deathParticle;
    public string deathSound;

    public event Action OnDeath;

    protected virtual void Start()
    {
        Health = startHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;

        if (Health > 0 || IsDead) return;

        IsDead = true;
        OnDeath?.Invoke();

        Destroy(gameObject);

        //À¿ÕˆÃÿ–ß
        if (deathParticle == null)
        {
            return;
        }

        ParticleSystem ps = Instantiate(deathParticle, transform.position, Quaternion.identity);
        Destroy(ps.gameObject, ps.main.duration);

        //À¿Õˆ“Ù–ß
        if (!string.IsNullOrEmpty(deathSound))
        {
            SoundManager.Instance.PlaySound(deathSound);
        }
    }
}
