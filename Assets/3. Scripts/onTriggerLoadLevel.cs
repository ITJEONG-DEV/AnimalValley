using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class onTriggerLoadLevel : MonoBehaviour
{
  
    public string levelToLoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(LoadScene("Town1"));
        }
                //SceneManager.LoadScene("Town1", LoadSceneMode.Additive);    
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
