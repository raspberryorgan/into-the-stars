using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UpgradeSystem : MonoBehaviour, IUpdate
{
    public List<PowerUp> powerUps = new List<PowerUp>();
    [SerializeField] Dictionary<powerUpType, PowerUp> allUpgradesAvailables = new Dictionary<powerUpType, PowerUp>();

    [SerializeField] Dictionary<powerUpType, Weapon> equipedWeapons = new Dictionary<powerUpType, Weapon>();
    Dictionary<powerUpType, Passive> equipedPassives = new Dictionary<powerUpType, Passive>();

    PlayerController player;

    bool instanced = false;
    private void OnEnable()
    {
        if (instanced) return;

        for (int i = 0; i < powerUps.Count; i++)
        {
            if (!allUpgradesAvailables.ContainsKey(powerUps[i].myType))
            {
                allUpgradesAvailables.Add(powerUps[i].myType, powerUps[i]);

            }
        }
        instanced = true;
        UpdateManager.Instance.AddUpdate(this);
        GameManager.Instance.UpgradeSystem = this;
    }

    public List<PowerUp> GetUpgrade(int size)
    {
        List<PowerUp> availables = new List<PowerUp>();

        foreach (var item in allUpgradesAvailables)
        {
            if (equipedPassives.ContainsKey(item.Key))
            {
                availables.Add(equipedPassives[item.Key]);
            }else if (equipedWeapons.ContainsKey(item.Key))
            {
                availables.Add(equipedWeapons[item.Key]);
            }
            else
            {
                availables.Add(item.Value);
            }
        }

        foreach (var item in availables.ToList())
        {          
            if (item.currentLevel >= item.maxLevel)
            {
                availables.Remove(item);
            }
        }

        List<PowerUp> toReturn = new List<PowerUp>();

        for (int i = 0; i < size; i++)
        {
            if (availables.Count == 0) continue;
            int rand = Random.Range(0, availables.Count);
            toReturn.Add(availables[rand]);
            availables.RemoveAt(rand);
        }

        return toReturn;
    }

    public void ApplyUpgrade(PowerUp upgrade)
    {
        if (upgrade.GetComponent<Weapon>())
        {
            EquipWeapon(upgrade.GetComponent<Weapon>());
        }
        else if (upgrade.GetComponent<Passive>())
        {
            AddPassive(upgrade.GetComponent<Passive>());
        }
    }

    public int CheckPassiveLevel(Passive passive)
    {
        return passive.currentLevel;
    }
    public void AddPassive(Passive passive)
    {
        if (!equipedPassives.ContainsKey(passive.myType))
        {
            Passive p = Instantiate(passive, transform);
            equipedPassives.Add(passive.myType, p);
        }
        equipedPassives[passive.myType].LevelUp();

        if (equipedPassives[passive.myType].GetComponent<DamageModifier>() != null)
        {
            foreach (var item in equipedWeapons)
            {
                item.Value.SetDamage(GameManager.Instance.player.TotalDamage);
            }
        }
    }

    public void EquipWeapon(Weapon weapon)
    {
        if (!equipedWeapons.ContainsKey(weapon.myType))
        {
            Weapon w = Instantiate(weapon, transform);
            equipedWeapons.Add(weapon.myType, w);
            equipedWeapons[weapon.myType].currentLevel = 0;
        }
        equipedWeapons[weapon.myType].LevelUp();
        equipedWeapons[weapon.myType].SetDamage(GameManager.Instance.player.TotalDamage);
    }
    public void OnUpdate()
    {
        foreach (var item in equipedWeapons.ToList())
        {
            item.Value.UpdateTimer();
        }
    }
}