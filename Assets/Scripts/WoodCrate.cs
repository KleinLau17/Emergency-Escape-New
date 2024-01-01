using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

public class WoodCrate : LivingEntity
{
    public override void TakeDamage(float damage)
    {
        if (damage >= Health && !IsDead)
        {
            OnDeath += () => { };
        }

        //ËÀÍöÌØĞ§

        //ËÀÍöÒôĞ§

        base.TakeDamage(damage);
    }
}
