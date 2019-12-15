using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiddleManager : MonoBehaviour
{
    bool isOk = false;
    void Start()
    {
    }

    private void Update()
    {
        if(GameManager.Ready && !isOk)
        {
            StartCoroutine(LoadScene(GameManager.currentSceneString));
            isOk = true;
        }
    }

    IEnumerator LoadScene(string sceneName)
    {

        AsyncOperation asyncOper = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncOper.isDone)
        {
            Debug.Log(asyncOper.progress);
            yield return null;

        }

    }
}
