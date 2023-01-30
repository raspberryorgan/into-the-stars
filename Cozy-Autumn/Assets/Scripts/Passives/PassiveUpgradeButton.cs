using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PassiveUpgradeButton : MonoBehaviour
{
    public string pName;
    public string pInfo;
    public int[] price;

    public int MaxLevel;
    public int currentLevel;
    public Passives myType;

    public TMP_Text level;

    public void UpdateLevel()
    {
        level.text = GameManager.Instance.data.passivesDictionary[myType] + "/" + MaxLevel;
    }
    public void Start()
    {
        currentLevel = GameManager.Instance.data.passivesDictionary[myType];
        UpdateLevel();
    }
}
