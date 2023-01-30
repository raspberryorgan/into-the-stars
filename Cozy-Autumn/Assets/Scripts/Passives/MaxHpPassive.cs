using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHpPassive : Passive
{
    protected override void OnLevelUp(int newLevel)
    {
        GameManager.Instance.player.MaxLifeModifier(ModifyStat(newLevel));
    }
}
