using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PassivesUpgrades : MonoBehaviour
{
   public PassiveUpgradeButton currentUpg;
    public TMP_Text pasName;
    public TMP_Text pasInfo;
    public Button buyButton;
    public TMP_Text price;
    public TMP_Text coins;

    private void Start()
    {
        SetCurrent(currentUpg);
    }
    public void UpdateGold()
    {
        coins.text = GameManager.Instance.data.gold.ToString();
    }
    public void SetCurrent(PassiveUpgradeButton button)
    {
        pasName.text = button.pName;
        pasInfo.text = button.pInfo;
        currentUpg = button;

        if (GameManager.Instance.data.passivesDictionary[button.myType] < button.MaxLevel)
        {
            price.text = button.price[ GameManager.Instance.data.passivesDictionary[button.myType]].ToString("00.00");
            buyButton.interactable = true;
        }
        else
        {
            price.text = "Max Level";
            buyButton.interactable = false;
        }

        coins.text = GameManager.Instance.data.gold.ToString("00.00");
        
    }

    public void UpgradePassive()
    {
        if (GameManager.Instance.data.gold >= currentUpg.price[currentUpg.currentLevel])
        {
            GameManager.Instance.data.gold -= currentUpg.price[currentUpg.currentLevel];
            UpdateGold();

            GameManager.Instance.data.passivesDictionary[currentUpg.myType] += 1;
            currentUpg.UpdateLevel();
            SetCurrent(currentUpg);
        }
    }
}