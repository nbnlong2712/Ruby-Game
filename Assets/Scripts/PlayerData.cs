using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int level;
    public float positionX;
    public float positionY;
    public float health;
    public int ammo;

    public PlayerData(int level, float positionX, float positionY, float health, int ammo)
    {
        this.level = level;
        this.positionX = positionX;
        this.positionY = positionY;
        this.health = health;
        this.ammo = ammo;
    }
}
