using System;
using UnityEngine;
using YG;

public class SaveData : MonoBehaviour
{
    public static SaveData Instance;

    [SerializeField] public DataHolder _data;

    public DataHolder Data => _data;

    private const string _leaderboardTxt = "Leaderboard";
    private const string _saveKey = "SaveData";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    /*private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            _data = new DataHolder();

            SaveManager.Reset(_saveKey, _data);
            SaveYandex();
        }
    }*/

    private void OnDisable()
    {
        SaveYandex();
        Save();
    }

    public void Save()
    {
        SaveManager.Save(_saveKey, _data);
    }

    public void Load()
    {
        var data = SaveManager.Load<DataHolder>(_saveKey);
        _data = data;
    }

    public void SaveYandex()
    {
        YandexGame.savesData.Coins = Data.Coins;
        YandexGame.savesData.CurrentLevel = Data.CurrentLevel;
        YandexGame.savesData.FakeLevel = Data.FakeLevel;
        YandexGame.savesData.muteMusic = Data.muteMusic;
        YandexGame.savesData.muteEffects = Data.muteEffects;
        YandexGame.savesData.CostOfDamageImprovements = Data.CostOfDamageImprovements;
        YandexGame.savesData.CostOfFiringRateImprovements = Data.CostOfFiringRateImprovements;
        YandexGame.savesData.BaseDamage = Data.BaseDamage;
        YandexGame.savesData.BaseFiringRate = Data.BaseFiringRate;

        YandexGame.SaveProgress();
    }
}

[Serializable]
public class DataHolder
{
    public int Coins = 0;
    public int CurrentLevel = 1;
    public int FakeLevel = 1;
    public bool muteMusic = false;
    public bool muteEffects = false;
    public int CostOfDamageImprovements = 10;
    public int CostOfFiringRateImprovements = 10;
    public int BaseDamage = 2;
    public float BaseFiringRate = 2;
}
