using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Passive: PowerUp
{
    [Header("Passive stats")]
    [SerializeField] bool byPercent;

    [SerializeField] float ammountToAddByPercent;
    [SerializeField] float ammountToAdd;

    public float ModifyStat(int level)
    {
        if (byPercent)
        {
            return (level * ammountToAddByPercent) / 100f;
        }
        else
        {
            return ammountToAdd * level;
        }
    }

    public void LevelUp()
    {
        currentLevel++;
        OnLevelUp(currentLevel);
    }
    protected abstract void OnLevelUp(int newLevel);
}
