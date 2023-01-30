using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModifier : Passive
{
    protected override void OnLevelUp(int newLevel)
    {
        GameManager.Instance.player.DamageMultiplier(ModifyStat(newLevel));
    }


}
