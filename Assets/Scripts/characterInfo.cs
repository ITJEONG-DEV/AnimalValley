using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterInfo : MonoBehaviour
{
    public GameObject face1;
    public GameObject[] zoo;      

    public Material face;
    public static int animal = 0;
   
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    //animal + face + skin + headAcc + chestAcc
    public void Setanimal()
    {
        if(GameObject.FindGameObjectWithTag("bunny"))
        {
            animal = 0;
        }
        if (GameObject.FindGameObjectWithTag("cat"))
        {
            animal = 1;
        }
        if (GameObject.FindGameObjectWithTag("bear"))
        {
            animal = 2;
        }
        if (GameObject.FindGameObjectWithTag("dog"))
        {
            animal = 3;
        }
        if (GameObject.FindGameObjectWithTag("frog"))
        {
            animal = 4;
        }
        if (GameObject.FindGameObjectWithTag("monkey"))
        {
            animal = 5;
        }

        face=zoo[animal].transform.Find("Face01").transform.GetComponent<Renderer>().material;


        




    }
}
