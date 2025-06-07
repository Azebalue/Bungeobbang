using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Util;


public class GameObjectController : MonoBehaviour
{
    static GameObjectController instance;
    public static GameObjectController Instance
    {
        get { 
            if(instance == null)
            {
                instance = GameObject.Find("@GameObject").
                    GetComponent<GameObjectController>();
            }

            return instance; }
    }

    public static event Action<CustomerController> OnCustomerSpawned;

    //손님 오브젝트
    const int numsOfCustomers = 3;
    GameObject[] customerArr = new GameObject[numsOfCustomers];
    GameObject[] fillingArr = new GameObject[GetEnumSize(typeof(FillingType))];

    //활성화되지 않은 손님 인덱스를 저장한 큐
    Queue<int> emptyCustomerSlot = new Queue<int>();

    //스폰 대기 시간 관련 변수
    float spawnDalayMin = 2f;
    float spawnDalayMax = 8f;

    void Awake()
    {
        Bind();

    }

    //활성화될때마다
    void OnEnable()
    {
        //화면상 게임 오브젝트 초기화
        InitGameObjectDaily();

        //손님 호출
        StartCoroutine(InstatiateCustomer());
    }


    //게임 시작 시 초기화
    public void Bind()
    {
        //게임 시작 1회에만, 화면 상 게임 오브젝트 찾아서 변수랑 맵핑/바인딩

        //1. 손님(customer) 오브젝트
        for (int i = 0; i < numsOfCustomers; ++i)
            customerArr[i] = FindObject(gameObject, $"customer{i + 1}", true); //저장

        //2. 필링(fillings) 오브젝트
        for (int i = 0; i < GetEnumSize(typeof(FillingType)); ++i)
            fillingArr[i] = FindObject(gameObject, $"{(FillingType)i}", true); //저장

    }


    public IEnumerator InstatiateCustomer()
    {
        //손님 생성 메서드 로직 수정 필요

        while (true)
        {
            //주문해야 할 손님 슬롯이 비어있지 않으면 => spawnDelayTime초 후 손님 생성
            if (emptyCustomerSlot.Count > 0)
            {
                float spawnDelayTime = UnityEngine.Random.Range(spawnDalayMin, spawnDalayMax);
                Debug.Log($"{spawnDelayTime / Managers.Game.gameSpeed}초 후 생성");

                yield return new WaitForSeconds(spawnDelayTime / Managers.Game.gameSpeed);

                GameObject customer = customerArr[emptyCustomerSlot.Peek()];
                customer.SetActive(true);

                customerArr[emptyCustomerSlot.Peek()].GetComponent<CustomerController>().InitCustomer();
                customerArr[emptyCustomerSlot.Peek()].GetComponent<CustomerController>().Order();
                
                //customerAction.invoke();
                emptyCustomerSlot.Dequeue();


            }
            else
                break;
        }

    }

    public void InitGameObjectDaily()
    {
        //매일 초기화

        //1. 손님 활성화
        for (int i = 0; i < numsOfCustomers; ++i)
        {
            customerArr[i].SetActive(false); //비활성화
            emptyCustomerSlot.Enqueue(i); //주문해야 할 손님 인덱스 enqueue
        }

        //2. 필링 활성화
        for (int i = 0; i < GetEnumSize(typeof(FillingType)); ++i)
        {
            if (i < Managers.Game.CurData.numOfFilling)
                fillingArr[i].SetActive(true);
            else
                fillingArr[i].SetActive(false);
        }
    }
}

