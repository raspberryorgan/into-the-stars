using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheThrow : Weapon
{
    public float initialForce = 2f;
    public override void Shoot(PlayerController player)
    {
        Debug.Log(currentAmmo);
        for (int i = 0; i < currentAmmo; i++)
        {
            Bullet a = GameManager.Instance.objectPool.SpawnPooledObject(spawneableBullet, player.transform.position);
            a.Initialize(enemyMask, totalDamage, bulletLifeTime, bulletSpeed, i, currentAmmo, player.transform);
            a.transform.up = player.AimDir;


            float size = -(currentAmmo / 2) * (40 / currentAmmo) + i * 40 / currentAmmo;
            float size2 = -(currentAmmo / 2) * (40 / currentAmmo) + (i + 1) * 40 / currentAmmo;

            a.GetComponent<Rigidbody2D>().AddForce(Vector3.up * initialForce + Random.Range(size, size2) * Vector3.right);
        }

        AudioManager.instance.Play("ThrowScythe");
    }

    protected override void OnLevelUp(int newLevel)
    {
        if (newLevel == 0 || newLevel == 1 || newLevel == 4)
        {
            AddNewBullet(GameManager.Instance.player);
        }
        else if(newLevel == 2 || newLevel == 3)
        {
            baseDamage += 6;
        }
        else if(newLevel == 5)
        {
            ReduceCooldown(1.5f);
        }
    }
}

