using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradePanel : MonoBehaviour
{
    public Image icon;
    public TMP_Text upgradeName;
    public TMP_Text upgradeInfo;
    PowerUp curr;
    public void SetPanel(PowerUp p)
    {
        if (p == null)
        {
            curr = null;
            icon.sprite = null;
            upgradeName.text = "";
            upgradeInfo.text = "";
        }
        else
        {
            curr = p;
            icon.sprite = p.icon;
            upgradeName.text = p.powerUpName;
            upgradeInfo.text = p.descriptionPerLevel[p.currentLevel];
        }
    }

    public void ApplyUpgrade()
    {
        GameManager.Instance.UpgradeSystem.ApplyUpgrade(curr);
    }

}
