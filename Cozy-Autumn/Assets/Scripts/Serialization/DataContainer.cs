using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataContainer : MonoBehaviour
{
    int pasDamageLvl;
    int pasSpeedLvl;
    int pasArmorLvl;
    int pasXpGainLvl;
    int pasGoldGainlvl;
    int pasMaxHealthLvl;
    int pasHpRegenLvl;
    int pasXpRangeLvl;
    int startWithUpgradeLvl;
    public float gold;

    public float sfxVolume = 1;
    public float musicVolume = 1;

    bool initialized = false;

    public Dictionary<Passives, int> passivesDictionary = new Dictionary<Passives, int>();
    private void Awake()
    {
        if (GameManager.Instance.data == null)
        {
            DontDestroyOnLoad(gameObject);
            if (!initialized)
                InitializeDict();
            GameManager.Instance.data = this;
        }
        else
            Destroy(gameObject);
    }

    public void InitializeDict()
    {
        passivesDictionary.Add(Passives.damage, pasDamageLvl);
        passivesDictionary.Add(Passives.speed, pasSpeedLvl);
        passivesDictionary.Add(Passives.armor, pasArmorLvl);
        passivesDictionary.Add(Passives.xpGain, pasXpGainLvl);
        passivesDictionary.Add(Passives.goldGain, pasGoldGainlvl);
        passivesDictionary.Add(Passives.maxHP, pasMaxHealthLvl);
        passivesDictionary.Add(Passives.hpRegen, pasHpRegenLvl);
        passivesDictionary.Add(Passives.xpRange, pasXpRangeLvl);
        passivesDictionary.Add(Passives.startWithUpgrade, startWithUpgradeLvl);

        initialized = true;
    }

    public float DamageMultiplier()
    {
        return 1 + passivesDictionary[Passives.damage] * 10f / 100;
    }
    public float SpeedMultiplier()
    {
        return 1 + passivesDictionary[Passives.speed] * 5f / 100;
    }
    public float ArmorMultiplier()
    {
        return 1 + passivesDictionary[Passives.armor] * 5f / 100;
    }
    public float XpGainMultiplier()
    {
        return 1 + passivesDictionary[Passives.xpGain] * 10f / 100;
    }
    public float GoldGainMultiplier()
    {
        return 1 + passivesDictionary[Passives.goldGain] * 15f / 100;
    }
    public float MaxHpMultiplier()
    {
        return 1 + passivesDictionary[Passives.maxHP] * 20f / 100;
    }
    public float HpRegenFromShop()
    {
        return passivesDictionary[Passives.hpRegen] * 0.1f;
    }
    public float XpRange()
    {
        return  1 + passivesDictionary[Passives.xpRange] * 0.5f;
    }

    public bool StartsWithUpgrade()
    {
        return passivesDictionary[Passives.startWithUpgrade] != 0;
    }
}

public enum Passives
{
    damage,
    speed,
    xpGain,
    goldGain,
    cooldown,
    armor,
    maxHP,
    hpRegen,
    xpRange,
    startWithUpgrade
}