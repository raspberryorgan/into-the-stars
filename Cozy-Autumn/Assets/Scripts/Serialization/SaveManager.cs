using System;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private SaveData saveData;
    private string savePath;
    private void Awake()
    {
        if (saveData == null)
        {
            saveData = new SaveData();
        }
        GameManager.Instance.saveManager = this;

        savePath = Application.persistentDataPath + "/Save1.sav";

        //savePath = Application.dataPath + "/Save1.sav";

        //savePath = Application.streamingAssetsPath + "/Save1.sav";

        Debug.Log(savePath);
        if (File.Exists(savePath))
        {
            Debug.Log("try load");
            Load();
        }
    }

    public void Save()
    {
        StreamWriter streamWriter = null;
        try
        {
            if (!File.Exists(savePath))
            {
                streamWriter = File.CreateText(savePath);
                Debug.Log("Created path: " + savePath);
            }
            else
            {
                Debug.Log("Saved on path: " + savePath);
                streamWriter = new StreamWriter(savePath, false);
            }

            saveData.pasDamageLvl = GameManager.Instance.data.passivesDictionary[Passives.damage];
            saveData.pasArmorLvl = GameManager.Instance.data.passivesDictionary[Passives.armor];
            saveData.pasSpeedLvl = GameManager.Instance.data.passivesDictionary[Passives.speed];
            saveData.pasGoldGainlvl = GameManager.Instance.data.passivesDictionary[Passives.goldGain];
            saveData.pasXpGainLvl = GameManager.Instance.data.passivesDictionary[Passives.xpGain];
            saveData.pasMaxHealthLvl = GameManager.Instance.data.passivesDictionary[Passives.maxHP];
            saveData.pasHPRegenLvl = GameManager.Instance.data.passivesDictionary[Passives.hpRegen];
            saveData.pasXpRangeLvl = GameManager.Instance.data.passivesDictionary[Passives.xpRange];
            saveData.startWithUpgrade = GameManager.Instance.data.passivesDictionary[Passives.startWithUpgrade];

            saveData.gold = GameManager.Instance.data.gold;

            saveData.sfxVolume = GameManager.Instance.data.sfxVolume;
            saveData.musicVolume = GameManager.Instance.data.musicVolume;

            string enctryptedJson = Enctryptor.EncryptDecrypt(saveData.ToJson());
            streamWriter.Write(enctryptedJson);
            //streamWriter.Write(saveData.ToJson()) ;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        finally
        {
            if (streamWriter != null)
            {
                streamWriter.Close();
            }
        }
    }

    public void Load()
    {
        Debug.Log("LOADING STATS");
        if (File.Exists(savePath))
        {
            StreamReader streamReader = null;
            try
            {
                streamReader = new StreamReader(savePath);

                Debug.Log("ReadingJson");
                string decryptedJson = Enctryptor.EncryptDecrypt(streamReader.ReadToEnd());

                saveData = SaveData.FromJson(decryptedJson);
                //saveData = SaveData.FromJson(streamReader.ReadToEnd());

                GameManager.Instance.data.passivesDictionary[Passives.damage] = saveData.pasDamageLvl;
                GameManager.Instance.data.passivesDictionary[Passives.speed] = saveData.pasSpeedLvl;
                GameManager.Instance.data.passivesDictionary[Passives.armor] = saveData.pasArmorLvl;
                GameManager.Instance.data.passivesDictionary[Passives.goldGain] = saveData.pasGoldGainlvl;
                GameManager.Instance.data.passivesDictionary[Passives.xpGain] = saveData.pasXpGainLvl;
                GameManager.Instance.data.passivesDictionary[Passives.maxHP] = saveData.pasMaxHealthLvl;
                GameManager.Instance.data.passivesDictionary[Passives.hpRegen] = saveData.pasHPRegenLvl;
                GameManager.Instance.data.passivesDictionary[Passives.xpRange] = saveData.pasXpRangeLvl;

                GameManager.Instance.data.passivesDictionary[Passives.startWithUpgrade] = saveData.startWithUpgrade;

                GameManager.Instance.data.gold = saveData.gold;
                GameManager.Instance.data.sfxVolume = saveData.sfxVolume;
                GameManager.Instance.data.musicVolume = saveData.musicVolume;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                }
            }
        }
        else
        {
            Debug.LogError("No save file could be found!");
        }
    }
}
