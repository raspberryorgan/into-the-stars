using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IUpdate
{
    public LayerMask enemyMask;
    public float damage;
    protected float timer = 0;
    protected float lifetime;
    protected float speed;

    protected int index;
    protected int life = 1;
    protected int maxIndex;
    protected Transform targetPlayer;
    public void Initialize(LayerMask _enemyMask, float dmg, float _lifetime, float _speed, int _index, int _maxIndex, Transform _targetPlayer, int _life = 999)
    {
        enemyMask = _enemyMask;
        damage = dmg;
        lifetime = _lifetime;
        speed = _speed;
        index = _index;
        maxIndex = _maxIndex;
        targetPlayer = _targetPlayer;
        life = _life;

        UpdateManager.Instance.AddUpdate(this);
    }

    public virtual void TakeDamage()
    {
        life--;
        if (life <= 0)
        {
            UpdateManager.Instance.RemoveUpdate(this);
            GameManager.Instance.objectPool.ReturnToPool(transform);
            timer = 0;
        }
    }

    public void SetIndex(int _index, int _maxIndex)
    {
        index = _index;
        maxIndex = _maxIndex;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Enemy a = collision.gameObject.GetComponent<Enemy>();
            TakeDamage();
            a.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Enemy a = collision.gameObject.GetComponent<Enemy>();
            TakeDamage();
            a.TakeDamage(damage);
        }
    }

    public virtual void OnUpdate()
    {
        if (timer >= lifetime)
        {
            timer = 0;
            GameManager.Instance.objectPool.ReturnToPool(transform);
            UpdateManager.Instance.RemoveUpdate(this);
        }
        transform.position += transform.up * Time.deltaTime * speed;
        timer += Time.deltaTime;
    }
}
