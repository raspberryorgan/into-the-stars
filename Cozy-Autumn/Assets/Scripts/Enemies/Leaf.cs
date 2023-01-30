using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    public float maxLife = 1;
    public float damage = 1;
    public float speed = 2;
    public float xpDrop = 1;
    Transform target;
    Rigidbody2D rb;
    float life = 0;
    public Experience experiencePF;
    public Gold goldPF;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        life = maxLife;
    }
    private void Update()
    {
        Move();
    }
    public void Move()
    {
        Vector3 dir = target.position - transform.position;
        transform.position += dir.normalized * Time.deltaTime * speed;
    }

    public void SetTarget(Transform _target)
    {
        life = maxLife;
        target = _target;
    }

    public void TakeDamage(float dmg)
    {
        life -= dmg;

        Debug.Log("REcibe: " + dmg);

        if(life <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        if(Random.Range(0,11) == 0)
        {
            GameManager.Instance.objectPool.SpawnPooledObject(goldPF, transform.position, Quaternion.identity).SetGoldAmmount(1);
        }

        GameManager.Instance.objectPool.SpawnPooledObject(experiencePF, transform.position, Quaternion.identity).SetXpAmmount(xpDrop, new Vector3(0.5f, 0.5f, 0.5f));
        GameManager.Instance.objectPool.ReturnToPool(transform);
        GameManager.Instance.player.AddKill();
    }
}
