using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CornBullet : Bullet
{
    Vector3 targetPos;
    public ParticleSystem particles;
    public float range = 1f;

    bool explode = false;
    public void SetDir(Vector3 targetP, float radius)
    {
        targetPos = targetP * (Random.Range(5, 15) / 10f);  
        timer = 0;
        explode = false;
        range = radius;
    }
    public override void OnUpdate()
    {
        Vector3 dir = targetPos - transform.position;
        if (dir.magnitude > 0.1f)
        {
            transform.position += dir * speed * Time.deltaTime;
        }
        else
        {
            if (!explode)
            {
                particles.transform.localScale = new Vector3(range, range, 1);
                particles.Play();
                AudioManager.instance.Play("Explosion");
                var a = Physics2D.OverlapCircleAll(transform.position, range * 0.8f, enemyMask);
                foreach (var item in a.ToList())
                {
                    var i = item.GetComponent<Enemy>();
                    i.TakeDamage(damage);
                }
                explode = true; 
            }           

            timer += Time.deltaTime;
            if (timer > 0.7f)
            {
                timer = 0;
                GameManager.Instance.objectPool.ReturnToPool(transform);
            }
        }
    }

}
