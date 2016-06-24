using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScene;
    public Image loadingBar;
    public Text labelLoading;

    public void LoadLevel(int idScene)
    {
        StartCoroutine(LevelCoroutine(idScene));
    }

    IEnumerator LevelCoroutine(int idScene)
    {
        loadingScene.SetActive(true);
        AsyncOperation async = SceneManager.LoadSceneAsync(idScene);

        while (!async.isDone)
        {
            loadingBar.fillAmount = async.progress / 0.9f;
            labelLoading.text = System.Math.Round((async.progress / 0.9f) * 100, 2) + " % ";
            yield return null;
        }
    }
}
