using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct info_
{
    public static int chestAcc;
    public static int headAcc;
    public static int skin;
    public static int face;
    public static int animal = 0;

    public void setAniType(int n)
    {
        animal = n;
    }

    public void setFace(int face_)
    {
        face = face_;
    }

    public void setSkin(int skin_)
    {
        skin = skin_;
    }

    public void set_chestAccessories(int chestAcc_)
    {
        chestAcc = chestAcc_;
    }

    public void set_headAccessories(int headAcc_)
    {
        headAcc= headAcc_;
    }
    // 정보 읽는 함수들
    public int getAnimal()
    {
        return animal;
    }

    public int getFace()
    {
        return face;
    }
    public int getSkin()
    {
        return skin;
    }
    public int get_chestAcc()
    {
        return chestAcc;
    }

    public int get_headAcc()
    {
        return headAcc;
    }


}


//캐릭터 선택 확인 버튼 클릭시로 생각중...
public class characterInfo : MonoBehaviour
{
    public Material[] faces;
    public GameObject[] zoo;
    string name;
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
        if(GameObject.FindGameObjectWithTag("bunny").activeSelf)
        {
            info.setAniType(0);
            name = "Bunny";
        }
        else if (GameObject.FindGameObjectWithTag("cat").activeSelf)
        {
            info.setAniType(1);
            name = "Cat";
        }
        else if (GameObject.FindGameObjectWithTag("bear").activeSelf)
        {
            info.setAniType(2);
            name = "Bear";
        }
        else if (GameObject.FindGameObjectWithTag("dog").activeSelf)
        {
            info.setAniType(3);
            name = "Dog";
        }
        else if (GameObject.FindGameObjectWithTag("frog").activeSelf)
        {
            info.setAniType(4);
            name = "Frog";
        }
        else if (GameObject.FindGameObjectWithTag("monkey").activeSelf)
        {
            info.setAniType(5);
            name = "Monkey";
        }

        Debug.Log("동물이름: "+ name);

        
        int typeA = info.getAnimal();
        string faceName=zoo[typeA].transform.Find("Mesh").transform.Find("Face01").GetComponent<SkinnedMeshRenderer>().material.name;
      
        for (int i=1; i<29; i++)
        {
            Debug.Log(faceName.Substring(4, 2));
            if (int.Parse(faceName.Substring(4,2)) == i)
            {

                info.setFace(i);
            }

        }
        Debug.Log("얼굴 번호 : "+info.getFace());


        string _material = zoo[typeA].transform.Find("Mesh").transform.Find(name).GetComponent<SkinnedMeshRenderer>().material.name;

        for (int i = 0; i < 15; i++)
        {
            Debug.Log(_material.Substring(name.Length, 2));
            if (int.Parse(_material.Substring(name.Length, 2)) == i)
            {
                info.setSkin(i);
            }
        }
        Debug.Log("피부 번호 : " + info.getSkin());



        int num_h = 14;
        for(int i=0; i< num_h; i++)
        {
            if(zoo[typeA].transform.Find("Root_M").transform.Find("Spine1_M").transform.Find("Chest_M").transform.Find("Accessories_locator").transform.GetChild(i).gameObject.activeSelf==true)
            {
                Debug.Log(zoo[typeA].transform.Find("Root_M").transform.Find("Spine1_M").transform.Find("Chest_M").transform.Find("Accessories_locator").transform.GetChild(i).name);
                info.set_chestAccessories(i);
            }

        }
        Debug.Log("흉부 악세서리 번호 : " + info.get_chestAcc());


        int num_c = 13;
        for (int i = 0; i < num_c; i++)
        {
            if (zoo[typeA].transform.Find("Root_M").transform.Find("Spine1_M").transform.Find("Chest_M").transform.Find("Neck_M").transform.Find("Head_M").transform.Find("Head_Accessories_locator").transform.GetChild(i).gameObject.activeSelf==true)
            {
                info.set_headAccessories(i);
            }

        }

        Debug.Log("머리 악세서리 번호 : " + info.get_headAcc());

    }
}
