using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpPassive : Passive
{
    protected override void OnLevelUp(int newLevel)
    {
        GameManager.Instance.player.XpModifier(ModifyStat(newLevel));
    }
}
