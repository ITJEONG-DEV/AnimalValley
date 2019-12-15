using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene("Middle");
    }
    public void OnClickLoad()
    {
        SceneManager.LoadScene("Middle");
    }
    public void OnClickExit()
    {
        Application.Quit();
    }
}
