using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData 
{
    public int Vitality { get; set; }
    public int Agility { get; set; }
    public int Accuracy { get; set; }
    public WeaponData WeaponData { get; set; }

    //player
    public float AimOffset => 1f / Accuracy * 8.0f; //-0.16 to 0.16，升级时增大Accuracy
    public float MoveSpeed => 2f + Agility / 30.0f; //升级时增大Agility
    public float Health => Vitality; //升级时增大Vitality

    //weapon
    public int Damage
    {
        get => WeaponData.Damage;
        set => WeaponData.Damage = value;
    }

    public int ClipSize
    {
        get => WeaponData.ClipSize;
        set => WeaponData.ClipSize = value;
    }

    public int FireRate
    {
        get => WeaponData.FireRate;
        set => WeaponData.FireRate = value;
    }

    public int CriticalChance
    {
        get => WeaponData.CriticalChance;
        set => WeaponData.CriticalChance = Mathf.Clamp(value, 0, 100);
    }

    public float ReloadTime
    {
        get => WeaponData.ReloadTime; 
        set => WeaponData.ReloadTime = value;
    }

    public PlayerData()
    {
        Vitality = 150;
        Agility = 150;
        Accuracy = 50;

        WeaponData = new WeaponData(10, 21, 7, 10, 1.5f);
    }

    public (int, bool) CalculateDamage()
    {
        float damage = Damage + Damage * Random.Range(-0.1f, 0.1f);
        if (Random.Range(0, 101) > CriticalChance)
        {
            return ((int)damage, false);
        }

        damage *= Random.Range(2.0f, 3.0f);
        return ((int)damage, true);
    }
}
