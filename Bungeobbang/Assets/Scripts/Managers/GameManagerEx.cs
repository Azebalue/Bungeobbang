using System;
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
    bool isRunning = true; //가게 운영 중인지(정지 여부 포함)

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
            Debug.Log($"netProfit: {todayRevenue} - {ingredientCost} = {todayRevenue - ingredientCost}");
            return  (todayRevenue - ingredientCost); }
    }



    #endregion
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

    //void 
    /*
        {

            if (didDayEnd == true)
                return;

            Debug.Log("게임 플레이");

            //1. 5시간 붕어빵 가게 운영
            if (shouldStop == false)
            {
                //1-1. 하루 시작
                if (didInitDay == false)
                {
                    InitDaily();
                    didInitDay = true;

                }

                //1-2. 운영 중 시간 카운팅
                delta += gameSpeed * Time.deltaTime * Managers.Instance._gameSpeed;

                //1-3. 운영 종료
                if (hour >= endHour)
                {
                    shouldStop = true;
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
                    hasDayEnd = true;
                    didDayEnd = true;

                }
            }
        }

        //내일 초기화 & 시작
        public void InitDaily()
        {
            //1. 데이터 init
            shouldStop = false;
            delta = 0;
            ++CurData.day;

            hasDayEnd = false;
            didInitDay = true;

            //2. UI화면
            Managers.UI.CloseUI();
            Managers.UI.ShowUI<UI_Game>();

            //3. 손님 비활성화
            for (int i = 0; i < numsOfCustomers; ++i)
            {
                customerArr[i].GetComponent<CustomerController>().CoInstantiateCustomer();
                //customerArr[i].GetComponent<CustomerController>().Customer.SetActive(false); //비활성화

            }

            //4. 필링 활성화/비활성화
            for (int i = 0; i < GetEnumSize(typeof(FillingType)); ++i)
            {
                if (i < Managers.Game.CurData.numOfFilling)
                    fillingArr[i].SetActive(true);
                else
                    fillingArr[i].SetActive(false);
            }


        }*/


}
