using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance;

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

            yield return null;
        }
        
        onLoaded?.Invoke();
    }

}
