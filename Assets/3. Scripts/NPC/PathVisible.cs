using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathVisible : MonoBehaviour
{
    void Start()
    {
        int count = transform.childCount;

        Transform temp;
        for(int i=0; i<count; i++)
        {
            temp = transform.GetChild(i);
            temp.GetComponent<Renderer>().enabled = false;
        }
    }
}
