using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class TimeManager
{
    public int hour;
    public int minute;

    public float delta;
    public int timeWeight = 2;

    public void InitTime()
    {
        hour = 18;
        minute = 0;
        ++Managers.Game.curData.day;
    }

    public void SimulateTime()
    {
        if (hour < 23)
        {

            delta += timeWeight * Time.deltaTime;
            if (delta > 9)
            {
                minute += 1;
                delta = 0;

                if (minute >= 60)
                {
                    ++hour;
                    minute = 0;
                }

            }

        }
        else
        {
            //게임 끝
            //내일
        }
    }


    public int GetTotalMinutes() => hour * 60 + minute;
}