using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public enum GateType { 
    Damage,
    LifeTime,
    FiringFrequency,
    SingleShootMode,
    DoubleShootMode,
    TripleShootMode
}

public class GateAppearaence : MonoBehaviour
{
    [Header("Images")]
    [SerializeField] Image _topImage;
    [SerializeField] Image _downImage;
    [SerializeField] Image _glassImage;

    [Header("Colors")]
    [SerializeField] Color _colorPositive;
    [SerializeField] Color _colorNegative;
    [SerializeField] Color _colorShootMode;


    [Header("Title Icons")]
    // Иконки увеличения/уменьшения ширины
    [SerializeField] GameObject _expandLable;
    [SerializeField] GameObject _shrinkLable;
    // Иконки увеличения/уменьшения высоты
    [SerializeField] GameObject _upLable;
    [SerializeField] GameObject _downLable;

    [Header("Sprites")]
    // Изображения типа стрельбы
    [SerializeField] Sprite _singleShootSprite;
    [SerializeField] Sprite _doubleShootSprite;
    [SerializeField] Sprite _tripleShootSprite;

    [Header("Texts")]
    [SerializeField] TextMeshProUGUI _downText;
    [SerializeField] TextMeshProUGUI _topText;

    private static string DamageText = "Урон";
    private static string LifeTimeText = "Дальность выстрела";
    private static string FiringFrequencyText = "Частота выстрела";
    private static string SingleShootModeText = "Одинарный выстрел";
    private static string DoubleShootModeText = "Двойной выстрел";
    private static string TripleShootModeText = "Тройной выстрел";
    private string _language;

    private void Start()
    {
        _language = YandexGame.EnvironmentData.language;

        switch (_language)
        {
            case "ru":
                DamageText = "Урон";
                LifeTimeText = "Дальность выстрела";
                FiringFrequencyText = "Частота выстрела";
                SingleShootModeText = "Одинарный выстрел";
                DoubleShootModeText = "Двойной выстрел";
                TripleShootModeText = "Тройной выстрел";
                break;
            case "en":
                DamageText = "Damage";
                LifeTimeText = "Shot range";
                FiringFrequencyText = "Fire rate";
                SingleShootModeText = "Single shot";
                DoubleShootModeText = "Double shot";
                TripleShootModeText = "Triple shot";
                break;
            case "tr":
                DamageText = "Zarar";
                LifeTimeText = "Atış aralığı";
                FiringFrequencyText = "Atış sıklığı";
                SingleShootModeText = "Tek atış";
                DoubleShootModeText = "Çift vuruş";
                TripleShootModeText = "Üçlü atış";
                break;
        }
    }


    public void UpdateVisual(GateType deformationType, float value)
    {
        string prefix = "";

        if (value == 0) 
        {
            SetColor(Color.grey);
        } 
        else if (value > 0)
        {
            prefix = "+";
            //SetColor(_colorPositive);
        }
        else
        {
            SetColor(_colorNegative);
        }

        _topText.text = deformationType switch
        {
            GateType.Damage => DamageText,
            GateType.LifeTime => LifeTimeText,
            GateType.FiringFrequency => FiringFrequencyText,
            GateType.SingleShootMode => SingleShootModeText,
            GateType.DoubleShootMode => DoubleShootModeText,
            GateType.TripleShootMode => TripleShootModeText,
            _ => string.Empty,
        };

        _downText.text = deformationType switch
        {
            GateType.FiringFrequency => prefix + value.ToString() + "/c",
            GateType.SingleShootMode => string.Empty,
            GateType.DoubleShootMode => string.Empty,
            GateType.TripleShootMode => string.Empty,
            _ => prefix + value.ToString()
        };

        _downImage.sprite = deformationType switch
        {
            GateType.SingleShootMode => _singleShootSprite,
            GateType.DoubleShootMode => _doubleShootSprite,
            GateType.TripleShootMode => _tripleShootSprite,
            _ => null
        };

        if(GateType.SingleShootMode == deformationType || GateType.DoubleShootMode == deformationType)
        {
            SetColorShootMode(_colorShootMode);
        }

        if (GateType.Damage == deformationType || GateType.FiringFrequency == deformationType || GateType.LifeTime == deformationType)
        {
            SetColor(_colorPositive);
        }
    }

    void SetColor(Color color) {
        _topImage.color = color;
        color.a = 0.5f;
        _downImage.color = color;
        _glassImage.color = new Color(color.r, color.g, color.b, 0.5f);
    }

    void SetColorShootMode(Color color)
    {
            _downImage.color = color;
    }
}


