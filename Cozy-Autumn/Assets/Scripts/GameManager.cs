using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }

    public UIManager UIManager;
    public CursorManager cursorManager;
    public PlayerController player;
    public UpgradeSystem UpgradeSystem;
    public DataContainer data;
    public LeaderboardsManager leaderboard;
    public SaveManager saveManager;
    public ObjectPool objectPool;
}
