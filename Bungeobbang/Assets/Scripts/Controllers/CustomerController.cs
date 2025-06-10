using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    CustomerData CustomerData; //SO 데이터
/*    static int level = 1; //손님 레벨
    static int Ex; //누적 손님 만족도*/

    #region 게임 오브젝트 관련 변수
    UI_Order ui_order;
    UI_Order UI_order
    {
        get
        {
            if (ui_order == null)
                ui_order = Util.FindObject(gameObject, "UI_Order", true).GetComponent<UI_Order>();

            return ui_order;
        }
    }

    GameObject customer;
    public GameObject Customer
    {
        get
        {
            if (customer == null)
                customer = Util.FindObject(gameObject, "Sprite", true);

            return customer;

        }
    }
    #endregion

    #region 주문 관련 변수
    Dictionary<FillingType, int> order = new Dictionary<FillingType, int>(); //붕어빵 종류, 개수
    static Dictionary<FillingType, int> allOrder
        = new Dictionary<FillingType, int>(); //주문 전체
    int numsOfFishBun; //주문하는 붕빵 개수
    public int NumOfFishBun
    {
        get
        {
            return numsOfFishBun;
        }
        set
        {
            numsOfFishBun = value;
        }
    }

    //붕어빵 주문 개수 범위
    int minFishBun = 1;
    int maxFishBun = 3;

    //붕어빵 종류 개수 범위
    const int minOrderType = 1;
    const int maxOrderType = 3;

    int orderAngryPoint; //주문 관련 불만도
    int angryPoint; //종합 불만도
    public int AngryPoint //종합 불만도 (주문 + 대기 시간)
    {
        get
        {
            angryPoint = orderAngryPoint + (int)WaitingTime;

            if (angryPoint >= 100)
            {

                return 100;
            }
            else
                return angryPoint;

        }
        set
        {
            AngryPoint = Mathf.Clamp(value, 0, 100);
            //Debug.Log($"angryPoint: {value} => {angryPoint} VS {AngryPoint}");

        }
    }

    int pay = 0;
    #endregion

    #region 시간 관련 변수
    float startTime;
    float endTime;
    float WaitingTime
    {
        get
        {
            endTime = Managers.Game.delta;
            return endTime - startTime;
        }
        set { startTime = value; }
    }

    int reactionTime = 1;
    #endregion

    bool didInstatiated = false;

    void Awake()
    {
        Managers.Game.InitObjAction -= CoInstantiateCustomer;
        Managers.Game.InitObjAction +=  CoInstantiateCustomer;

/*        Managers.Game.InitObjAction -= CoInstantiateCustomer;
        Managers.Game.InitObjAction += CoInstantiateCustomer;*/
    }

    void Update()
    {
        //플레이어 존재
        if (AngryPoint < 100)
        {
            UI_order.slider.value = AngryPoint;
        }
        else
        {
            if(didInstatiated == false)
            {
                StartCoroutine(Exit()); //퇴장
                didInstatiated = true;
            }
        }


    }

    public void InitCustomer()
    {
        Debug.Log("손님 초기화");

        UI_order.gameObject.SetActive(false);
        Customer.gameObject.SetActive(false);

        //1. 만족도 관련 변수 측정 시작
        startTime = Managers.Game.delta;
        orderAngryPoint = 0;

        //1. 손님 종류 랜덤 지정
        int customerType = UnityEngine.Random.Range(0, Util.GetEnumSize(typeof(CustomerType)));
        CustomerData = Managers.Resource.LoadCustomerSO((CustomerType)customerType);

        //2. 손님 스프라이트
        customer.GetComponent<SpriteRenderer>().sprite = CustomerData.GetImage();
        //콜라이더 reset
        Destroy(customer.gameObject.GetComponent<PolygonCollider2D>());
        customer.gameObject.AddComponent<PolygonCollider2D>();

        pay = 0;

    }

    public void Order()
    {

        //주문 내역 비우기
        order.Clear();

        //맛 중복 방지를 위한 범위 리스트
        List<int> orderableFillingType = new List<int>();

        //주문 가능한 맛 범위(orderableFillingType)에 대해 초기화
        for (int i = 0; i < Managers.Game.CurData.numOfFilling; ++i)
            orderableFillingType.Add(i);

        //1. 주문할 붕어빵 개수
        NumOfFishBun = UnityEngine.Random.Range(minFishBun, maxFishBun + 1);
        //Debug.Log($"[Order]{gameObject.name}의 주문 : 총 {NumOfFishBun}개");

        //붕어빵 랜덤 종류*개수 
        for (int fishbun = NumOfFishBun; fishbun > 0;)
        {
            //종류 랜덤
            int randomIndex = UnityEngine.Random.Range(0, orderableFillingType.Count);
            FillingType fillingType = (FillingType)orderableFillingType[randomIndex];
            orderableFillingType.RemoveAt(randomIndex); //고른 맛 빼기

            //개수 랜덤
            int _numsOfFishBun; // fillingType맛으로 시킬 붕빵 개수
            //남은 붕어빵 개수 1개 이상일 때에만 랜덤
            if (fishbun > 1)
                _numsOfFishBun = UnityEngine.Random.Range(1, fishbun - 1);
            else
                _numsOfFishBun = 1;

            fishbun -= _numsOfFishBun;
            order.Add(fillingType, _numsOfFishBun);

        }

        UI_order.SetOrderText(order);
        UI_order.gameObject.SetActive(true);


    }

    public void Eat(FillingType filling, QualityStatus baking)
    {
        int perfectPoint = (100 / NumOfFishBun);
        int normalPoint = (int)(perfectPoint * 0.8);
        int disappointPoint = 20;

        //1. 종류가 맞는 지 체크
        if (order.ContainsKey(filling) == true)
        {

            //맛 체크
            if (baking == QualityStatus.perfect)
            {
                //Debug.Log($"{AngryPoint} - {perfectPoint}");
                orderAngryPoint -= perfectPoint;
                //Debug.Log($"{AngryPoint}로 내려감");

            }
            else
                orderAngryPoint -= normalPoint;



            //지불할 돈 적립
            pay += Define.FillingPrice[(int)filling];
            //Debug.Log($"지금까지 {pay}원 어치 먹음 ");

            //order 딕셔너리 정리
            if (--order[filling] == 0)
            {
                order.Remove(filling); //딕셔너리 제거

                if (order.Count == 0)
                {
                    orderAngryPoint -= normalPoint;
                    StartCoroutine(Exit());
                }
            }




        }
        else
        {
            //다른 맛을 주면 불만 미세하게 down
            orderAngryPoint -= disappointPoint;

            //환불해줘야 하나

        }

        //다시 업뎃
        UI_order.SetOrderText(order);

        //통계 업뎃
        ++Managers.Game.totalFishBunsSold;
        

    }

    IEnumerator Exit()
    {
        //Debug.Log($" {gameObject.name} Exit 시작");

        //반응 효과
        Sprite reaction;
        if (AngryPoint >= 100)
            reaction = CustomerData.GetImage(2); //불만족
        else
            reaction = CustomerData.GetImage(1); //만족

        // 대화 팝업 없애기
        UI_order.gameObject.SetActive(false);

        customer.GetComponent<SpriteRenderer>().sprite = reaction;

        yield return new WaitForSeconds(reactionTime);

        //돈 내기
        Managers.Game.CurData.money += pay;

        //손님 비활성화
        customer.gameObject.SetActive(false);

        //Debug.Log($" {gameObject.name} Exit 끝");
        
        //다음 손님
        StartCoroutine(InstatiateCustomer());
        yield break;

    }


    public void CoInstantiateCustomer()
    {
        StartCoroutine(InstatiateCustomer());
    }

    IEnumerator InstatiateCustomer()
    {
        //Debug.Log($"{gameObject.name} InstatiateCustomer 시작");

        InitCustomer();
        ++Managers.Game.totalCustomers;

        //스폰 대기 시간 관련 변수
        float spawnDalayMin = 3f;
        float spawnDalayMax = 8f;

        float spawnDelayTime = UnityEngine.Random.Range(spawnDalayMin, spawnDalayMax);
        Debug.Log($"1. {spawnDelayTime} 초 후 생성");

        spawnDelayTime /= Managers.Game.GameSpeed; //시간 속도
        Debug.Log($"2. {spawnDelayTime} 초 후 생성");

        yield return new WaitForSeconds(spawnDelayTime);

        customer.gameObject.SetActive(true);
        Order();

        //Debug.Log($"{gameObject.name} InstatiateCustomer 끝");

        yield break;
    }

}
