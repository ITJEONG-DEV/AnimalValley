using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Gold
{
    private static int gold;

    public static string ToString()
    {
        return "gold : " + gold;
    }

    public static void SetGold(int cost)
    {
        gold = cost;
    }

    public static int GOLD
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
        }
    }
}
