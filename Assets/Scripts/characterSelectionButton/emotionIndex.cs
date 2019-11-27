using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emotionIndex : MonoBehaviour
{
    public GameObject[] faces;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    public void onClick()
    {
        if (GameObject.FindGameObjectWithTag("bunny"))
            faces[0].GetComponent<ExampleSwapMaterials>().onclickEvent();
        else if (GameObject.FindGameObjectWithTag("cat"))
            faces[1].GetComponent<ExampleSwapMaterials>().onclickEvent();
        else if (GameObject.FindGameObjectWithTag("bear") == true)
            faces[2].GetComponent<ExampleSwapMaterials>().onclickEvent();
    }
}
