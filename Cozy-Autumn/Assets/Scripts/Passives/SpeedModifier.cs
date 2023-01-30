using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedModifier: Passive
{  
    protected override void OnLevelUp(int newLevel)
    {
        GameManager.Instance.player.SpeedMultiplier( ModifyStat(newLevel));
    }
}