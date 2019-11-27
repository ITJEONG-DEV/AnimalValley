using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct info_
{
    public static GameObject chestAcc;
    public static GameObject headAcc;
    public static Material skin;
    public static Material face;
    public static int animal = 0;

    public void setAnimal(int n)
    {
        animal = n;
    }

    public void setFace(Material face_)
    {
        face = face_;
    }

    public void setSkin(Material skin_)
    {
        skin = skin_;
    }

    public void set_chestAccessories(GameObject chestAcc_)
    {
        chestAcc = chestAcc_;
    }

    public void set_headAccessories(GameObject headAcc_)
    {
        headAcc= headAcc_;
    }
    // 정보 읽는 함수들
    public int getAnimal()
    {
        return animal;
    }

    public Material getFace()
    {
        return face;
    }
    public Material getSkin()
    {
        return skin;
    }
    public GameObject get_chestAcc()
    {
        return chestAcc;
    }

    public GameObject get_headAcc()
    {
        return headAcc;
    }


}


//캐릭터 선택 확인 버튼 클릭시로 생각중...
public class characterInfo : MonoBehaviour
{
    public GameObject face1;
    public GameObject[] zoo;

    //캐릭터 정보 저장된것들
    //public static GameObject chestAcc;
    //public static GameObject headAcc;
    //public static Material skin;
    //public static Material face;
    //public static int animal = 0;


    public info_ info = new info_();
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    //animal + face + skin + headAcc + chestAcc
    public void setAnimal()
    {
        if(GameObject.FindGameObjectWithTag("bunny"))
        {
            info.setAnimal(0);
        }
        if (GameObject.FindGameObjectWithTag("cat"))
        {
            info.setAnimal(1);
        }
        if (GameObject.FindGameObjectWithTag("bear"))
        {
            info.setAnimal(2);
        }
        if (GameObject.FindGameObjectWithTag("dog"))
        {
            info.setAnimal(3);
        }
        if (GameObject.FindGameObjectWithTag("frog"))
        {
            info.setAnimal(4);
        }
        if (GameObject.FindGameObjectWithTag("monkey"))
        {
            info.setAnimal(5);
        }

        int typeA = info.getAnimal();
        info.setFace(zoo[typeA].transform.Find("Face01").transform.GetComponent<Renderer>().material);

        info.setSkin(zoo[typeA].transform.Find("Mesh").transform.GetComponent<Renderer>().material);
        Debug.Log(info.getSkin().name);


        int num_h = zoo[typeA].transform.Find("Accessories_locator").transform.childCount;
        for(int i=0; i< num_h; i++)
        {
            if(zoo[typeA].transform.Find("Accessories_locator").transform.GetChild(i).gameObject.activeSelf)
            {
                info.set_headAccessories(zoo[typeA].transform.Find("Accessories_locator").transform.GetChild(i).gameObject);
            }

        }

        int num_c = zoo[typeA].transform.Find("Head_Accessories_locator").transform.childCount;
        for (int i = 0; i < num_c; i++)
        {
            if (zoo[typeA].transform.Find("Head_Accessories_locator").transform.GetChild(i).gameObject.activeSelf)
            {
                info.set_chestAccessories(zoo[typeA].transform.Find("Head_Accessories_locator").transform.GetChild(i).gameObject);
            }

        }

    }
}
