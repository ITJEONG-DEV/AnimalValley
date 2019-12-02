using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    private static bool hasItemNamePairData = false;
    static List<ItemInfo> itemInfoList;

    public static bool HasItemNamePairData
    {
        get
        {
            return HasItemNamePairData;
        }
    }

    public static void RoadItemName()
    {
        itemInfoList = new List<ItemInfo>();

        try
        {
            string path = "Assets/3. Scripts/SystemSettings/CharacterSettings/item.data";
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader streamReader = new StreamReader(fileStream);

            string[] lines = streamReader.ReadToEnd().Trim('\n').Split('\n');

            foreach (string line in lines)
            {
                string[] data = line.Split(',');

                string itemCode = data[0];
                string name = data[1];
                int sell = int.Parse(data[2]);
                int buy = int.Parse(data[3]);
                int hp = int.Parse(data[4]);
                int energe = int.Parse(data[5]);

                itemInfoList.Add(new ItemInfo(itemCode, name, sell, buy, hp, energe));
            }

        }
        catch (Exception exception)
        {
            Debug.Log(exception.Message);
            Debug.Log("FAILED: ");
        }


        hasItemNamePairData = true;

    }
    public static string GetItemName(string itemCode)
    {
        string name = "";
        if (!hasItemNamePairData)
        {
            Debug.Log("아이템 정보를 불러오지 않았음");
            return null;
        }

        if (itemInfoList.Count > 1)
            foreach (ItemInfo itemValue in itemInfoList)
                if (itemValue.ItemCode.Equals(itemCode))
                    return itemValue.Name;

        return name;
    }
    public static ItemInfo GetItemInfo(string itemCode)
    {
        if(!hasItemNamePairData)
        {
            Debug.Log("아이템 정보를 불러오지 않았음");
            return null;
        }

        if(itemInfoList.Count > 1)
            foreach(ItemInfo itemValue in itemInfoList)
                if (itemValue.ItemCode.Equals(itemCode))
                    return itemValue;

        return null;
    }
}
