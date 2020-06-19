using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public float lifepoints;            
    public float maxLifepoints;
    public int money = 0;
    public int keys = 0;
    public float weaponShootingTime;        //fireRate
    public float damage;
    public string nameWeapon;
    public string bulletName;

    public Player(float lifepoints, float maxLifepoints, int money, int keys, float weaponShootingTime, float damage, string nameWeapon, string bulletName)
    {
        this.lifepoints = lifepoints;
        this.maxLifepoints = maxLifepoints;
        this.money = money;
        this.keys = keys;
        this.weaponShootingTime = weaponShootingTime;
        this.damage = damage;
        this.nameWeapon = nameWeapon;
        this.bulletName = bulletName;
    }
}
