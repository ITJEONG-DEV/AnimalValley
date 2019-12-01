using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Vector3 pos_= new Vector3(0,0,0);       // 소환할 위치 지정용 뵨수
    //public GameObject[] zoo;
    //public GameObject[] headAcc;
    //public GameObject[] chestAcc;


    public float speed = 3f;
    public float runWeight = 2f;

    float walkingTime = 0.0f;
    bool isWalk = false;
    bool isRun = false;
    bool left = false;
    bool right = false;
    bool forward = false;
    bool back = false;

    void Start()
    {
    }

    void FixedUpdate()
    {
        Move();

        if (isWalk)
        {
            walkingTime += Time.fixedDeltaTime;

            if (walkingTime >= 1.5f) isRun = true;

            if (forward || back)
                GetComponent<Animator>().SetInteger("animation", 15);
            else if (right)
                GetComponent<Animator>().SetInteger("animation", 16);
            else if (left)
                GetComponent<Animator>().SetInteger("animation", 17);
        }
        else
        {
            isRun = false;
            walkingTime = 0.0f;
            GetComponent<Animator>().SetInteger("animation", 1);
        }
    }

    void Move()
    {

        // real : right, key : down
        if (Input.GetKeyDown(KeyCode.S)) right = true;
        else if (Input.GetKeyUp(KeyCode.S)) right = false;

        // real : left, key : up
        if (Input.GetKeyDown(KeyCode.W)) left = true;
        else if (Input.GetKeyUp(KeyCode.W)) left = false;

        // real : forward, key : right
        if (Input.GetKeyDown(KeyCode.D)) forward = true;
        else if (Input.GetKeyUp(KeyCode.D)) forward = false;

        // real : back, key : back
        if (Input.GetKeyDown(KeyCode.A)) back = true;
        else if (Input.GetKeyUp(KeyCode.A)) back = false;

        isWalk = false;

        if (left && !right)
        {
            isWalk = true;
            transform.Translate(Vector3.left * speed * Time.fixedDeltaTime * (isRun ? runWeight : 1));
        }
        else if (right && !left)
        {
            isWalk = true;
            transform.Translate(Vector3.right * speed * Time.fixedDeltaTime * (isRun ? runWeight : 1));
        }
        else if (left && right)
        {
            left = false;
            right = false;
        }

        if (forward && !back)
        {
            isWalk = true;
            transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime * (isRun ? runWeight : 1));
        }
        else if (back && !forward)
        {
            isWalk = true;
            transform.Translate(Vector3.back * speed * Time.fixedDeltaTime * (isRun ? runWeight : 1));
        }
        else if (forward && back)
        {
            forward = false;
            back = false;
        }
    }

    void setMe(info_ info) 
    {
        string realNum = null;
        int num = info.getSkin();
        if(num<10)
        {
            realNum = "0" + num.ToString();
        }
        GameObject temp = GameObject.Find(info.getAnimal() + realNum);
        Instantiate(temp, pos_, Quaternion.identity);                   //스킨이랑 같이 불렀다.

        temp.gameObject.transform.Find("Mesh").gameObject.AddComponent<ExampleSwapMaterials>();
       
        for(int i=1; i<=28; i++)
        {
            temp.gameObject.transform.Find("Mesh").transform.Find("Face01").GetComponent<ExampleSwapMaterials>().SendMessage("onclickEvent");
            if (i== info.getFace())
            {
                break;
            }
        }

        for(int i=0; i<14; i++)           // 위의 public  쓰지않는방법 일단 시도
        {
            if(info.get_chestAcc()==i )
            {
                if(i==0)   //아무것도 없는것
                {
                    break;
                }
                else
                {
                    GameObject c = GameObject.Find("c_"+i.ToString());  //이걸로 project panel에 있는 오브젝트 찾을 수 있나..? 
                    c.transform.parent = temp.transform.Find("Root_M").transform.Find("Spine1_M").transform.Find("Chest_M").transform.Find("Accessories_locator").transform;
                    c.gameObject.SetActive(true);
                }
             
            }
        }

        for (int i = 0; i < 14; i++)           // 위의 public  쓰지않는방법 일단 시도
        {
            if (info.get_headAcc() == i)
            {
                if (i == 0)   //아무것도 없는것
                {
                    break;
                }
                else
                {
                    GameObject h = GameObject.Find("h_" + i.ToString());  //이걸로 project panel에 있는 오브젝트 찾을 수 있나..? 
                    h.transform.parent = temp.transform.Find("Root_M").transform.Find("Spine1_M").transform.Find("Neck_M").transform.Find("Head_M").transform.Find("Head_Accessories_locator").transform;
                    h.gameObject.SetActive(true);
                }

            }
        }


    }

}
