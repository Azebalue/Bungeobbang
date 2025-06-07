using System;
using UnityEngine;

public class GameData
{
    public int day;

    public int money;
    public int gold;
    public int spritPiece;

    //도감

    //해금된 재료 개수
    public int numOfFilling;
}

public class GameManagerEx
{
    GameData gameData = new GameData();
    public GameData CurData
    {
        get { return gameData; }
        set { gameData = value; }
    }

    #region 시간 관련 변수
    readonly int startHour = 18;
    readonly int endHour = 23;

    public int hour
    { get { return (int)delta / 60 + startHour; } }

    public int minute
    { get { return (int)delta % 60; } }


    public float delta;
    public int gameSpeed = 1;

    bool shouldRunning; //가게 운영중인지
    public bool didInitDay; //하루 시작 처리 했는지
    bool hasDayEnd; //하루 종료 처리 했는지

    GameObject gameObject;
    #endregion

    //게임 생성 시
    public void InitGame()
    {
        Debug.Log("게임 시작");
        CurData.day = 0;
        shouldRunning = true;

        CurData.numOfFilling = 3;

        //GameObjectController.Instance.Bind();

    }

    //하루 운영 메서드
    public void runGame()
    {
        //1. 5시간 붕어빵 가게 운영

        if(shouldRunning == true)
        {
            //1-1. 하루 시작
            if (didInitDay == false)
            {
                InitNextDay();
                didInitDay = true;
                GameObjectController.Instance.enabled = true;
            }


            //1-2. 운영 중 시간 카운팅
            delta += gameSpeed * Time.deltaTime;

            //운영 종료
            if (hour >= endHour)
            {
                shouldRunning = false;
                didInitDay = false;

            }
        }
        //2. 운영 결과 UI & 상점 UI
        else
        {
            //하루 종료 처리했는지
            if (hasDayEnd == false)
            {
                Managers.UI.CloseUI();
                Managers.UI.ShowUI<UI_DayEnd>();
                GameObjectController.Instance.enabled = false;
                hasDayEnd = true;

            }
        }
    }

    //내일 초기화 & 시작
    public void InitNextDay()
    {
        //데이터 init
        shouldRunning = true;
        delta = 0;
        ++CurData.day;

        hasDayEnd = false;
        didInitDay = true;

        //UI화면
        Managers.UI.CloseUI();
        Managers.UI.ShowUI<UI_Game>();


    }

}
