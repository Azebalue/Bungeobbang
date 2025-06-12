using System;
using System.Collections.Generic;
using UnityEngine;
using static Util;

public class GameData
{
    public int day;

    public int money;
    //public int spritPiece;

    //�رݵ� ��� ����
    public int numOfFilling;
}

public class GameManagerEx
{
    GameData gameData = new GameData();
    GameData CurData
    {
        get { return gameData; }
        set { gameData = value; }
    }

    #region GameData
    public int Day
    {
        get { 
            return CurData.day + Managers.Instance.day; }
        set { CurData.day = value; }
    }

    public int Money
    {
        get { return CurData.money + Managers.Instance.money; }
        set {
            Debug.Log($"�� �ٲ� {value}");
            CurData.money = value; }
    }

    public int NumOfFilling
    {
        get { return CurData.numOfFilling; }
        set { CurData.numOfFilling = value; }
    }
    #endregion

    #region �ð� ���� ����
    readonly int startHour = 18;
    readonly int endHour = 23;
    public int hour
    { get { return (int)delta / 60 + startHour; } }

    public int minute
    { get { return (int)delta % 60; } }


    public float delta; //�ð�
    float gameSpeed = 1f; //���� �ӵ�
    public float GameSpeed
    {
        get { return gameSpeed * Managers.Instance._gameSpeed; }
    }

    //� ���� ����
    bool hasInitialized = false; //���� � ����ó���ߴ���
    bool hasFinalized= false; //���� � ��ó���ߴ���
    public bool isRunning = true; //���� � ������(���� ���� ����)

    public int numsOfCurCustomers = 0;
    public bool isAllExited
    {
        get { return numsOfCurCustomers == 0; }
    }
    public bool isClosingTime
    {
        get { return hour >= endHour; }
    }
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

    GameObject[] fillingArr = new GameObject[GetEnumSize(typeof(FillingType))];

    #endregion

    #region ��� ���� ����
    public int totalFishBunsSold;      // �Ǹ��� �ؾ ��
    public int totalCustomers;         // �湮�� �մ� ��

    public int yesterdayProfit;      // ���� ����
    private int ingredientCost;         // ��� ���
    public int IngredientCost
    {
        get { return ingredientCost; }
        set { ingredientCost = value; }
    }

    public int todayRevenue;

    public int netProfit //���� ������
    {
        get {
            Debug.Log($"netProfit: {todayRevenue} - {ingredientCost} = {todayRevenue - ingredientCost}");
            return  (todayRevenue - ingredientCost); }
    }
    #endregion

    #region ���� ���� ����
    int clearCondition = 40000;
    int endingDay = 5;
    
    bool isEndingDay { get { return Day >= endingDay;  } }
    bool isOver { get { return Money <= 0;  } }
    bool isClear { get { return Money > clearCondition; } }
    #endregion

    //���� �ֹ� 
    Dictionary<FillingType, int> order = new Dictionary<FillingType, int>();
    public Dictionary<FillingType, int> Order
    {
        get { return order; }
        set { order = value; }
    }

    public event Action InitAction;

    //���� ���� �� �ʱ�ȭ �޼���
    public void InitGame()
    {
        Debug.Log("���� �ʱ�ȭ");

        //1. �ʸ�(fillings) ������Ʈ
        for (int i = 0; i < GetEnumSize(typeof(FillingType)); ++i)
            fillingArr[i] = FindObject(ParentGo, $"{(FillingType)i}", true);

        //2. ������ �ʱ�ȭ
        CurData.day = 0;
        CurData.numOfFilling = 4;
        CurData.money = 0;

        isRunning = true;
        hasInitialized = false;
        hasFinalized = false;

        numsOfCurCustomers = 0;


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
        //�Ϸ� �� ó�� (1ȸ��) ����: � ���� & ���� �մ� ����
        else if ( isClosingTime == true && isAllExited == true)
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

    #region �Ϸ� ��ƾ ó��
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

        //2. UIȭ��
        Managers.UI.CloseUI();
        Managers.UI.ShowUI<UI_Game>();

        //3. ������Ʈ Ȱ��ȭ/��Ȱ��ȭ 
        InitAction?.Invoke();

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
        Debug.Log("2. �Ϸ� �� & ���� üũ");

        isRunning = false;
        order.Clear();

        //����
        todayRevenue = Money - yesterdayProfit;
        //Debug.Log($"���� ��: {Money} - ���� ����{yesterdayProfit}");
        //Debug.Log($"���� ����: {todayRevenue} - ����: {ingredientCost} = ���� ������ {netProfit}");
        Money -= ingredientCost;

        //���� üũ
        if (Managers.Game.IsEnding() == true)
            return;

        Managers.UI.CloseUI();
        Managers.UI.ShowUI<UI_DayEnd>();



    }

    public void StartNextDay()
    {
        Debug.Log("3. ���� ���� �Ѿ��");


        isRunning = true;
        hasInitialized = false;


    }

    bool IsEnding()
    {
        Debug.Log("IsEnding ����");

        if (isOver == true)
        {
            Managers.UI.CloseUI();
            Managers.UI.ShowUI<UI_Ending>().SetInfo(EndingType.Over);
            return true;
        }
        else
        {
            Debug.Log($"{Day} VS {endingDay}");

            if (isEndingDay == false)
                return false;

            Debug.Log("5������");

            Managers.UI.CloseUI();

            if (isClear == true)
                Managers.UI.ShowUI<UI_Ending>().SetInfo(EndingType.Clear);
            else
                Managers.UI.ShowUI<UI_Ending>().SetInfo(EndingType.Normal);

            return true;
        }


    }

    #endregion

    #region �ֹ�
    public void acceptOrder(Dictionary<FillingType, int> orders)
    {
        foreach (var _order in orders)
        {
            if(order.ContainsKey(_order.Key) == true)
            {
                order[_order.Key] += _order.Value;
                //Debug.Log($"{_order.Key}: {_order.Value} += {order[_order.Key]}��");

            }
            else
            {
                //Debug.Log($"{_order.Key} ���ο� �� �ֹ� ����");
                order.Add(_order.Key, _order.Value);
            }

        }

        UI_Game.orderUpdateAction?.Invoke();

    }

    public void serveOrder(Dictionary<FillingType, int> orders, FillingType filling)
    {
        if (orders.ContainsKey(filling) == false)
            return;

        if(--Order[filling] == 0)
            Order.Remove(filling);

        UI_Game.orderUpdateAction?.Invoke();
    }

    public void cancelOrder(Dictionary<FillingType, int> orders)
    {
        foreach (var order in orders)
        {
            //Debug.Log($"�ֹ� ��� {order.Key}: {Order[order.Key]} - {order.Value}");
            if (Order.ContainsKey(order.Key) == false)
                return;

            Order[order.Key] -= order.Value;
            //Debug.Log($"�ֹ� ��� ��� {Order[order.Key]}");

            if (Order[order.Key] == 0)
                Order.Remove(order.Key);

        }

        UI_Game.orderUpdateAction?.Invoke();

    }
    #endregion

    #region ��Ÿ
    public void QuitGame()
    {
    #if UNITY_EDITOR
        //�����Ϳ��� ������ ��
        UnityEditor.EditorApplication.isPlaying = false; //������ ���� �ߴ�
    #else
        Application.Quit();
    #endif
    }

    #endregion

}
