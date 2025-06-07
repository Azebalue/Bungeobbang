using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    CustomerData CustomerData; //SO 데이터
    static int level = 1; //손님 레벨
    static int Ex; //누적 손님 만족도

    UI_Order ui_order;
    UI_Order UI_order
    {
        get { 
            if(ui_order == null)
                ui_order = Util.FindObject(gameObject, "UI_Order", true).GetComponent<UI_Order>();

            return ui_order;
        }
    }

    #region 주문 관련 변수
    Dictionary<FillingType, int> order = new Dictionary<FillingType, int>(); //붕어빵 종류, 개수
    int numsOfFishBun; //주문하는 붕빵 개수
    int numsOfOrderType; //주문하는 맛 종류 개수

    //붕어빵 주문 개수 범위
    int minFishBun = 1;
    int maxFishBun = 3;

    //붕어빵 종류 개수 범위
    const int minOrderType = 1; 
    const int maxOrderType = 3;

    int orderAngryPoint; //주문 관련 불만도
    public int AngryPoint //종합 불만도 (주문 + 대기 시간)
    {
        get
        {
            int angryPoint = orderAngryPoint + (int)WaitingTime;

            if (angryPoint >= 100)
                return 100;
            else
                return angryPoint;

        }
    }
    #endregion

    #region 시간 관련 변수
    float startTime;
    float endTime;
    float WaitingTime 
    {
        get {
            endTime = Managers.Game.delta;
            return endTime - startTime;  
        }
        set { startTime = value; }
    }

    int reactionTime = 1;
    #endregion

    public event Action customerInitAction;

    void Awake()
    {
        //UI_order 프리팹 위치 고정
        // UI_order.gameObject.transform.SetParent(gameObject.transform);
        
        customerInitAction -= InitCustomer;
        customerInitAction += InitCustomer;

        customerInitAction -= Order;
        customerInitAction += Order;

    }

    void Update()
    {
        UI_order.slider.value = AngryPoint * 0.01f;

        //분노 게이지가 100이 되면 화남
        if (AngryPoint == 100)
            StartCoroutine(Exit());
    }

    public void InitCustomer()
    {

        UI_order.gameObject.SetActive(false);

        //1. 만족도 관련 변수 측정 시작
        startTime = Managers.Game.delta;
        orderAngryPoint = 0;

        //1. 손님 종류 랜덤 지정
        int customerType = UnityEngine.Random.Range(0, Util.GetEnumSize(typeof(CustomerType)));
        CustomerData = Managers.Resource.LoadCustomerSO((CustomerType)customerType);

        //2. 손님 스프라이트
        GetComponent<SpriteRenderer>().sprite = CustomerData.GetImage();
        //콜라이더 reset
        Destroy(gameObject.GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();

    }

    public void Order()
    {

        //맛 중복 방지를 위한 범위 리스트
        List<int> orderableFillingType = new List<int>();

        //주문 가능한 맛 범위(orderableFillingType)에 대해 초기화
        for (int i = 0; i < Managers.Game.CurData.numOfFilling; ++i)
            orderableFillingType.Add(i);

        //1. 주문할 붕어빵 개수
        //Debug.Log($"최저 {minFishBun}, 최고 {maxFishBun+1}");
        numsOfFishBun = UnityEngine.Random.Range(minFishBun, maxFishBun+1);
        //Debug.Log($"주문할 붕어빵 {numsOfFishBun}개");

        //붕어빵 랜덤 종류*개수 
        while(numsOfFishBun != 0)
        {
            //종류 랜덤
            int randomIndex = UnityEngine.Random.Range(0, orderableFillingType.Count);
            FillingType fillingType = (FillingType)orderableFillingType[randomIndex];
            orderableFillingType.RemoveAt(randomIndex); //고른 맛 빼기


            //개수 랜덤
            int _numsOfFishBun ; // fillingType맛으로 시킬 붕빵 개수
            //남은 붕어빵 개수 1개 이상일 때에만 랜덤
            if (numsOfFishBun > 1) 
                _numsOfFishBun = UnityEngine.Random.Range(1, numsOfFishBun - 1);
            else 
                _numsOfFishBun = 1;

            Debug.Log($"{fillingType}맛 {_numsOfFishBun}개");

            numsOfFishBun -= _numsOfFishBun;
            order.Add(fillingType, _numsOfFishBun);

        }

        UI_order.SetOrderText(order); 
        UI_order.gameObject.SetActive(true); 


    }

    public void Eat(FillingType filling, QualityStatus baking)
    {
        int perfectPoint = 20;
        int normalPoint = 10;
        int disappointPoint = 5;

        //1. 종류가 맞는 지 체크
        if (order.ContainsKey(filling) == true)
        {
            //맛 체크
            if(baking == QualityStatus.perfect)
                orderAngryPoint -= perfectPoint;
            else
                orderAngryPoint -= normalPoint;

            //order 딕셔너리 정리
            if(--order[filling] == 0)
            {
                Debug.Log("한 종류 다 먹음");

                order.Remove(filling); //딕셔너리 제거

                if(order.Count == 0)
                {
                    orderAngryPoint -= normalPoint;
                    StartCoroutine(Exit());
                }
            }

            


        }
        else
        {
            //기획 고민중..

            //다른 맛을 주면 불만 up
            orderAngryPoint += disappointPoint;

            //환불해줘야 하나

        }

        //다시 업뎃
        UI_order.SetOrderText(order);


    }

/*    void Exit()
    {
        int angryPoint = AngryPoint;

        //if ( angryPoint )
        
        // 대화 팝업 없애기
        UI_order.gameObject.SetActive(false);

        //1초 웃고 사라짐
        GetComponent<SpriteRenderer>().sprite = CustomerData.GetImage(1); //만족
        gameObject.SetActive(false);
        //StartCoroutine( () => { }, 2);
        //
    }*/

    IEnumerator Exit()
    {
        int angryPoint = AngryPoint;

        // 대화 팝업 없애기
        UI_order.gameObject.SetActive(false);

        //반응 효과
        Sprite reaction;
        if(AngryPoint >= 100)
            reaction = CustomerData.GetImage(2); //불만족
        else
            reaction = CustomerData.GetImage(1); //만족


        GetComponent<SpriteRenderer>().sprite = reaction;


        yield return new WaitForSeconds(reactionTime);
        gameObject.SetActive(false);

        yield break;

    }

}
