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

    //�մ� ������Ʈ
    const int numsOfCustomers = 3;
    GameObject[] customerArr = new GameObject[numsOfCustomers];
    GameObject[] fillingArr = new GameObject[GetEnumSize(typeof(FillingType))];

    //Ȱ��ȭ���� ���� �մ� �ε����� ������ ť
    Queue<int> emptyCustomerSlot = new Queue<int>();

    //���� ��� �ð� ���� ����
    float spawnDalayMin = 2f;
    float spawnDalayMax = 8f;

    void Awake()
    {
        Bind();

    }

    //Ȱ��ȭ�ɶ�����
    void OnEnable()
    {
        //ȭ��� ���� ������Ʈ �ʱ�ȭ
        InitGameObjectDaily();

        //�մ� ȣ��
        StartCoroutine(InstatiateCustomer());
    }


    //���� ���� �� �ʱ�ȭ
    public void Bind()
    {
        //���� ���� 1ȸ����, ȭ�� �� ���� ������Ʈ ã�Ƽ� ������ ����/���ε�

        //1. �մ�(customer) ������Ʈ
        for (int i = 0; i < numsOfCustomers; ++i)
            customerArr[i] = FindObject(gameObject, $"customer{i + 1}", true); //����

        //2. �ʸ�(fillings) ������Ʈ
        for (int i = 0; i < GetEnumSize(typeof(FillingType)); ++i)
            fillingArr[i] = FindObject(gameObject, $"{(FillingType)i}", true); //����

    }


    public IEnumerator InstatiateCustomer()
    {
        //�մ� ���� �޼��� ���� ���� �ʿ�

        while (true)
        {
            //�ֹ��ؾ� �� �մ� ������ ������� ������ => spawnDelayTime�� �� �մ� ����
            if (emptyCustomerSlot.Count > 0)
            {
                float spawnDelayTime = UnityEngine.Random.Range(spawnDalayMin, spawnDalayMax);
                Debug.Log($"{spawnDelayTime / Managers.Game.gameSpeed}�� �� ����");

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
        //���� �ʱ�ȭ

        //1. �մ� Ȱ��ȭ
        for (int i = 0; i < numsOfCustomers; ++i)
        {
            customerArr[i].SetActive(false); //��Ȱ��ȭ
            emptyCustomerSlot.Enqueue(i); //�ֹ��ؾ� �� �մ� �ε��� enqueue
        }

        //2. �ʸ� Ȱ��ȭ
        for (int i = 0; i < GetEnumSize(typeof(FillingType)); ++i)
        {
            if (i < Managers.Game.CurData.numOfFilling)
                fillingArr[i].SetActive(true);
            else
                fillingArr[i].SetActive(false);
        }
    }
}

