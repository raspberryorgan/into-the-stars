using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow : Weapon
{
    public override void Shoot(PlayerController player)
    {
        Debug.Log(currentAmmo);
        for (int i = 0; i < currentAmmo; i++)
        {
            int aux = 0;
            aux = i;
            GameManager.Instance.objectPool.SpawnPooledObject(spawneableBullet, player.transform.position)
              .Initialize(enemyMask, totalDamage, bulletLifeTime, bulletSpeed, aux, currentAmmo, player.transform, 999);
        }
    }
   
    protected override void OnLevelUp(int newLevel)
    {
        if (newLevel == 0 || newLevel == 1 || newLevel == 2 || newLevel == 4 )
        {
            AddNewBullet(GameManager.Instance.player);

        }
        if (newLevel == 3 || newLevel == 5)
        {
            baseDamage += 5;

        }        
    }
}

