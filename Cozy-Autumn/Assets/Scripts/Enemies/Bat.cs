using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    Vector3 dir;

    public override void SetTarget(Transform _target)
    {
        base.SetTarget(_target);

        dir = target.position - transform.position;
        if (sr == null) sr = GetComponent<SpriteRenderer>();
        sr.flipX = dir.x > 0;
    }
    float timer = 0;
    public override void Move()
    {
        timer += Time.deltaTime;

        Vector3 sinMove = new Vector3(0, 0, 3 * Mathf.Cos(timer * 3));

        transform.position += (Vector3.Cross(dir.normalized, sinMove) + dir.normalized) * Time.deltaTime;
        Debug.Log(Vector3.Distance(GameManager.Instance.player.transform.position, transform.position));
        if ( timer >= 10 && Vector3.Distance(GameManager.Instance.player.transform.position,  transform.position) >= 12)
        {
            GameManager.Instance.objectPool.ReturnToPool(transform);
        }
    }
}
