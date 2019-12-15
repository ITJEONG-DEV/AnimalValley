using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cropManager : MonoBehaviour
{
    public string[,] seedItem;
    public string[,] seedString;
    public GameObject mud_prefab;
    public Material mud;
    public Material normalGround;
    public string area = null;
    public GameObject seed;
    public GameObject[] plants_lv2;  //2단계 작물 프리팹
    public GameObject[] plants_lv3;   //3단계 작물 프리팹
    public GameObject trunk;
    public List<GameObject>[,] crops_;
    int x;
    int y;

    GameObject temp;
    // Start is called before the first frame update
    void Start()
    {
        seedItem = new string[5,2];
        crops_ = new List<GameObject>[5,2];
    }

    // Update is called once per frame
    void Update()
    {
        //area = testMoving_YJ.groundName;
        //GameObject temp = GameObject.Find(area);
        
    }                                                                       

    public void plowGround(string _area)    //땅 갈기
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

    public int typeofSeed(int _x, int _y)
    {
        int i = 0;
        if(seedItem[_x,_y]=="RSG0" || seedItem[_x, _y] == "RSG1")
        {
            i = Random.Range(12,13);
        }
        else if(seedItem[_x,_y]=="RSV0")
        {
            i = Random.Range(8,9);
        }
        else if(seedItem[_x, _y] == "RSV1")
        {
            i = 2;     
        }  
        else if(seedItem[_x,_y]=="RSV2")
        {
            i = 1;
        }
        else if(seedItem[_x, _y] == "RSV3")
        {
            i = 0;
        }
        else if(seedItem[_x, _y] == "RSV4")
        {
            i = 7;
        }
        else if (seedItem[_x, _y] == "RSV8")
        {
            i = 11;
        }
        else if (seedItem[_x, _y] == "RSCR")
        {
            i = Random.Range(3,4);
        }
        else if (seedItem[_x, _y] == "RSCW")
        {
            i = 5;
        }
        else if (seedItem[_x, _y] == "RSCS")
        {
            i = 10;
        }


        return i;
        
    }

    public void choppingTree(Vector3 pos)
    {
        Vector3 randPos = new Vector3(Random.Range(-1,1),0, Random.Range(-1, 1));
        Instantiate(trunk,pos+ randPos, Quaternion.identity);
    }

    public void waterGround(string _area)    //물뿌리기
    {
        Debug.Log("물 줄 땅="+_area);
        if (GameObject.Find(_area).tag == "wetGround")
            GameObject.Find(_area).GetComponent<Renderer>().material = mud;
        else 
            return;
    }

    public void sowSeed(string[] Names)    //씨앗 뿌리기  + 씨앗 정보 저장
    {
        string _area= Names[0];
        string seedName = Names[1];
        Debug.Log("_area_내용="+_area);
        Debug.Log(seedName);
        GameObject SEED;
        x = int.Parse(_area.Substring(1, 1));
        y = int.Parse(_area.Substring(3, 1));
        Debug.Log("[" + x + "," + y + "]");
        if (GameObject.Find(_area).tag=="wetGround")
        {         
            seedItem[x, y] = seedName;
            Vector3 pos = new Vector3(Random.Range(-0.1f, 0.1f), 0.21f, Random.Range(-0.1f, 0.1f));
            SEED=Instantiate(seed, GameObject.Find(_area).transform.position + pos, Quaternion.identity) as GameObject;
            crops_[x, y].Add(SEED);
        }
        
    }

    public void harvestCrops(string _area)   //수확하기
    {
        x = int.Parse(_area.Substring(1,1));
        y = int.Parse(_area.Substring(3, 1));
        if (crops_[x,y]!=null)
        {
            for (int i = 0; i < crops_[x, y].Count; i++)
                Destroy(crops_[x,y][i]);
            Debug.Log("수확했다");
        }
    }
                                                  //매개인자 :  cropLevel, 땅 번호 
    public void growingCrops(int cropLevel,int pos_x, int pos_y)      //작물 상태 진화
    {
        string _area = "("+pos_x +","+pos_y+")";
        if (GameObject.Find(_area).tag == "wetGround")
        {
            Vector3 origin_pos = GameObject.Find("(" + pos_x + "," + pos_y + ")").transform.position;
            Vector3 pos = new Vector3(Random.Range(-0.25f, 0.25f), 0.21f, Random.Range(-0.25f, 0.25f));
            Debug.Log("grwongCrops 내에 있다");
            if (cropLevel == 1)
            {
                if (seedItem[pos_x, pos_y] != null)
                {
                    for (int i = 0; i < crops_[pos_x, pos_y].Count; i++)
                        Destroy(crops_[pos_x, pos_y][i]);            //일단 원래 있던것들 다 지우기

                }
                GameObject temp = Instantiate(seed, pos + origin_pos, Quaternion.identity);
                crops_[pos_x, pos_y].Add(temp);
            }
            else if (cropLevel == 2)
            {
                if (seedItem[pos_x, pos_y] != null)
                {
                    for (int i = 0; i < crops_[pos_x, pos_y].Count; i++)
                        Destroy(crops_[pos_x, pos_y][i]);            //일단 원래 있던것들 다 지우기
                }
                int type = Random.Range(0, 5);
                GameObject temp = Instantiate(plants_lv2[type], pos + origin_pos, Quaternion.identity);
                crops_[pos_x, pos_y].Add(temp);
            }
            else if (cropLevel == 3)
            {

                if (seedItem[pos_x, pos_y] != null)
                {
                    for (int i = 0; i < crops_[pos_x, pos_y].Count; i++)
                        Destroy(crops_[pos_x, pos_y][i]);
                }//일단 원래 있던것들 다 지우기
                Debug.Log("타입 알아보기 전");
                int type = typeofSeed(pos_x, pos_y);
                GameObject temp = Instantiate(plants_lv3[type], pos + origin_pos, Quaternion.identity);
                crops_[pos_x, pos_y].Add(temp);

            }
        }
    }


}
