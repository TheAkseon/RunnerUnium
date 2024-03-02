using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public static Boss Instance;
    
    [SerializeField] private int _force;
    [SerializeField] private TextMeshProUGUI _countForceText;
    [SerializeField] private GameObject _hitEffectPrefab;

    private bool _isNeedDie = true;

    public event UnityAction<Boss> Fight;
    public event UnityAction<int> HealthChanged;
    public event UnityAction Die;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        int force = SaveData.Instance.Data.BaseDamage;
        int healthMultiplier = Random.Range(10, 15);
        int health = force * healthMultiplier;
        
        _force = 30 + health;
        _countForceText.text = _force.ToString();
    }

    public void TakeDamage(int damage)
    {
        if (FindObjectOfType<BossFight>()._isFight)
        {
            CameraShake();
            _force -= damage;

            if (_force <= 0)
            {
                if (_isNeedDie)
                {
                    _isNeedDie = false;
                    Die?.Invoke();
                }
                _force = 0;
            }

            GetComponent<HP_Animation>().SpawnCanvas(transform, damage);
            _countForceText.text = _force.ToString();

            HealthChanged?.Invoke(_force);
        }
    }

    private void CameraShake()
    {
        GetComponent<BossFight>()._bossFightCamera.GetComponent<Animator>().SetTrigger("Shake");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out WebBehaviour webBullet))
        {
            Instantiate(_hitEffectPrefab, other.gameObject.transform.position, transform.rotation);
            Destroy(other.gameObject);
        }

        if (other.gameObject.TryGetComponent(out PlayerModifier playerModifier))
        {
            Fight?.Invoke(this);
        }
    }

    public int GetForce() => _force;
}
