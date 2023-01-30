using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldGainPassive : Passive
{
    protected override void OnLevelUp(int newLevel)
    {
        GameManager.Instance.player.GoldModifier(ModifyStat(newLevel));
    }
}
