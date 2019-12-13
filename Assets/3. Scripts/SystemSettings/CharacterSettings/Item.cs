using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private string itemCode;
    private int number;

    private string name;
    private int cost;
    private int hp;
    private int energe;

    public Item(string itemCode, int number)
    {
        ItemInfo itemInfo = Settings.GetItemInfo(itemCode);

        this.itemCode = itemCode;
        this.number = number;

        this.name = itemInfo.Name;
        this.cost = itemInfo.Sell;
        this.hp = itemInfo.Hp;
        this.energe = itemInfo.Energe;

        //itemIcon = "Assets/Item/Icon" + itemCode + ".png";
        //itemPrefab = "Assets/Item/Prefab" + itemCode + ".prefab";

        Debug.Log(ToString());
    }

    public string ToString()
    {
        return name + "(" + itemCode + ") "  + number + " " + cost + "G " + hp + "/" + energe;
    }

    public int Count
    {
        get
        {
            return number;
        }
        set
        {
            number = value;
        }
    }

    public string ItemCode
    {
        get
        {
            return itemCode;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }
    }

    public int Cost
    {
        get
        {
            return cost;
        }
    }

    public int Hp
    {
        get
        {
            return hp;
        }
    }

    public int Energe
    {
        get
        {
            return energe;
        }
    }
}

public class ItemInfo
{
    private string itemCode;
    private string name;
    private int sellPrice;
    private int price;
    private int hp;
    private int energe;

    public ItemInfo(string itemCode, string name, int sellPrice, int price, int hp, int energe)
    {
        this.itemCode = itemCode;
        this.name = name;
        this.sellPrice = sellPrice;
        this.price = price;
        this.hp = hp;
        this.energe = energe;
    }

    public string ToString()
    {
        return itemCode + " " + sellPrice + " " + price + " " + hp + " " + energe;
    }

    public string ItemCode
    {
        get
        {
            return itemCode;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }
    }

    public int Sell
    {
        get
        {
            return sellPrice;
        }
    }

    public int Buy
    {
        get
        {
            return price;
        }
    }

    public int Hp
    {
        get
        {
            return hp;
        }
    }

    public int Energe
    {
        get
        {
            return energe;
        }
    }
}