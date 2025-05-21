using UnityEngine;
using TMPro;

public class UI_Game : UI_Base
{
    [SerializeField] TextMeshProUGUI day;
    [SerializeField] TextMeshProUGUI time;

    //시간은 10분단위로 표시
    int _minute
    {
        get { return (Managers.Time.minute) % 10; }
    }

    protected override void Get()
    {
        day.text = $"Day {Managers.Game.curData.day}";

    }

    private void Update()
    {

        time.text = ($"{Managers.Time.hour} : {_minute.ToString("D2")}");


    }
}
