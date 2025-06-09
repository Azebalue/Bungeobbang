using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Util;

public class GameData
{
    public int day;

    public int money;
    //public int spritPiece;

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

    bool shouldStop; //���� �������
    public bool didInitDay; //�Ϸ� ���� ó�� �ߴ���
    bool hasDayEnd; //�Ϸ� ���� ó�� �ߴ���
    #endregion

    #region ���� ��� ���� ����
    GameObject parentGo;
    public GameObject ParentGo
    {
        get
        {
            if (parentGo == null)
                parentGo = GameObject.Find("@GameObject");

            return parentGo;
        }
    }

    const int numsOfCustomers = 3;
    GameObject[] customerArr = new GameObject[numsOfCustomers];
    GameObject[] fillingArr = new GameObject[GetEnumSize(typeof(FillingType))];
    #endregion

    //���� ���� �� �ʱ�ȭ �޼���
    public void InitGame()
    {
        Debug.Log("���� �ʱ�ȭ");

        //���� ���� 1ȸ����, ȭ�� �� ���� ������Ʈ ã�Ƽ� ������ ����/���ε�
        //1. �մ�(customer) ������Ʈ
        for (int i = 0; i < numsOfCustomers; ++i)
            customerArr[i] = FindObject(ParentGo, $"customer{i + 1}", true); 

        //2. �ʸ�(fillings) ������Ʈ
        for (int i = 0; i < GetEnumSize(typeof(FillingType)); ++i)
            fillingArr[i] = FindObject(ParentGo, $"{(FillingType)i}", true); 

        //3. ������ �ʱ�ȭ
        CurData.day = 0;
        CurData.numOfFilling = 3;
        CurData.money = 0;

        shouldStop = false;

    }

    //�Ϸ� � �޼���
    public void runGame()
    {
        //1. 5�ð� �ؾ ���� �
        if(shouldStop == false)
        {
            //1-1. �Ϸ� ����
            if (didInitDay == false)
            {
                InitDaily();
                didInitDay = true;

            }

            //1-2. � �� �ð� ī����
            delta += gameSpeed * Time.deltaTime * Managers._gameSpeed;

            //1-3. � ����
            if (hour >= endHour)
            {
                shouldStop = true;
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
                hasDayEnd = true;

            }
        }
    }

    //���� �ʱ�ȭ & ����
    public void InitDaily()
    {
        //1. ������ init
        shouldStop = false;
        delta = 0;
        ++CurData.day;

        hasDayEnd = false;
        didInitDay = true;

        //2. UIȭ��
        Managers.UI.CloseUI();
        Managers.UI.ShowUI<UI_Game>();

        //3. �մ� ��Ȱ��ȭ
        for (int i = 0; i < numsOfCustomers; ++i)
        {
            customerArr[i].GetComponent<CustomerController>().CoInstantiateCustomer();
            //customerArr[i].GetComponent<CustomerController>().Customer.SetActive(false); //��Ȱ��ȭ
            //customerArr[i].GetComponent<CustomerController>().Customer.SetActive(false); //��Ȱ��ȭ

        }

        //4. �ʸ� Ȱ��ȭ/��Ȱ��ȭ
        for (int i = 0; i < GetEnumSize(typeof(FillingType)); ++i)
        {
            if (i < Managers.Game.CurData.numOfFilling)
                fillingArr[i].SetActive(true);
            else
                fillingArr[i].SetActive(false);
        }


    }


}
