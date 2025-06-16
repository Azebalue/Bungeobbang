using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomerController : MonoBehaviour, IPointerClickHandler
{
    CustomerData CustomerData; //SO ������
/*    static int level = 1; //�մ� ����
    static int Ex; //���� �մ� ������*/

    #region ���� ������Ʈ ���� ����
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

    #region �ֹ� ���� ����
    bool didAcceptOrder = false;
    Dictionary<FillingType, int> order = new Dictionary<FillingType, int>(); //�ؾ ����, ����
    int numsOfFishBun; //�ֹ��ϴ� �ػ� ����
    public int NumOfFishBun
    {
        get{ return numsOfFishBun; }
        set { numsOfFishBun = value; }
    }

    //�ؾ �ֹ� ���� ����
    int minFishBun = 1;
    int maxFishBun = 3;
/*
    //�ؾ ���� ���� ����
    const int minOrderType = 1;
    const int maxOrderType = 3;*/

    int orderAngryPoint; //�ֹ� ���� �Ҹ���
    int angryPoint; //���� �Ҹ���
    public int AngryPoint //���� �Ҹ��� (�ֹ� + ��� �ð�)
    {
        get
        {
            angryPoint = orderAngryPoint + (int)WaitingTime;
            angryPoint = Mathf.Clamp(angryPoint, 0, 100);
            return angryPoint;

        }
        set
        {
            angryPoint = Mathf.Clamp(value, 0, 100);
            //Debug.Log($"angryPoint: {value} => {angryPoint} VS {AngryPoint}");
        }
    }

    int pay = 0;
    #endregion

    #region �ð� ���� ����
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

    bool hasExited = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (didAcceptOrder == true)
            return;

        //�ֹ�
        Order();
        //�ֹ� ����
        Managers.Game.acceptOrder(order);
        didAcceptOrder = true;
    }

    void Awake()
    {
        Managers.Game.InitAction -= CoInstantiateCustomer;
        Managers.Game.InitAction +=  CoInstantiateCustomer;

    }

    void Update()
    {
        if (Managers.Game.isRunning == false)
            return;


        if (AngryPoint < 100)
        {
            UI_order.slider.value = AngryPoint;

        }
        else
        {
            Util.ExecuteOnce(
                () => { StartCoroutine(Exit(true)); },
                ref hasExited, false);
        }


    }

    public void InitCustomer()
    {

        UI_order.gameObject.SetActive(false);
        Customer.gameObject.SetActive(false);

        //1. ������ ���� ���� ���� ����
        startTime = Managers.Game.delta;
        orderAngryPoint = 0;

        //1. �մ� ���� ���� ����
        int customerType = UnityEngine.Random.Range(0, Util.GetEnumSize(typeof(CustomerType)));
        CustomerData = Managers.Resource.LoadCustomerSO((CustomerType)customerType);

        //2. �մ� ��������Ʈ
        customer.GetComponent<SpriteRenderer>().sprite = CustomerData.GetImage();
        //�ݶ��̴� reset
        Destroy(customer.gameObject.GetComponent<PolygonCollider2D>());
        customer.gameObject.AddComponent<PolygonCollider2D>();

        pay = 0;
        didAcceptOrder = false;
        ++Managers.Game.numsOfCurCustomers;
    }

    public void Order()
    {

        //�ֹ� ���� ����
        order.Clear();

        //�� �ߺ� ������ ���� ���� ����Ʈ
        List<int> orderableFillingType = new List<int>();

        //�ֹ� ������ �� ����(orderableFillingType)�� ���� �ʱ�ȭ
        for (int i = 0; i < Managers.Game.NumOfFilling; ++i)
            orderableFillingType.Add(i);

        //1. �ֹ��� �ؾ ����
        NumOfFishBun = UnityEngine.Random.Range(minFishBun, maxFishBun + 1);
        //Debug.Log($"[Order]{gameObject.name}�� �ֹ� : �� {NumOfFishBun}��");

        //�ؾ ���� ����*���� 
        for (int fishbun = NumOfFishBun; fishbun > 0;)
        {
            //���� ����
            int randomIndex = UnityEngine.Random.Range(0, orderableFillingType.Count);
            FillingType fillingType = (FillingType)orderableFillingType[randomIndex];
            orderableFillingType.RemoveAt(randomIndex); //�� �� ����

            //���� ����
            int _numsOfFishBun; // fillingType������ ��ų �ػ� ����
            //���� �ؾ ���� 1�� �̻��� ������ ����
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
        //�ֹ� �ȹ����� �ȸ���
        if(didAcceptOrder == false)
             return; 

        Managers.Game.serveOrder(order, filling);

        //�г�Point ����
        int perfectPoint = (100 / NumOfFishBun);
        int normalPoint = (int)(perfectPoint * 0.8);
        int disappointPoint = 20;

        //1. ������ �´� �� üũ
        if (order.ContainsKey(filling) == true)
        {

            //�� üũ
            if (baking == QualityStatus.perfect)
            {
                //Debug.Log($"{AngryPoint} - {perfectPoint}");
                orderAngryPoint -= perfectPoint;
                //Debug.Log($"{AngryPoint}�� ������");

            }
            else
                orderAngryPoint -= normalPoint;



            //������ �� ����
            pay += Define.FillingPrice[(int)filling];
            //Debug.Log($"���ݱ��� {pay}�� ��ġ ���� ");

            //order ��ųʸ� ����
            if (--order[filling] == 0)
            {
                order.Remove(filling); //��ųʸ� ����

                if (order.Count == 0)
                {
                    orderAngryPoint -= normalPoint;
                    StartCoroutine(Exit());
                }
            }




        }
        else
        {
            //�ٸ� ���� �ָ� �Ҹ� �̼��ϰ� down
            orderAngryPoint -= disappointPoint;
           
            //ȯ������� �ϳ�

        }

        //�ٽ� ����
        UI_order.SetOrderText(order);

        //��� ����
        ++Managers.Game.totalFishBunsSold;
        

    }

    IEnumerator Exit(bool isAngry = false)
    {
        //Debug.Log($" {gameObject.name} Exit ����");

        //���� ȿ��
        Sprite reaction;
        if (isAngry == true)
        {
            reaction = CustomerData.GetImage(2); //�Ҹ���
            if(didAcceptOrder == true)
                Managers.Game.cancelOrder(order); //�ֹ� ���
        }
        else
            reaction = CustomerData.GetImage(1); //����

        // ��ȭ �˾� ���ֱ�
        UI_order.gameObject.SetActive(false);

        customer.GetComponent<SpriteRenderer>().sprite = reaction;

        yield return new WaitForSeconds(reactionTime);

        //�� ����
        Managers.Game.Money += pay;

        //�մ� ��Ȱ��ȭ
        customer.gameObject.SetActive(false);
        hasExited = false;
        --Managers.Game.numsOfCurCustomers;
        //Debug.Log($" {gameObject.name} Exit ��");


        //���� �մ�
        StartCoroutine(InstatiateCustomer());
        yield break;

    }

    public void CoInstantiateCustomer()
    {
        StartCoroutine(InstatiateCustomer());
    }

    IEnumerator InstatiateCustomer()
    {
        if (Managers.Game.isClosingTime == true)
            yield break;

        InitCustomer();
        ++Managers.Game.totalCustomers;

        //���� ��� �ð� ���� ����
        float spawnDalayMin = 3f;
        float spawnDalayMax = 8f;

        float spawnDelayTime = UnityEngine.Random.Range(spawnDalayMin, spawnDalayMax);
        //Debug.Log($"1. {spawnDelayTime} �� �� ����");
        spawnDelayTime /= Managers.Game.GameSpeed; //�ð� �ӵ�
        //Debug.Log($"2. {spawnDelayTime} �� �� ����");

        yield return new WaitForSeconds(spawnDelayTime);

        customer.gameObject.SetActive(true);

        yield break;
    }


}
