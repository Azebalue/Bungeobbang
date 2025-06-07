using System;
using UnityEngine;

public class GameData
{
    public int day;

    public int money;
    public int gold;
    public int spritPiece;

    //����

    //�رݵ� ��� ����
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

    #region �ð� ���� ����
    readonly int startHour = 18;
    readonly int endHour = 23;

    public int hour
    { get { return (int)delta / 60 + startHour; } }

    public int minute
    { get { return (int)delta % 60; } }


    public float delta;
    public int gameSpeed = 1;

    bool shouldRunning; //���� �������
    public bool didInitDay; //�Ϸ� ���� ó�� �ߴ���
    bool hasDayEnd; //�Ϸ� ���� ó�� �ߴ���

    GameObject gameObject;
    #endregion

    //���� ���� ��
    public void InitGame()
    {
        Debug.Log("���� ����");
        CurData.day = 0;
        shouldRunning = true;

        CurData.numOfFilling = 3;

        //GameObjectController.Instance.Bind();

    }

    //�Ϸ� � �޼���
    public void runGame()
    {
        //1. 5�ð� �ؾ ���� �

        if(shouldRunning == true)
        {
            //1-1. �Ϸ� ����
            if (didInitDay == false)
            {
                InitNextDay();
                didInitDay = true;
                GameObjectController.Instance.enabled = true;
            }


            //1-2. � �� �ð� ī����
            delta += gameSpeed * Time.deltaTime;

            //� ����
            if (hour >= endHour)
            {
                shouldRunning = false;
                didInitDay = false;

            }
        }
        //2. � ��� UI & ���� UI
        else
        {
            //�Ϸ� ���� ó���ߴ���
            if (hasDayEnd == false)
            {
                Managers.UI.CloseUI();
                Managers.UI.ShowUI<UI_DayEnd>();
                GameObjectController.Instance.enabled = false;
                hasDayEnd = true;

            }
        }
    }

    //���� �ʱ�ȭ & ����
    public void InitNextDay()
    {
        //������ init
        shouldRunning = true;
        delta = 0;
        ++CurData.day;

        hasDayEnd = false;
        didInitDay = true;

        //UIȭ��
        Managers.UI.CloseUI();
        Managers.UI.ShowUI<UI_Game>();


    }

}
