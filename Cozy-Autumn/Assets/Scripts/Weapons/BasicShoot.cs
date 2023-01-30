using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShoot : Weapon
{
    int bulletLife = 1;
    public override void Shoot(PlayerController player)
    {
        if (currentAmmo == 0) AddNewBullet(GameManager.Instance.player);
        for (int i = 0; i < currentAmmo; i++)
        {
            var a = GameManager.Instance.objectPool.SpawnPooledObject(spawneableBullet, player.transform.position /*+ new Vector3(0, Random.Range(-1f, -1f)*/);
            a.Initialize(enemyMask, totalDamage, bulletLifeTime, bulletSpeed, i, currentAmmo, player.transform, bulletLife);
            a.transform.up = player.AimDir;
        }
        AudioManager.instance.Play("Shoot");
    }

    public override void UpdateTimer()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= cooldown)
        {
            if (Input.GetMouseButton(0))
            {
                cooldownTimer = 0;
                Shoot(GameManager.Instance.player);
            }
        }
    }


    protected override void OnLevelUp(int newLevel)
    {
        if (newLevel == 0)
        {
            AddNewBullet(GameManager.Instance.player);
        }
        else if (newLevel == 1 || newLevel == 4)
        {
            ReduceCooldown(0.2f);
        }
        else if (newLevel == 2 )
        {
            baseDamage += 5;
        }
        else if (newLevel == 5)
        {
            baseDamage += 10;
        }
        else if(newLevel == 3)
        {
            bulletLife += 1;
        }
        else if (newLevel == 6)
        {
            bulletLife += 2;
        }
    }
}
