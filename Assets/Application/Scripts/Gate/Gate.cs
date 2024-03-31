using System;
using UnityEngine;
using YG;

public class Gate : MonoBehaviour
{
    [SerializeField] private float _value;
    [SerializeField] private GateType _deformationType;
    [SerializeField] private GateAppearaence _gateAppearaence;
    [SerializeField] private GameObject _effectPrefab;
    [SerializeField] private Transform _particlePosition;
    private string _language = "ru";
    private string DamageText;
    private string LifeTimeText;
    private string FiringFrequencyText;
    private string SingleShootModeText;
    private string DoubleShootModeText;
    private string TripleShootModeText;

    private void Awake()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        _language = YandexGame.EnvironmentData.language;
#endif

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

    private void Start() => _gateAppearaence.UpdateVisual(_deformationType, _value, DamageText, LifeTimeText, FiringFrequencyText,
        SingleShootModeText, DoubleShootModeText, TripleShootModeText);

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerModifier>())
        {
            switch (_deformationType)
            {
                case GateType.Damage:
                        WebBullet.ChangeDamage(Convert.ToInt32(_value));
                    break;
                case GateType.LifeTime:
                    _value /= 2;    
                    WebBullet.ChangeLifeTime(_value);
                    break;
                case GateType.FiringFrequency:
                    other.GetComponent<WebShooting>().ChangeFiringRate(_value);
                    break;
                default:
                    other.GetComponent<WebShooting>().ChangeShootMode(_deformationType);
                    break;
            }
            Instantiate(_effectPrefab, _particlePosition.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
