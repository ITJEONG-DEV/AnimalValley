using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    static class GameTime
    {
        public static int day, hour, minute;
    }

    private float time;

    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
        GameTime.day = 1;
        GameTime.hour = 6;
        GameTime.minute = 0;

        ShowTime();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time>=10.0f)
        {
            GameTime.minute += 5;
            if(GameTime.minute >= 60)
            {
                GameTime.hour += 1;
                GameTime.minute -= 60;

                if(GameTime.hour > 24)
                {
                    GameTime.hour -= 24;
                }
            }
            time = 0f;

            ShowTime();
        }

        if(GameTime.hour == 1)
        {
            // faint()
        }

        if(GameTime.hour == 6)
        {

        }
    }

    void ShowTime()
    {
        Debug.Log("Day " + GameTime.day.ToString("D2") + " " + GameTime.hour.ToString("D2") + ":" + GameTime.minute.ToString("D2"));
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
