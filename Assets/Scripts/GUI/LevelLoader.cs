using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScene;
    public Image loadingBar;
    public Text labelLoading;

    public void LoadLevel()
    {
        StartCoroutine(LevelCoroutine());
    }

    IEnumerator LevelCoroutine()
    {
        loadingScene.SetActive(true);
        AsyncOperation async = SceneManager.LoadSceneAsync(1);

        while (!async.isDone)
        {
            Debug.Log(async.progress / 0.9f);
            loadingBar.fillAmount = async.progress / 0.9f;
            labelLoading.text = System.Math.Round((async.progress / 0.9f) * 100, 2) + " % ";
            yield return null;
        }
    }
}
