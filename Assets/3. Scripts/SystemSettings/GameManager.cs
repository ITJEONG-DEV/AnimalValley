using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static class GameTime
    {
        public static int day, hour, minute;

        public static string ToString()
        {
            return day.ToString("D2") + " " + hour.ToString("D2") + " " + minute.ToString("D2");
        }
    }

    private const string path = "Assets/3. Scripts/SystemSettings/CharacterSettings/data.data";
    private static List<Item> itemList = new List<Item>();
    private float time;

    public GameObject UI;
    public GameObject CHARACTER;

    public GameObject Timer;
    public GameObject Money;
    public Image hpbar;
    public Image energybar;
    public GameObject sun;
    public GameObject moon;


    private static string name;
    private static string valleyName;

    private Inventory inventory;
    private static bool isReady = false;

    private string characterAppearanceInfo;

    public static bool isStart = false;

    public static bool TALKING = false;

    public static string currentSceneString = "";
    public static Vector3 characterPosition;

    public static bool Ready
    {
        get
        {
            return isReady;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(UI);
        DontDestroyOnLoad(CHARACTER);
    }

    void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        // 게임 시작 시, 초기 설정(불러오기)
        InitialSettings();
        isReady = true;
        Timer.SetActive(true);
    }

    void Update()
    {
        // Debug.Log("isStart : " + isStart);
        if(currentSceneString.Equals(SceneManager.GetActiveScene()))
        {
            // character position

            currentSceneString = "";
            CHARACTER.transform.position = characterPosition;
        }

        if (isStart)
        {
            sun.SetActive(false);
            moon.SetActive(false);
            ShowGold();
            ShowHp();
            ShowEnergy();

            if (!inventory.MenuStatus || TALKING)
            {
                time += Time.deltaTime;

                if (time >= 10.0f)
                {
                    GameTime.minute += 5;
                    if (GameTime.minute >= 60)
                    {
                        GameTime.hour += 1;
                        GameTime.minute -= 60;

                        if (GameTime.hour > 24)
                        {
                            GameTime.hour -= 24;
                        }
                    }
                    time = 0f;

                    // show time
                    Debug.Log(GameTime.ToString());
                }

                if (GameTime.hour == 1)
                {
                    // faint()
                }

                if (GameTime.hour == 6)
                {

                }
            }
            else
            {
                Debug.Log("Stopped");
            }

            ShowTime();
            ShowSun();
        }


    }

    bool Load()
    {
        String[] tempStr;
        try
        {
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader streamReader = new StreamReader(fileStream);

            // time info
            tempStr = streamReader.ReadLine().Split(' ');
            TimeSettings(int.Parse(tempStr[0]), int.Parse(tempStr[1]), int.Parse(tempStr[2]));

            // story status info
            tempStr = streamReader.ReadLine().Split(' ');
            int storyStatusInfo = int.Parse(tempStr[0]);

            // gold info : gold
            tempStr = streamReader.ReadLine().Split(' ');
            Gold.GOLD = int.Parse(tempStr[0]);

            // status info : hp, energe
            tempStr = streamReader.ReadLine().Split(' ');
            Status.SetHp(int.Parse(tempStr[0]));
            Status.CUR_HP = int.Parse(tempStr[1]);

            Status.SetEnerge(int.Parse(tempStr[2]));
            Status.CUR_ENERGE = int.Parse(tempStr[3]);

            // item info
            tempStr = streamReader.ReadLine().Split(' ');
            for (int i = 0; i < (tempStr.Length / 2); i++)
            {
                AddItem(tempStr[i * 2], int.Parse(tempStr[i * 2 + 1]));
            }

            // character info : characterType, characterSkin, characterEmotion, accessory1(head), accessory2(body)
            characterAppearanceInfo = streamReader.ReadLine();

            // character location info
            tempStr = streamReader.ReadLine().Split(' ');
            currentSceneString = tempStr[0];
            characterPosition = new Vector3(float.Parse(tempStr[1]), float.Parse(tempStr[2]), float.Parse(tempStr[3]));

            // user info
            tempStr = streamReader.ReadLine().Split(' ');
            name = tempStr[0];

            // valley info
            valleyName = streamReader.ReadLine().Trim();

            // npc love
            tempStr = streamReader.ReadLine().Split(' ');

            // crop info

            streamReader.Close();
            fileStream.Close();
        }
        catch (Exception exception)
        {
            Debug.Log("FAILED: " + exception.Message);

            return false;
        }

        return true;
    }
    bool Save()
    {
        try
        {
            FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter streamWriter = new StreamWriter(fileStream);

            // time info
            streamWriter.WriteLine(GameTime.day + " " + GameTime.hour + " " + GameTime.minute);

            // story status info
            streamWriter.WriteLine();

            // gold info : gold
            streamWriter.WriteLine(Gold.GOLD);

            // status info : hp, energe
            streamWriter.WriteLine(Status.MAX_HP + " " + Status.CUR_HP + " " + Status.MAX_ENERGE + " " + Status.CUR_ENERGE);

            // item info
            string itemString = "";
            foreach (Item item in itemList)
            {
                itemString += item.ItemCode + " " + item.Count + " ";
            }
            streamWriter.WriteLine(itemString);

            // character info : characterType, characterSkin, characterEmotion, accessory1(head), accessory2(body)
            streamWriter.WriteLine(characterAppearanceInfo);

            // character location info
            string characterLocationString = transform.position.x + " " + transform.position.y + " " + transform.position.z;
            streamWriter.WriteLine(SceneManager.GetActiveScene() + " " + characterLocationString);

            // user info
            streamWriter.WriteLine(name);

            // valley info
            streamWriter.WriteLine(valleyName);

            // npc love
            // 0 0 0 0 0 0 0 0
            streamWriter.WriteLine("0 0 0 0 0 0 0 0");

            // crop info

            streamWriter.Close();
            fileStream.Close();
        }
        catch (Exception exception)
        {
            Debug.Log("Save Failed: " + exception.Message);
        }
        return true;
    }

    // 초기 시작 시 설정
    void InitialSettings()
    {
        // 아이템 정보 불러오기(
        ItemSettings();

        // inventory settings
        InventorySettings();

        Load();


        // time info
        //Debug.Log(GameTime.ToString());

        // story status info

        // gold info
        //Debug.Log(Gold.ToString());

        // status info
        //Debug.Log(Status.ToString());

        // item info
        //foreach (Item item in itemList)
        //{
        //    Debug.Log(item.ToString());
        //}
        // chararcter info

        // user info
        //Debug.Log(name);

        // valley info
        //Debug.Log(valleyName);
    }

    // 처음 시작 시간을 설정하는 함수
    void TimeSettings(int day = 1, int hour = 6, int minute = 0)
    {
        time = 0.0f;
        GameTime.day = day;
        GameTime.hour = hour;
        GameTime.minute = minute;

        GameTime.ToString();
    }

    void InventorySettings()
    {
        inventory = GetComponent<Inventory>();
        inventory.ShowSemiInventory(itemList);
        inventory.CloseMenu();
        //inven.ShowInventory(itemList); ->ESC를 눌러 메뉴의 인벤토리를 켰을 때만 실행
    }

    // item 정보를 읽어오는 함수
    void ItemSettings()
    {
        Settings.RoadItemName();
    }

    private void ChangeItemListUI()
    {
        if (inventory.MenuStatus)
            inventory.RenewInventory(itemList);
        inventory.RenewSemiInventory(itemList);
    }

    public void AddItem(string itemCode, int count)
    {
        if (count < 1) return;

        if (itemList.Count>=1)
            foreach (Item item in itemList)
            {
                if (item.ItemCode.Equals(itemCode))
                {
                    item.Count += count;

                    ChangeItemListUI();
                    return;
                }
            }

        itemList.Add(new Item(itemCode, count));
        if(isReady)
            ChangeItemListUI();
    }
    public void UseItem(string itemCode)
    {
        foreach (Item item in itemList)
        {
            if (item.ItemCode.Equals(itemCode))
            {
                item.Count -= 1;

                if (item.Hp != -1)
                    Status.CUR_HP += item.Hp;

                if (item.Energe != -1)
                    Status.CUR_ENERGE += item.Energe;

                // item effect

                if (item.Count == 0)
                    itemList.Remove(item);

                ChangeItemListUI();
            }
        }
    }
    public void SellItem(string itemCode)
    {
        foreach (Item item in itemList)
        {
            if (item.ItemCode.Equals(itemCode))
            {
                item.Count -= 1;

                Gold.GOLD += item.Cost;

                if (item.Count == 0)
                    itemList.Remove(item);

                ChangeItemListUI();
            }
        }
    }
    public void BuyItem(string itemCode)
    {
        int cost = Settings.GetItemInfo(itemCode).Buy;

        if (Gold.GOLD >= cost)
        {
            Gold.GOLD -= cost;

            AddItem(itemCode, 1);

            ChangeItemListUI();
        }
        else
        {
            Debug.Log("돈이 부족");
        }
    }
    public Item GetItemAt(int i)
    {
        Item item = null;

        if (i > 0 && i <= itemList.Count)
            item = itemList[i - 1];

        return item;
    }

    // inventory
    public static List<Item> GetItemList()
    {
        return itemList;
    }
    public static int Day
    {
        get
        {
            return GameTime.day;
        }
    }
    public static int Hour
    {
        get
        {
            return GameTime.hour;
        }
    }
    public static int Minute
    {
        get
        {
            return GameTime.minute;
        }
    }

    public void ShowHp()
    {
        float HP = (float)Status.CUR_HP; //캐릭터 hp를 받아옴
        hpbar.fillAmount = HP / 150f;
    }

    public void ShowEnergy()
    {
        float ENERGY = (float)Status.CUR_ENERGE;
        energybar.fillAmount = ENERGY / 200f;
    }

    public void ShowTime()
    {
        Timer.GetComponent<Text>().text = GameTime.ToString();
    }

    public void ShowGold()
    {
        Money.GetComponent<Text>().text = Gold.GOLD.ToString();
    }

    public void ShowSun()
    {
        int nowTime = Hour;
        if(nowTime >= 6 || nowTime <= 18)
        {
            moon.SetActive(false);
            sun.SetActive(true);
        }
        else
        {
            sun.SetActive(false);
            moon.SetActive(true);
        }
    }

    public static string Name
    {
        get
        {
            return name;
        }
    }

    public static string VillageName
    {
        get
        {
            return valleyName;
        }
    }
}
