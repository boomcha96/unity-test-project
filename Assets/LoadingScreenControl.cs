using System.Collections;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenControl : MonoBehaviour
{

    public GameObject loadingScreenObj;
    public Slider slider;

    AsyncOperation async;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadingScreen(sceneName));
    }

    IEnumerator LoadingScreen(string sceneName)
    {
        loadingScreenObj.SetActive(true);
        async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            slider.value = async.progress;
            if (async.progress >= 0.9f)
            {
                slider.value = 1f;
                async.allowSceneActivation = true;
            }
            yield return null;
        }

    }    
}
