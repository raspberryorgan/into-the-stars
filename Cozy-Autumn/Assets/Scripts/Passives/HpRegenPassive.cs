using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpRegenPassive : Passive
{
    protected override void OnLevelUp(int newLevel)
    {
        GameManager.Instance.player.HpRegenModifier(ModifyStat(newLevel));
    }
}
