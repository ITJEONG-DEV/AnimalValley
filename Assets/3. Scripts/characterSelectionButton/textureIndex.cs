using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textureIndex : MonoBehaviour
{
    public GameObject[] animals;
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
            animals[0].GetComponent<ExampleSwapMaterials>().onclickEvent();
        else if (GameObject.FindGameObjectWithTag("cat"))
            animals[1].GetComponent<ExampleSwapMaterials>().onclickEvent();
        else if (GameObject.FindGameObjectWithTag("bear") == true)
            animals[2].GetComponent<ExampleSwapMaterials>().onclickEvent();



        //indexTexture++;

    }
}
