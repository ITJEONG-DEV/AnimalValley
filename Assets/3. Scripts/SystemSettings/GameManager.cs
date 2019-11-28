﻿using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private const string path = "Assets/Scripts/SystemSettings/CharacterSettings/data.data";
    private List<Item> itemList = new List<Item>();
    private float time;

    private string name;
    private string valleyName;

    void Start()
    {
        // 게임 시작 시, 초기 설정(불러오기)
        InitialSettings();


        // code for test
        /*
        TimeSettings();
        
        AddItem("TTSM", 1);
        AddItem("BED5", 7);

        Gold.GOLD = 500;

        Status.SetHp(150);
        Status.SetEnerge(200);

        name = "itjeong";
        valleyName = "apple cream roll";

        Save();
        */
    }

    void Update()
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

            for(int i=0; i<tempStr.Length/2; i++)
            {
                AddItem(tempStr[i * 2], int.Parse(tempStr[i * 2 + 1]));
            }

            // character info : characterType, characterSkin, characterEmotion, accessory1(head), accessory2(body)

            // user info
            tempStr = streamReader.ReadLine().Split(' ');
            name = tempStr[0];

            // valley info
            valleyName = streamReader.ReadLine().Trim() ;

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

            // user info
            streamWriter.WriteLine(name);

            // valley info
            streamWriter.WriteLine(valleyName);

            streamWriter.Close();
            fileStream.Close();
        }
        catch (Exception exception)
        {
            Debug.Log("Save Failed: " + exception.Message);
        }
        return true;
    }

    void InitialSettings()
    {
        // 아이템 정보 불러오기(
        ItemSettings();

        Load();

        // time info
        Debug.Log(GameTime.ToString());

        // story status info

        // gold info
        Debug.Log(Gold.ToString());

        // status info
        Debug.Log(Status.ToString());

        // item info
        foreach(Item item in itemList)
        {
            Debug.Log(item.ToString());
        }
        // chararcter info

        // user info
        Debug.Log(name);

        // valley info
        Debug.Log(valleyName);
    }

    void TimeSettings(int day = 1, int hour = 6, int minute = 0)
    {
        time = 0.0f;
        GameTime.day = day;
        GameTime.hour = hour;
        GameTime.minute = minute;

        GameTime.ToString();
    }

    void ItemSettings()
    {
        Settings.RoadItemName();
    }

    public void AddItem(string itemCode, int count)
    {
        if (count < 1) return;

        foreach (Item item in itemList)
        {
            if (item.ItemCode.Equals(itemCode))
            {
                item.Count += count;

                return;
            }
        }

        itemList.Add(new Item(itemCode, count));
    }
    public Item GetItemAt(int i)
    {
        Item item = null;

        if (i > 0 && i <= itemList.Count)
            item = itemList[i - 1];

        return item;
    }

    int Day
    {
        get
        {
            return GameTime.day;
        }
    }
    int Hour
    {
        get
        {
            return GameTime.hour;
        }
    }
    int Minute
    {
        get
        {
            return GameTime.minute;
        }
    }
}