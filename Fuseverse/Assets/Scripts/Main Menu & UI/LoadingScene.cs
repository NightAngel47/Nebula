using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    bool isLoadDelayDone = false;

    void Start()
    {
        StartCoroutine(LoadAsyc());
        StartCoroutine(LoadDelay());
    }

    IEnumerator LoadAsyc()
    {
        AsyncOperation asyncLoad = null;
        asyncLoad = SceneManager.LoadSceneAsync(GameManager.SceneToLoad);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone && isLoadDelayDone)
        {
            yield return null;
        }
    }
    IEnumerator LoadDelay()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        isLoadDelayDone = true;
    }
}
