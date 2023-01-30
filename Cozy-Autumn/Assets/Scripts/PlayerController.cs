using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IUpdate
{
    public int level = 1;
    public float exp = 0;
    public float expToLevel = 10;

    public float speed;
    [HideInInspector] public Vector3 lastDir = Vector3.zero;

    public UpgradeSystem upgrades;

    public float MaxLifeDefault = 10;
    float maxLifeBase;
    float currentLife;

    Animator anim;
    UIManager ui;
    [SerializeField] PlayerStats stats;

    float damageModifier = 1;
    public float speedModifier = 1;
    float maxLifeModifier = 1;
    float goldModifier = 1;
    float xpModifier = 1;
    float armorModifier = 1;
    float hpRegenExtra = 0;

    float inGameDamageModifier = 1;
    public float inGameSpeedModifier = 1;
    float inGameMaxLifeModifier = 1;
    float inGameGoldModifier = 1;
    float inGameXpModifier = 1;
    float inGameHpRegen = 0f;
    float inGameArmorModifier = 1;

    private void Awake()
    {
        GameManager.Instance.player = this;
    }

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        level = 1;
        ui = GameManager.Instance.UIManager;
        expToLevel = 10;
        damageModifier = GameManager.Instance.data.DamageMultiplier();
        speedModifier = GameManager.Instance.data.SpeedMultiplier();
        xpModifier = GameManager.Instance.data.XpGainMultiplier();
        maxLifeModifier = GameManager.Instance.data.MaxHpMultiplier();
        goldModifier = GameManager.Instance.data.GoldGainMultiplier();
        armorModifier = GameManager.Instance.data.ArmorMultiplier();
        hpRegenExtra = GameManager.Instance.data.HpRegenFromShop();

        UpdateManager.Instance.AddUpdate(this);

        upgrades.EquipWeapon(baseWeapon);
        maxLifeBase = MaxLifeDefault * maxLifeModifier;
        currentLife = maxLifeBase;
        ui.UpdateLife(currentLife, maxLifeBase);

        if (GameManager.Instance.data.StartsWithUpgrade())
        {
            LevelUp();
        }
    }
    public void OnUpdate()
    {
        Move();
        Aim();
        UpdateTimer();
        Regen();
    }
    [SerializeField] Weapon baseWeapon;

    float hpRegenTimer = 0;
    public void Regen()
    {
        hpRegenTimer += Time.deltaTime;

        if (hpRegenTimer >= 1f)
        {
            if (currentLife + inGameHpRegen < maxLifeBase)
            {
                currentLife += inGameHpRegen + hpRegenExtra;
                ui.UpdateLife(currentLife, maxLifeBase);
            }
            hpRegenTimer = 0;
        }
    }

    public Vector2Int TimeSurvived
    {
        get
        {
            int mins = (int)stats.timeSurvived / 60;
            int secs = (int)stats.timeSurvived % 60;
            return new Vector2Int(mins, secs);
        }
    }
    public void UpdateTimer()
    {
        stats.UpdateTime();


        ui.UpdateTimer(TimeSurvived.x, TimeSurvived.y);
    }

    float FinalSpeed { get { return speed * speedModifier * inGameSpeedModifier; } }
    public void Move()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            lastDir = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime * FinalSpeed;
        transform.position += lastDir;

        GameManager.Instance.UIManager.UpdateLifeBarPosition(transform.position - Vector3.up);
    }

    public Vector3 aimDir = Vector3.zero;
    public Vector3 AimDir { get { return aimDir; } set { } }
    public void Aim()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        aimDir = mousePos - transform.position;
        aimDir.z = 0;

        anim.SetFloat("DirX", aimDir.x);
        anim.SetFloat("DirY", aimDir.y);
    }
    public void AddExperience(float xp)
    {
        exp += xp * xpModifier * inGameXpModifier;
        extraXp = exp;
        if (exp >= expToLevel)
        {
            LevelUp();
        }
        else
        {
            AudioManager.instance.Play("PickupCoin");
        }
        ui.UpdateExpAmmount(exp, expToLevel);
    }
    float extraXp = 0;
    public void LevelUp()
    {
        Debug.Log("LEVLE UP: " + expToLevel);
        AudioManager.instance.Play("LevelUp");
        level++;
        exp = extraXp - expToLevel;
        expToLevel += 5;

        ui.UpdateLevel(level);
        
        if(exp >= expToLevel)
            ui.TurnOnUpgradePanel(LevelUp);
        else
            ui.TurnOnUpgradePanel(null);


        ui.UpdateExpAmmount(exp, expToLevel);
    }

    public void TakeDamage(float damage)
    {
        currentLife -= damage * armorModifier * inGameArmorModifier;
        ui.UpdateLife(currentLife, maxLifeBase);
        AudioManager.instance.Play("Hurt");
        if (currentLife <= 0)
        {
            Die();
        }
    }

    public void AddGold(float gold)
    {
        float g = gold * goldModifier * inGameGoldModifier;
        stats.gold += g;
        ui.AddGold(g);
    }
    public void AddKill()
    {
        stats.kills++;
        ui.UpdateKill(stats.kills);
    }

    public void AddBoss()
    {
        stats.bossKilled++;
        ui.UpdateKill(stats.kills);
    }
    bool dead = false;
    void Die()
    {
        GameManager.Instance.leaderboard.SetScore(stats.TotalScore());
        ui.SetStats(TimeSurvived, stats.kills, stats.bossKilled, stats.timeScore, stats.killScore, stats.bossScore);

        GameManager.Instance.data.gold += stats.gold;
        GameManager.Instance.saveManager.Save();

        UpdateManager.Instance.ClearAll();
    }

    public int TotalScore { get { return stats.TotalScore(); } }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            TakeDamage(collision.gameObject.GetComponent<Enemy>().damage);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Enemy e = collision.gameObject.GetComponent<Enemy>();
            if (e.damageTrigger)
                TakeDamage(collision.gameObject.GetComponent<Enemy>().damage);
        }
    }


    #region inGamePassives
    public void SpeedMultiplier(float value)
    {
        inGameSpeedModifier = 1 + value;
    }
    public void DamageMultiplier(float value)
    {
        inGameDamageModifier = 1 + value;
    }
    public void MaxHpModifier(float value)
    {
        inGameMaxLifeModifier = 1 + value;
    }
    public void XpModifier(float value)
    {
        inGameXpModifier = 1 + value;
    }
    public void GoldModifier(float value)
    {
        inGameGoldModifier = 1 + value;
    }
    public void HpRegenModifier(float value)
    {
        inGameHpRegen = value;
    }
   
    public void MaxLifeModifier(float value)
    {
        inGameMaxLifeModifier = value;

        float vidaantesdeupgrade = maxLifeBase;
        maxLifeBase = MaxLifeDefault * maxLifeModifier * ( 1+ inGameMaxLifeModifier);
        float next = maxLifeBase;
        currentLife += next - vidaantesdeupgrade;
    }
    public void ArmorMultiplier(float value)
    {
        inGameDamageModifier = 1 - value;
    }

   
    public float TotalDamage
    {
        get
        {
            return damageModifier * inGameDamageModifier;
        }
    }
    #endregion
}
