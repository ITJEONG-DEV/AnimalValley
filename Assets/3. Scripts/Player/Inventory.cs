using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    static GameObject Inven_Select = null;
    static Vector3 Inven_Select_Pos;
    public GameObject MenuInvenIconPrefab; //메뉴에서 아이템 아이콘 띄우는 프리팹
    public GameObject MenuInvenNumberPrefab; //메뉴에서 아이템 개수 띄우는 프리팹
    public GameObject[] Menu; //메뉴 불러오기 위한 변수
    public GameObject Menu_Selectbox;
    public GameObject Menutext;
    public GameObject Myname;
    public GameObject Villagename;
    public GameObject 소지금액;

    Vector3 Menu_Selectbox_Pos;

    private int MenuIndex;
    public bool MenuOn;


    public GameObject[] SemiInven_Icon; //인벤토리에서 아이템 아이콘 저장하는 배열
    public GameObject[] SemiInven_Number; //인벤토리에서 아이템 개수 저장하는 배열

    GameObject[] Inven_Icon = new GameObject[30];  //메뉴에서 아이템 아이콘 저장하는 배열
    GameObject[] Inven_Number = new GameObject[30]; //메뉴에서 아이템 개수 저장하는 배열

    // Start is called before the first frame update
    void Start()
    {
        MenuIndex = 0;
        MenuOn = false;
        for(int i = 0; i < 6; i++)
        {
            Menu[i].SetActive(false);
        }
        Menu_Selectbox.SetActive(false);

        // initial
        SemiInven_Icon = new GameObject[10];
        SemiInven_Number = new GameObject[10];
    }

    // Update is called once per frame
    void Update()
    {
        Inventory_Select();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            KeyDown_ESC();
        }
        Menu_Select();
    }

    public bool MenuStatus{
        get
        {
            return MenuOn;
        }
    }

    public void ShowMenu()
    {
        MenuOn = true;
        Menu[0].SetActive(true);
        Menu_Selectbox.SetActive(true);
        Myname.SetActive(true);
        ShowInfoName();
        ShowInfoVillageName();
        ShowInfo소지금();
    }

    public void CloseMenu()
    {
        MenuOn = false;
        for(int i = 0; i < 6; i++)
        {
            Menu[i].SetActive(false);
        }
        Menu_Selectbox.SetActive(false);
        Menutext.SetActive(false);
    }

    // renew semi inventory
    public void RenewSemiInventory(List<Item> itemList)
    {
        RemoveSemiInventory();
        ShowSemiInventory(itemList);
    }
    // 일반 화면 하단의 세미-인벤토리에 아이템 띄우기
    public void ShowSemiInventory(List<Item> itemList)
    {
        string Number = "";
        for (int i = 0; i < itemList.Count; i++)
        {
            // Debug.Log(itemList[i].ItemCode);
            SemiInven_Icon[i].GetComponent<Image>().sprite = Resources.Load(itemList[i].ItemCode, typeof(Sprite)) as Sprite;
            Number = itemList[i].Count.ToString();
            if(Number.Equals(0))
            {
                Number = "";
            }
            SemiInven_Number[i].GetComponent<Text>().text = Number;
        }
    }
    // 일반 화면 하단의 세미-인벤토리 아이템 지우기
    public void RemoveSemiInventory()
    {
        string Number = "";
        for (int i = 0; i < 10; i++)
        {
            if(SemiInven_Icon[i] != null)
                SemiInven_Icon[i].GetComponent<Image>().sprite = Resources.Load("0", typeof(Sprite)) as Sprite;

            if(SemiInven_Number[i] != null)
                SemiInven_Number[i].GetComponent<Text>().text = Number;
        }
    }

    public void RenewInventory(List<Item> itemList)
    {
        RemoveInventoy();
        ShowInventory(itemList);
    }
    // 메뉴의 인벤토리 창에서 아이템 띄우기 : ESC로 메뉴 탭 열었을 때만 실행해야 함...
    public void ShowInventory(List<Item> itemList)
    {
        Vector3 IconPos = GameObject.Find("Menu_Item_Icon").transform.position;
        Vector3 NumberPos = GameObject.Find("Menu_Item_Number").transform.position;
        string Number = "";

        for (int j = 0; j < 30; j++)
        {
            if (j / 10 == 0) //1~10칸
            {
                Inven_Icon[j] = Instantiate(MenuInvenIconPrefab, new Vector3(IconPos.x + j * 86.4f - 960f, IconPos.y - 540f, IconPos.z), MenuInvenIconPrefab.transform.rotation);
                Inven_Icon[j].transform.SetParent(GameObject.Find("MenuInventory").transform, false);

                Inven_Number[j] = Instantiate(MenuInvenNumberPrefab, new Vector3(NumberPos.x + j * 86.4f - 960f, NumberPos.y - 540f, NumberPos.z), MenuInvenNumberPrefab.transform.rotation);
                Inven_Number[j].transform.SetParent(GameObject.Find("MenuInventory").transform, false);
            }
            else if (j / 10 == 1) //11~20칸
            {

                Inven_Icon[j] = Instantiate(MenuInvenIconPrefab, new Vector3(IconPos.x + (j-10) * 86.4f - 960f, IconPos.y - 540f - 86.4f, IconPos.z), MenuInvenIconPrefab.transform.rotation);
                Inven_Icon[j].transform.SetParent(GameObject.Find("MenuInventory").transform, false);

                Inven_Number[j] = Instantiate(MenuInvenNumberPrefab, new Vector3(NumberPos.x + (j-10) * 86.4f - 960f, NumberPos.y - 540f - 86.4f, NumberPos.z), MenuInvenNumberPrefab.transform.rotation);
                Inven_Number[j].transform.SetParent(GameObject.Find("MenuInventory").transform, false);
            }
            else //21~30칸
            {
                Inven_Icon[j] = Instantiate(MenuInvenIconPrefab, new Vector3(IconPos.x + (j-20) * 86.4f - 960f, IconPos.y - 540f - 172.8f, IconPos.z), MenuInvenIconPrefab.transform.rotation);
                Inven_Icon[j].transform.SetParent(GameObject.Find("MenuInventory").transform, false);

                Inven_Number[j] = Instantiate(MenuInvenNumberPrefab, new Vector3(NumberPos.x + (j-20) * 86.4f - 960f, NumberPos.y - 540f - 172.8f, NumberPos.z), MenuInvenNumberPrefab.transform.rotation);
                Inven_Number[j].transform.SetParent(GameObject.Find("MenuInventory").transform, false);
            }
        }

        for(int i = 0; i < itemList.Count; i++)
        {
            Number = itemList[i].Count.ToString();
            if (Number.Equals(0))
            {
                Number = "";
            }
            Debug.Log(itemList[i].ItemCode);
            Inven_Icon[i].GetComponent<Image>().sprite = Resources.Load(itemList[i].ItemCode, typeof(Sprite)) as Sprite;
            Inven_Number[i].GetComponent<Text>().text = Number;
        }
    }

    // 메뉴의 인벤토리 창의 아이템 지우기
    public void RemoveInventoy()
    {
        string Number = "";
        for(int i = 0; i < 30; i++)
        {
            Inven_Icon[i].GetComponent<Image>().sprite = Resources.Load("0", typeof(Sprite)) as Sprite;
            Inven_Number[i].GetComponent<Text>().text = Number;
        }
        Debug.Log("삭제");
    }

    //숫자 키를 눌러 인벤토리 1칸~0칸까지 선택
    public static void Inventory_Select()
    {
        Inven_Select = GameObject.Find("Inventory_select");
        Inven_Select_Pos = Inven_Select.transform.position;
        switch (Input.inputString)
        {
            case "1":
                KeyDown_1();
                break;
            case "2":
                KeyDown_2();
                break;
            case "3":
                KeyDown_3();
                break;
            case "4":
                KeyDown_4();
                break;
            case "5":
                KeyDown_5();
                break;
            case "6":
                KeyDown_6();
                break;
            case "7":
                KeyDown_7();
                break;
            case "8":
                KeyDown_8();
                break;
            case "9":
                KeyDown_9();
                break;
            case "0":
                KeyDown_0();
                break;
            default:
                break;
        }
    }
    public static void KeyDown_1()
    {
        Inven_Select_Pos.x = -4.6f;
        Inven_Select.transform.position = Inven_Select_Pos;
    }
    public static void KeyDown_2()
    {
        Inven_Select_Pos.x = -3.58f;
        Inven_Select.transform.position = Inven_Select_Pos;
    }
    public static void KeyDown_3()
    {
        Inven_Select_Pos.x = -2.56f;
        Inven_Select.transform.position = Inven_Select_Pos;
    }
    public static void KeyDown_4()
    {
        Inven_Select_Pos.x = -1.54f;
        Inven_Select.transform.position = Inven_Select_Pos;
    }
    public static void KeyDown_5()
    {
        Inven_Select_Pos.x = -0.52f;
        Inven_Select.transform.position = Inven_Select_Pos;
    }
    public static void KeyDown_6()
    {
        Inven_Select_Pos.x = 0.52f;
        Inven_Select.transform.position = Inven_Select_Pos;
    }
    public static void KeyDown_7()
    {
        Inven_Select_Pos.x = 1.54f;
        Inven_Select.transform.position = Inven_Select_Pos;
    }
    public static void KeyDown_8()
    {
        Inven_Select_Pos.x = 2.56f;
        Inven_Select.transform.position = Inven_Select_Pos;
    }
    public static void KeyDown_9()
    {
        Inven_Select_Pos.x = 3.58f;
        Inven_Select.transform.position = Inven_Select_Pos;
    }
    public static void KeyDown_0()
    {
        Inven_Select_Pos.x = 4.6f;
        Inven_Select.transform.position = Inven_Select_Pos;
    }

    public void Menu_Selectbox_Move()
    {
        Menu_Selectbox_Pos = Menu_Selectbox.transform.position;
        switch(MenuIndex)
        {
            case 0: //menu_info
                RemoveInventoy();
                Menu_Selectbox_Pos.y = 857f;
                Menu_Selectbox.transform.position = Menu_Selectbox_Pos;
                ShowInfoName();
                ShowInfoVillageName();
                ShowInfo소지금();
                Debug.Log("인포");
                break;
            case 1: //menu_item
                ShowInventory(GameManager.GetItemList());
                Menu_Selectbox_Pos.y = 780f;
                Menu_Selectbox.transform.position = Menu_Selectbox_Pos;
                Debug.Log("아이템");
                break;
            case 2: //menu_npc
                RemoveInventoy();
                Menu_Selectbox_Pos.y = 703f;
                Menu_Selectbox.transform.position = Menu_Selectbox_Pos;
                Debug.Log("npc");

                break;
            case 3: //menu_making
                Menu_Selectbox_Pos.y = 626f;
                Menu_Selectbox.transform.position = Menu_Selectbox_Pos;
                Debug.Log("make");

                break;
            case 4: //menu_skill
                Menu_Selectbox_Pos.y = 549f;
                Menu_Selectbox.transform.position = Menu_Selectbox_Pos;
                Debug.Log("skill");

                break;
            case 5: //menu_book
                Menu_Selectbox_Pos.y = 472f;
                Menu_Selectbox.transform.position = Menu_Selectbox_Pos;
                Debug.Log("book");

                break;
            default:
                break;
        }
    }

    public void Menu_Select()
    {
        if (MenuOn)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Debug.Log("다운");
                if (MenuIndex < 5)
                {
                    Menu[MenuIndex].SetActive(false);
                    MenuIndex++;
                    Menu[MenuIndex].SetActive(true);
                    Menu_Selectbox_Move();
                }
                else
                {
                    MenuIndex = 5;
                }
                Debug.Log(MenuIndex);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Debug.Log("업");
                if (MenuIndex > 0)
                {
                    Menu[MenuIndex].SetActive(false);
                    MenuIndex--;
                    Menu[MenuIndex].SetActive(true);
                    Menu_Selectbox_Move();
                }
                Debug.Log(MenuIndex);
            }
        }
    }

    public void KeyDown_ESC()
    {
        if(MenuOn)
        {
            CloseMenu();
            //RemoveInventoy();
        }
        else
        {
            ShowMenu();
        }
    }

    public void ShowInfoName()
    {
        Myname.SetActive(true);
        Myname.GetComponent<Text>().text = GameManager.Name;
    }

    public void ShowInfoVillageName()
    {
        Villagename.SetActive(true);
        Villagename.GetComponent<Text>().text = GameManager.VillageName;
    }

    public void ShowInfo소지금()
    {
        소지금액.SetActive(true);
        소지금액.GetComponent<Text>().text = Gold.GOLD.ToString();
    }
}
