using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int _obstacleValue;
    [SerializeField] private GameObject _hitParticle;
    [SerializeField] private GameObject _hitBulletParticle;

    public int ObstacleValue => _obstacleValue;
    public event UnityAction<Obstacle> Offend;

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerMove.Instance.IsInvulnerble == false & other.gameObject.TryGetComponent(out PlayerModifier playerModifier))
        {
            Instantiate(_hitParticle, other.gameObject.transform.position, gameObject.transform.rotation);
            Offend?.Invoke(this);
            Destroy(gameObject);
        }
        if (other.gameObject.TryGetComponent(out WebBehaviour webBehaviour))
        {
            SoundsManager.Instance.PlaySound("WebHit");
            Instantiate(_hitBulletParticle, other.gameObject.transform.position, transform.rotation);
            Destroy(other.gameObject);
        }
    }
}
