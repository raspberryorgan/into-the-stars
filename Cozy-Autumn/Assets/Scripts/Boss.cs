using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{

    public override void AddKill()
    {
        GameManager.Instance.player.AddBoss();
    }

    public override void Die()
    {
        if (Random.Range(0, 11) < goldDropChance)
        {
            GameManager.Instance.objectPool.SpawnPooledObject(goldPF, transform.position, Quaternion.identity).SetGoldAmmount(goldDrop);
        }
        AudioManager.instance.Play("EnemyDie");

        GameManager.Instance.objectPool.SpawnPooledObject(experiencePF, transform.position, Quaternion.identity).SetXpAmmount(xpDrop, Vector3.one, true);
        GameManager.Instance.objectPool.ReturnToPool(transform);
        AddKill();
    }

}
