using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public abstract class PowerUp : MonoBehaviour
{
    [Header("For UI")]
    public Sprite icon;
    public string powerUpName;
    public string[] descriptionPerLevel;

    public int maxLevel = 10;
    public int currentLevel = 0;

    public powerUpType myType;

}

public enum powerUpType
{
    basicShoot,
    corn,
    scythe,
    crow,
    armor,
    damage,
    xp,
    gold,
    hp,
    regen,
    speed
}

