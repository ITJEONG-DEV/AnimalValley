using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private string itemCode;
    private string name;
    private int number;

    public Item(string itemCode, int number)
    {
        this.itemCode = itemCode;
        this.name = Settings.GetItemName(itemCode);
        this.number = number;
        //itemIcon = "Assets/Item/Icon" + itemCode + ".png";
        //itemPrefab = "Assets/Item/Prefab" + itemCode + ".prefab";

        Debug.Log(this.itemCode + "->" + this.name + "이 " + number + "개 생성됨");
    }

    public string ToString()
    {
        return name + "(" + itemCode + ") "  + number;
    }

    public int Count
    {
        get
        {
            return number;
        }
        set
        {
            number += value;
        }
    }
    public string ItemCode
    {
        get
        {
            return itemCode;
        }
    }
}
