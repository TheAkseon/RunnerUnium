using UnityEngine;

public class PlayerModifier : MonoBehaviour
{
    public static PlayerModifier Instance;

    [SerializeField] private Transform _colliderTransform;
    [SerializeField] private Transform _playerModel;

    //[SerializeField] private AudioSource _increaseSound;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void Die()
    {
        UIBehaviour.Instance.GameOver(false);
        gameObject.SetActive(false);
    }

    public void Reberth()
    {
        gameObject.SetActive(true);
    }
}
