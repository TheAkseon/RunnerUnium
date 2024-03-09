using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance;
    public Slider slider;
    public TMP_Text progressText;

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

    public void LoadLevel (int sceneBuildIndex, Action onLoaded = null) 
    {
        StartCoroutine(LoadAsyncronously(sceneBuildIndex, onLoaded));
    }

    private IEnumerator LoadAsyncronously (int sceneBuildIndex, Action onLoaded) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneBuildIndex);

        while (operation.isDone == false)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            
            slider.value = progress;
            progressText.text = progress * 100f + "%";

            yield return null;
        }
        
        onLoaded?.Invoke();
    }

}
