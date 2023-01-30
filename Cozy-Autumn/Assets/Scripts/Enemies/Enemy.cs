using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IUpdate
{
    [HideInInspector] public float maxLife = 1;
    public float InitialLife = 1;
    [HideInInspector] public float damage = 1;
    public float InitialDamage = 1;
    public float speed = 2;
    public float xpDrop = 1;
    public float goldDrop = 10;
    public float goldDropChance = 1;
    protected Transform target;
    Rigidbody2D rb;
    float life = 0;
    public Experience experiencePF;
    public Gold goldPF;
    protected Animator anim;
    protected SpriteRenderer sr;
    public bool isMirrored;
    Color originalColor;
    public bool damageTrigger = true;

    public DamagedText damageText;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        life = maxLife;
    }
    private void Start()
    {
        UpdateManager.Instance.AddUpdate(this);
    }
    public virtual void OnUpdate()
    {
        Move();
    }
    public virtual void Move()
    {
        Vector3 dir = target.position - transform.position;
        transform.position += dir.normalized * Time.deltaTime * speed;
        transform.right = target.position - transform.position;
    }

    public void LevelUpEnemy(int level)
    {
        if (level > 1)
        {
            maxLife = InitialLife * Mathf.Pow(1.14f, level);
            damage = InitialDamage * Mathf.Pow(1.14f, level);
        }
        else
        {
            maxLife = InitialLife;
            damage = InitialDamage;
        }
    }

    public virtual void SetTarget(Transform _target)
    {
        life = maxLife;
        target = _target;
        sr.color = originalColor;
    }

    public void TakeDamage(float dmg)
    {
        life -= dmg;
        GameManager.Instance.objectPool.SpawnPooledObject(damageText, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity).SetDamage(dmg);
        if (life <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(ReceiveDamage());
        }
    }

    IEnumerator ReceiveDamage()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = originalColor;

    }
   public virtual void Die()
    {
        if (Random.Range(0, 11) < goldDropChance)
        {
            GameManager.Instance.objectPool.SpawnPooledObject(goldPF, transform.position, Quaternion.identity).SetGoldAmmount(goldDrop);
        }
        AudioManager.instance.Play("EnemyDie");

        GameManager.Instance.objectPool.SpawnPooledObject(experiencePF, transform.position, Quaternion.identity).SetXpAmmount(xpDrop, new Vector3(0.5f,0.5f,0.5f));
        GameManager.Instance.objectPool.ReturnToPool(transform);
        AddKill();
    }

    public virtual void AddKill()
    {
        GameManager.Instance.player.AddKill();
    }
}
