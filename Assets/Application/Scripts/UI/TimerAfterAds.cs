using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TimerAfterAds : MonoBehaviour
{
    [SerializeField] private float time = 4;
    [SerializeField] private TextMeshProUGUI timer_text;
    [SerializeField] private GameObject Text_menu;

    private float _timeLeft = 0f;
    private bool _timerOn = false;

    public void TimerStart()
    {
        _timeLeft = time;
        _timerOn = true;
        Text_menu.SetActive(true);
        StartCoroutine(Timer());
    }
    IEnumerator Timer()
    {
        while (_timerOn)
        {
            if (_timeLeft > 0)
            {
                Debug.Log("Timer:" + _timeLeft);
                timer_text.text = _timeLeft.ToString();
                _timeLeft -= 1;
            }
            else
            {
                PlayerMove.Instance.ResumeMovement();
                PlayerMove.Instance.ApplyInvulnerable();
                PlayerAnimationController.Instance.Run();
                _timerOn = false;
                _timeLeft = time;
                Text_menu.SetActive(false);
                StopCoroutine(Timer());
            }
            yield return new WaitForSeconds(1);
        }
    }
}
