using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class UIManager : MonoBehaviour
{
    public Image xpBar;
    public TMP_Text levelTxt;

    public GameObject UpgradePanel;
    public UpgradePanel[] panels;

    public Camera myCamera;

    public GameObject lifeBarAll;
    public Image lifeBar;

    public TMP_Text killCount;

    public TMP_Text goldCount;
    float gold = 0;

    public TMP_Text timer;

    public GameObject leaderboardPanel;
    public GameObject leaderboardInputField;
    public GameObject leaderboardSubmitButton;

    public LeaderboardsManager leaderboards;

    public Scoreboard scoreboard;

    public GameObject pauseMenu;
    public Slider music;
    public Slider sfx;
    public AudioSource musicSource;
    private void Awake()
    {
        GameManager.Instance.UIManager = this;
    }

    bool isUpdating = true;
    bool isPause = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPause)
                OpenPauseMenu();
            else
                ClosePauseMenu();
        }
        if (isUpdating && !isPause)
            UpdateManager.Instance.OnUpdate();
    }

    public void UpdateExpAmmount(float current, float max)
    {
        xpBar.fillAmount = current / max;
    }

    public void UpdateLevel(int level)
    {
        levelTxt.text = "Level " + level;
    }

    public void UpdateLifeBarPosition(Vector3 pos)
    {
        lifeBarAll.transform.position = myCamera.WorldToScreenPoint(pos);
    }

    public void UpdateLife(float current, float max)
    {
        lifeBar.fillAmount = current / max;
    }

    public UpgradeSystem upgradeSystem;

    int cant = 3;
    public void GetPowerUps()
    {
        List<PowerUp> a = upgradeSystem.GetUpgrade(cant);

        if (a.Count <= 0)
        {
            GameManager.Instance.player.AddGold( 100 );
            TurnOffUpgradePanel();
            return;
        }
        for (int i = 0; i < cant; i++)
        {
            if (i < a.Count)
            {
                panels[i].SetPanel(a[i]);
            }
            else
            {
                panels[i].SetPanel(null);
            }
        }
    }

    public void UpdateTimer(int minutes, int seconds)
    {
        timer.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    Action CheckIfExtraXp;

    public void TurnOnUpgradePanel(Action checkerXp)
    {
        isUpdating = false;
        UpgradePanel.SetActive(true);
        GetPowerUps();
        CheckIfExtraXp = checkerXp;
    }

    public void TurnOffUpgradePanel()
    {
        isUpdating = true;
        UpgradePanel.SetActive(false);

        if (CheckIfExtraXp != null) CheckIfExtraXp();
    }

    public void UpdateKill(int k)
    {
        killCount.text = k.ToString();
    }

    public void AddGold(float g)
    {
        gold += g;
        goldCount.text = gold.ToString();
    }

    public void SetStats(Vector2Int time, int kills, int bosses, int timeScore, int killScore, int bossScore)
    {
        scoreboard.gameObject.SetActive(true);
        scoreboard.SetCounter(time, kills, bosses);
        scoreboard.SetScores(timeScore, killScore, bossScore);

        leaderboards.CheckIfIsHighScore(GameManager.Instance.player.TotalScore, leaderboardSubmitButton, leaderboardInputField);
    }
    public void OpenLeaderboards(bool active)
    {
        leaderboardPanel.gameObject.SetActive(active);
    }

    public void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
        isPause = true;
        music.value = AudioManager.instance.music.volume * 10;
        sfx.value = AudioManager.instance.sfxMultiplier;
    }
    public void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
        isPause = false;
    }
    public void SetMusicVolume(float val)
    {
        AudioManager.instance.SetMusicVolume(0.1f * val);
    }
    public void SetSfxVolume(float val)
    {
        AudioManager.instance.SetSfxVolume(val);
    }
}
