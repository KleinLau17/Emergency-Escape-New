using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData 
{
    public int Vitality { get; set; }
    public int Agility { get; set; }
    public int Accuracy { get; set; }

    //player
    public float AimOffset => 1f / Accuracy * 8.0f; //-0.16 to 0.16，升级时增大Accuracy
    public float MoveSpeed => 2f + Agility / 30.0f; //升级时增大Agility
    public float Health => Vitality; //升级时增大Vitality

    public PlayerData()
    {
        Vitality = 150;
        Agility = 80;
        Accuracy = 50;
    }

}
