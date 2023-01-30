using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorModifier : Passive
{
    protected override void OnLevelUp(int newLevel)
    {
        GameManager.Instance.player.ArmorMultiplier(ModifyStat(newLevel));
    }
}
