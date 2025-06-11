using System;
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


    public float delta; //�ð�
    int gameSpeed = 1; //���� �ӵ�
    public int GameSpeed
    {
        get
        {
            return gameSpeed * Managers.Instance._gameSpeed;
        }
    }

    //� ���� ����
    bool hasInitialized = false; //���� � ����ó���ߴ���
    bool hasFinalized= false; //���� � ��ó���ߴ���
    public bool isRunning = true; //���� � ������(���� ���� ����)

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

    #region � ��� ���� ����
    public int totalFishBunsSold;      // �Ǹ��� �ؾ ��
    public int totalCustomers;         // �湮�� �մ� ��

    public int yesterdayProfit;      // ���� ����
    private int ingredientCost;         // ��� ���
    public int IngredientCost
    {
        get { return ingredientCost; }
        set { ingredientCost = value; }
    }

    // ���� ����
    public int todayRevenue;

    public int netProfit //���� ������
    {
        get {
            Debug.Log($"netProfit: {todayRevenue} - {ingredientCost} = {todayRevenue - ingredientCost}");
            return  (todayRevenue - ingredientCost); }
    }



    #endregion

    //���� �ֹ� 
    public static Dictionary<FillingType, int> order = new Dictionary<FillingType, int>();

    public event Action InitObjAction;

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
        CurData.numOfFilling = 4;
        CurData.money = 0;

        isRunning = true;
        hasInitialized = false;
        hasFinalized = false;

    }

    //�Ϸ� � �޼���
    public void OnUpdate()
    {
        if (isRunning == false)
            return;

        //�Ϸ� ���� ó�� (1ȸ��)
        if (hasInitialized == false)
        {
            InitDaily();
            hasInitialized = true;
        }

        //�Ϸ� �� ó�� (1ȸ��)
        else if (hour >= endHour)
        {

            if (hasFinalized == false)
            {
                FinalizeDaily();
                hasFinalized = true;
            }

        }

        else
        {
            //���� �: �ð� ���
            delta += Time.deltaTime * GameSpeed;
        }
    }

    void InitDaily()
    {
        Debug.Log("1. �Ϸ� ����");

        //1. ������ �ʱ�ȭ
        hasFinalized = false;
        delta = 0; 
        ++CurData.day;

        totalFishBunsSold = 0;      
        totalCustomers = 0;         
        ingredientCost = 0;
        yesterdayProfit = CurData.money;

        order.Clear();


    //2. UIȭ��
    Managers.UI.CloseUI();
        Managers.UI.ShowUI<UI_Game>();

        //3. ������Ʈ Ȱ��ȭ/��Ȱ��ȭ 
        InitObjAction?.Invoke();

        //4. �ʸ� Ȱ��ȭ/��Ȱ��ȭ
        for (int i = 0; i < GetEnumSize(typeof(FillingType)); ++i)
        {
            if (i < Managers.Game.CurData.numOfFilling)
                fillingArr[i].SetActive(true);
            else
                fillingArr[i].SetActive(false);
        }
    }

    void FinalizeDaily()
    {
        Debug.Log("2. �Ϸ� �� ");

        isRunning = false;

        //����
        todayRevenue = CurData.money - yesterdayProfit;
        CurData.money -= ingredientCost;

        Managers.UI.CloseUI();
        Managers.UI.ShowUI<UI_DayEnd>();

    }

    public void StartNextDay()
    {
        Debug.Log("3. ���� ���� �Ѿ��");

        isRunning = true;
        hasInitialized = false;
    }

    public void acceptOrder(Dictionary<FillingType, int> orders)
    {
        Debug.Log("�ֹ� ����");
        foreach (var _order in orders)
        {
            if (order.TryGetValue(_order.Key, out int count) == true)
            {
                Debug.Log($"{_order.Key} �̹� ����");
                order[_order.Key] += count;
            }
            else
            {
                Debug.Log($"{_order.Key} ���ο� �� �ֹ� ����");
                order.Add(_order.Key, _order.Value);
            }

        }
    }



}
