using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : PowerUp
{
    [Header("Weapon stats")]
    public float baseDamage;
    public float totalDamage;
    public float cooldown;
    protected float cooldownTimer = 0;
    public LayerMask enemyMask;

    public Bullet spawneableBullet;
    public float bulletLifeTime;
    public float bulletSpeed;

    protected int currentAmmo = 0;
    public int maxAmmo;

    void Start()
    {
        cooldownTimer = cooldown;
    }
    public virtual void UpdateTimer()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= cooldown)
        {
            Shoot(GameManager.Instance.player);
            cooldownTimer = 0;
        }
    }
    public void SetDamage(float damageMultiplier)
    {
        totalDamage = baseDamage * damageMultiplier;
    }

    public abstract void Shoot(PlayerController player);

    public virtual void AddNewBullet(PlayerController player)
    {
        if (currentAmmo >= maxAmmo) return;

        currentAmmo += 1;        
    }

    public void LevelUp()
    {
        OnLevelUp(currentLevel);
        currentLevel++;
    }
    public void ReduceCooldown(float cant)
    {
        cooldown = Mathf.Clamp(cooldown - cant, 0.1f, 5);
    }
    protected abstract void OnLevelUp(int newLevel);

   
}
