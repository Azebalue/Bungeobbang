using System;
using System.Collections.Generic;
using UnityEngine;
using static Util;

public class GameData
{
    public int day;

    public int money;
    //public int spritPiece;

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


    public float delta; //시간
    int gameSpeed = 1; //게임 속도
    public int GameSpeed
    {
        get
        {
            return gameSpeed * Managers.Instance._gameSpeed;
        }
    }

    //운영 관련 변수
    bool hasInitialized = false; //가게 운영 시작처리했는지
    bool hasFinalized= false; //가게 운영 끝처리했는지
    public bool isRunning = true; //가게 운영 중인지(정지 여부 포함)

    #endregion

    #region 게임 요소 관련 변수
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

    #region 운영 결과 관련 변수
    public int totalFishBunsSold;      // 판매한 붕어빵 수
    public int totalCustomers;         // 방문한 손님 수

    public int yesterdayProfit;      // 어제 수익
    private int ingredientCost;         // 재료 비용
    public int IngredientCost
    {
        get { return ingredientCost; }
        set { ingredientCost = value; }
    }

    // 오늘 매출
    public int todayRevenue;

    public int netProfit //오늘 순수익
    {
        get {
            //Debug.Log($"netProfit: {todayRevenue} - {ingredientCost} = {todayRevenue - ingredientCost}");
            return  (todayRevenue - ingredientCost); }
    }



    #endregion

    //현재 주문 
    Dictionary<FillingType, int> order = new Dictionary<FillingType, int>();
    public Dictionary<FillingType, int> Order
    {
        get { return order; }
        set
        {

            order = value;
        }
    }

    public event Action InitObjAction;

    //게임 생성 시 초기화 메서드
    public void InitGame()
    {
        Debug.Log("게임 초기화");

        //게임 시작 1회에만, 화면 상 게임 오브젝트 찾아서 변수랑 맵핑/바인딩
        //1. 손님(customer) 오브젝트
        for (int i = 0; i < numsOfCustomers; ++i)
            customerArr[i] = FindObject(ParentGo, $"customer{i + 1}", true); 

        //2. 필링(fillings) 오브젝트
        for (int i = 0; i < GetEnumSize(typeof(FillingType)); ++i)
            fillingArr[i] = FindObject(ParentGo, $"{(FillingType)i}", true); 

        //3. 데이터 초기화
        CurData.day = 0;
        CurData.numOfFilling = 4;
        CurData.money = 0;

        isRunning = true;
        hasInitialized = false;
        hasFinalized = false;

    }

    //하루 운영 메서드
    public void OnUpdate()
    {
        if (isRunning == false)
            return;

        //하루 시작 처리 (1회성)
        if (hasInitialized == false)
        {
            InitDaily();
            hasInitialized = true;
        }

        //하루 끝 처리 (1회성)
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
            //가게 운영: 시간 계산
            delta += Time.deltaTime * GameSpeed;
        }
    }

    void InitDaily()
    {
        Debug.Log("1. 하루 시작");

        //1. 데이터 초기화
        hasFinalized = false;
        delta = 0; 
        ++CurData.day;

        totalFishBunsSold = 0;      
        totalCustomers = 0;         
        ingredientCost = 0;
        yesterdayProfit = CurData.money;



        //2. UI화면
        Managers.UI.CloseUI();
        Managers.UI.ShowUI<UI_Game>();

        //3. 오브젝트 활성화/비활성화 
        InitObjAction?.Invoke();

        //4. 필링 활성화/비활성화
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
        Debug.Log("2. 하루 끝 ");

        isRunning = false;
        order.Clear();

        //정산
        todayRevenue = CurData.money - yesterdayProfit;
        CurData.money -= ingredientCost;

        Managers.UI.CloseUI();
        Managers.UI.ShowUI<UI_DayEnd>();

    }

    public void StartNextDay()
    {
        Debug.Log("3. 다음 날로 넘어가기");

        isRunning = true;
        hasInitialized = false;

    }

    #region 주문
    public void acceptOrder(Dictionary<FillingType, int> orders)
    {
        foreach (var _order in orders)
        {
            if(order.ContainsKey(_order.Key) == true)
            {
                order[_order.Key] += _order.Value;
                //Debug.Log($"{_order.Key}: {_order.Value} += {order[_order.Key]}개");

            }
            else
            {
                //Debug.Log($"{_order.Key} 새로운 맛 주문 받음");
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
            //Debug.Log($"주문 취소 {order.Key}: {Order[order.Key]} - {order.Value}");

            Order[order.Key] -= order.Value;
            //Debug.Log($"주문 취소 결과 {Order[order.Key]}");

            if (Order[order.Key] == 0)
                Order.Remove(order.Key);

        }

        UI_Game.orderUpdateAction?.Invoke();

    }
    #endregion
}
