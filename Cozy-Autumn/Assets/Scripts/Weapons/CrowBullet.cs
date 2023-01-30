using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowBullet : Bullet
{
    public override void OnUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            timer = 0;
            GameManager.Instance.objectPool.ReturnToPool(transform);
            UpdateManager.Instance.RemoveUpdate(this);
        }

        int a = (360 / maxIndex) * index;
        if (timer < lifetime * .3f)
        {
            transform.position = targetPlayer.position + new Vector3(2 * timer * Mathf.Sin(3 * timer + a * Mathf.Deg2Rad), -2 * timer * Mathf.Cos(3 * timer + a * Mathf.Deg2Rad), transform.position.z);
        }
        else
            transform.position = targetPlayer.position + new Vector3(2 * lifetime * .3f * Mathf.Sin(3 * timer + a * Mathf.Deg2Rad), -2 * lifetime * .3f * Mathf.Cos(3 * timer + a * Mathf.Deg2Rad), transform.position.z);
    }
}

