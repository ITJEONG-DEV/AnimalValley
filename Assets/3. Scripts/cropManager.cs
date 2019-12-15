using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cropManager : MonoBehaviour
{
    public string[,] seedItem;
    public GameObject mud_prefab;
    public Material mud;
    public Material normalGround;
    public string area = null;
    public GameObject seed;

    GameObject temp;
    // Start is called before the first frame update
    void Start()
    {
        seedItem = new string[5,2];
    }

    // Update is called once per frame
    void Update()
    {
        //area = testMoving_YJ.groundName;
        //GameObject temp = GameObject.Find(area);
        
    }                                                                       

    public void plowGround(string _area)    //땅
    {
        area = _area;
        Debug.Log("구현 할 땅 이름:"+_area);
        Vector3 originalPos;
        if (GameObject.Find(area).activeSelf==true)
        { 
            originalPos = GameObject.Find(area).transform.position;
        }
        else
        {
            return;
        }

        if (GameObject.Find(area).tag == "ground")
        {
            GameObject.Find(area).SetActive(false);

        }
        else
        {
            return;
        }

       
        temp=Instantiate(mud_prefab, originalPos, Quaternion.identity);
        temp.GetComponent<Renderer>().material= normalGround;
        temp.name =_area ;
   
    }

    public void waterGround(string _area)
    {
        Debug.Log("물 줄 땅="+_area);
        if (GameObject.Find(_area).tag == "wetGround")
            GameObject.Find(_area).GetComponent<Renderer>().material = mud;
        else 
            return;
    }

    public void sowSeed(string[] Names)
    {
        string _area= Names[0];
        string seedName = Names[1];
        Debug.Log("_area_내용="+_area);
        Debug.Log(seedName);

        int x = int.Parse(_area.Substring(1, 1));
        int y = int.Parse(_area.Substring(3, 1));
        Debug.Log("[" + x + "," + y + "]");
        if (GameObject.Find(_area).tag=="wetGround")
        {         
            seedItem[x, y] = seedName;
            Vector3 pos = new Vector3(Random.Range(-0.1f, 0.1f), 0.21f, Random.Range(-0.1f, 0.1f));
            Instantiate(seed, GameObject.Find(_area).transform.position + pos, Quaternion.identity);
        }
        
    }


}
