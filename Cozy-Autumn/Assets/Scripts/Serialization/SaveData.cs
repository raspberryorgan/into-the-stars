using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{    
    [Header("Passives")]
    public int pasDamageLvl;
    public int pasSpeedLvl;
    public int pasArmorLvl;
    public int pasXpGainLvl;
    public int pasGoldGainlvl;
    public int pasMaxHealthLvl;
    public int pasHPRegenLvl;
    public int pasXpRangeLvl;

    public int startWithUpgrade;

    public float gold;

    public float sfxVolume;
    public float musicVolume;


    public UpgradeSystem upgrades;
    public static SaveData FromJson(string jsonString)
    {
        return JsonUtility.FromJson<SaveData>(jsonString);
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this, true);
    }
}
