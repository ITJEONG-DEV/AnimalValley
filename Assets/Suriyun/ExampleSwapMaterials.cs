using UnityEngine;
using System.Collections;

// Change renderer's material each changeInterval
// seconds from the material array defined in the inspector.
public class ExampleSwapMaterials : MonoBehaviour
{
    public static int index_T;
    public Material[] materials;
    public float changeInterval = 0.33F;
    public Renderer rend;

    void Start()
    {
        rend = this.gameObject.AddComponent<Renderer>();
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        index_T = 0;
    }

    void Update()
    {
       
    }

    public void onclickEvent()
    {
        if (materials.Length == 0)
            return;
        Debug.Log(index_T);
        index_T++;
        // we want this material index now
        // take a modulo with materials count so that animation repeats
        index_T = index_T % materials.Length;
       
        // assign it to the renderer
        rend.sharedMaterial = materials[index_T];


        


    }

<<<<<<< HEAD
    public void onclickEvent()
    {

    }
=======
  
>>>>>>> dev
}