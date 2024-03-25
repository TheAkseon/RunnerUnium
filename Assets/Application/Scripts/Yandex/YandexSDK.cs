using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class YandexSDK : MonoBehaviour
{
    [SerializeField] private Localization _localization;

    private LevelLoader _levelLoader;
    private const string _saveKey = "SaveData";
    private string _language;
    public bool IsAdRunning;

    public static YandexSDK Instance;

    public string CurrentLanguage => _language;

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

    private void Start()
    {
        if (YandexGame.SDKEnabled)
        {
            GetLoad();
            _levelLoader = FindObjectOfType<LevelLoader>();
            _language = YandexGame.EnvironmentData.language;
            _localization.SetLanguage(_language);
            
            LevelLoader.Instance.LoadLevel(SaveData.Instance.Data.CurrentLevel, GameReady);
        }
    }

    private void OnEnable()
    {
        YandexGame.GetDataEvent += GetLoad;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= GetLoad;
    }

    private void OnInBackgroundChange(bool inBackground)
    {
        if (!IsAdRunning)
            MuteAudio(inBackground);
    }

    private void MuteAudio(bool value)
    {
        Time.timeScale = value ? 0f : 1f;
        AudioListener.pause = value;
        AudioListener.volume = value ? 0f : 1f;
        SoundsManager.Instance.Mute("music", value);
        SoundsManager.Instance.Mute("effects", value);
    }

    private void GetLoad()
    {
        SaveData.Instance.Data.Coins = YandexGame.savesData.Coins;
        SaveData.Instance.Data.CurrentLevel = YandexGame.savesData.CurrentLevel;
        SaveData.Instance.Data.FakeLevel = YandexGame.savesData.FakeLevel;
        SaveData.Instance.Data.muteMusic = YandexGame.savesData.muteMusic;
        SaveData.Instance.Data.muteEffects = YandexGame.savesData.muteEffects;
        SaveData.Instance.Data.CostOfDamageImprovements = YandexGame.savesData.CostOfDamageImprovements;
        SaveData.Instance.Data.CostOfFiringRateImprovements = YandexGame.savesData.CostOfFiringRateImprovements;
        SaveData.Instance.Data.BaseDamage = YandexGame.savesData.BaseDamage;
        SaveData.Instance.Data.BaseFiringRate = YandexGame.savesData.BaseFiringRate;

        SaveManager.Save(_saveKey, SaveData.Instance.Data);
    }

    private void GameReady()
    {
        YandexGame.GameReadyAPI();
    }
}