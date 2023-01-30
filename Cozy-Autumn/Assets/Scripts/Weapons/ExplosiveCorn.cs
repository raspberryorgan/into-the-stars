using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveCorn : Weapon
{
    float explosionRadius = 1;
    public override void Shoot(PlayerController player)
    {
        for (int i = 0; i < currentAmmo; i++)
        {
            var a = GameManager.Instance.objectPool.SpawnPooledObject(spawneableBullet, player.transform.position);
            a.GetComponent<CornBullet>().Initialize(enemyMask, totalDamage, bulletLifeTime, bulletSpeed, i, currentAmmo, player.transform);
            a.GetComponent<CornBullet>().SetDir(player.transform.position  + new Vector3(+ Random.Range(-4, 4), Random.Range(-4, 4), 0).normalized, explosionRadius);
        }
    }
    protected override void OnLevelUp(int newLevel)
    {
        if (newLevel == 0)
        {
            AddNewBullet(GameManager.Instance.player);
        }
        else if (newLevel == 1 )
        {
            explosionRadius += 1;
        }
        else if(newLevel == 2 || newLevel == 4)
        {
            baseDamage += 10;
        }
        else if (newLevel == 3)
        {
            ReduceCooldown(3f);
        }
        else if (newLevel == 5)
        {
            explosionRadius += 2;
        }
    }
}
