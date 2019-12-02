using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Status
{
    private static int hp = 150;
    private static int current_hp;
    private static int energe = 200;
    private static int current_energe;

    public static string ToString()
    {
        return "hp : " + current_hp + "/" + hp + ", energe : " + current_energe + "/" + energe; 
    }

    public static int MAX_HP
    {
        get
        {
            return hp;
        }
    }
    public static int CUR_HP
    {
        get
        {
            return current_hp;
        }
        set
        {
            current_hp = value;

            if (value > hp)
                current_hp = hp;
        }
    }

    public static int MAX_ENERGE
    {
        get
        {
            return energe;
        }
    }
    public static int CUR_ENERGE
    {
        get
        {
            return current_energe;
        }
        set
        {
            current_energe = value;

            if (value > energe)
                current_energe = energe;
        }
    }

    public static void SetHp(int hp)
    {
        Status.hp = hp;
        Status.current_hp = hp;
    }

    public static void SetEnerge(int energe)
    {
        Status.energe = energe;
        Status.current_energe = energe;
    }

    public static void AddHP(int cost)
    {
        hp += cost;
    }

    public static void AddEnerge(int cost)
    {
        energe += cost;
    }
}

