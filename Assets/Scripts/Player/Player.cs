using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity
{
    private PlayerData playerData;

    protected override void Start()
    {
        playerData = new PlayerData();
        startHealth = playerData.Health;

        base.Start();

        //init player control
        GetComponent<PlayerController>().Init(playerData.MoveSpeed);

        //init weapon system
        GetComponentInChildren<PlayerGun>().Init(playerData);

        GameEvents.OnPlayerHealthChanged?.Invoke(Health, startHealth);
    }

    public override void TakeDamage(float damage)
    {
        if (damage >= Health && !IsDead)
        {
            GameEvents.GameOver?.Invoke();
        }

        base.TakeDamage(damage);

        GameEvents.OnPlayerHealthChanged?.Invoke(Health, startHealth);
    }

    [ContextMenu("Suicide")]
    public void Suicide()
    {
        if (!Application.isPlaying)
        {
            return;
        }
        TakeDamage(999);
    }
}
