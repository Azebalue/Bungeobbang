using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    CustomerData CustomerData; //SO ������
    static int level = 1; //�մ� ����
    static int Ex; //���� �մ� ������

    #region ���� ������Ʈ ���� ����
    bool didAppeared = false;
    UI_Order ui_order;
    UI_Order UI_order
    {
        get { 
            if(ui_order == null)
                ui_order = Util.FindObject(gameObject, "UI_Order", true).GetComponent<UI_Order>();

            return ui_order;
        }
    }

    GameObject customer;
    public GameObject Customer
    {
        get
        {
            if(customer == null)
                customer = Util.FindObject(gameObject, "Sprite", true);

            return customer;

        }
    }
    #endregion

    #region �ֹ� ���� ����
    Dictionary<FillingType, int> order = new Dictionary<FillingType, int>(); //�ؾ ����, ����
    static Dictionary<FillingType, int> allOrder 
        = new Dictionary<FillingType, int>(); //�ֹ� ��ü
    int numsOfFishBun; //�ֹ��ϴ� �ػ� ����
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

    //�ؾ �ֹ� ���� ����
    int minFishBun = 1;
    int maxFishBun = 3;

    //�ؾ ���� ���� ����
    const int minOrderType = 1; 
    const int maxOrderType = 3;

    int orderAngryPoint; //�ֹ� ���� �Ҹ���
    public int AngryPoint //���� �Ҹ��� (�ֹ� + ��� �ð�)
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

    #region �ð� ���� ����
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

    void Update()
    {
        if (didAppeared == false)
            return;

        UI_order.slider.value = AngryPoint * 0.01f;

        //�г� �������� 100�� �Ǹ� ȭ��
        if (AngryPoint == 100)
            StartCoroutine(Exit());
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

    }

    public void Order()
    {

        //�� �ߺ� ������ ���� ���� ����Ʈ
        List<int> orderableFillingType = new List<int>();

        //�ֹ� ������ �� ����(orderableFillingType)�� ���� �ʱ�ȭ
        for (int i = 0; i < Managers.Game.CurData.numOfFilling; ++i)
            orderableFillingType.Add(i);

        //1. �ֹ��� �ؾ ����
        NumOfFishBun = UnityEngine.Random.Range(minFishBun, maxFishBun+1);

       //Debug.Log($"[Order]{gameObject.name}�� �ֹ� : �� {NumOfFishBun}��");

        //�ؾ ���� ����*���� 
        for (int fishbun = NumOfFishBun; fishbun > 0;  )
        {
            //���� ����
            int randomIndex = UnityEngine.Random.Range(0, orderableFillingType.Count);
            FillingType fillingType = (FillingType)orderableFillingType[randomIndex];
            orderableFillingType.RemoveAt(randomIndex); //�� �� ����

            //���� ����
            int _numsOfFishBun ; // fillingType������ ��ų �ػ� ����
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
        int perfectPoint = (int) (100 / NumOfFishBun);
        int normalPoint = (int)(perfectPoint * 0.8);
        int disappointPoint = 20;

        //1. ������ �´� �� üũ
        if (order.ContainsKey(filling) == true)
        {
            //�� üũ
            if(baking == QualityStatus.perfect)
                orderAngryPoint -= perfectPoint;
            else
                orderAngryPoint -= normalPoint;

            //order ��ųʸ� ����
            if(--order[filling] == 0)
            {
                Debug.Log("�� ���� �� ����");

                order.Remove(filling); //��ųʸ� ����

                if(order.Count == 0)
                {
                    orderAngryPoint -= normalPoint;
                    StartCoroutine(Exit());
                }
            }

            


        }
        else
        {
            //��ȹ �����..

            //�ٸ� ���� �ָ� �Ҹ� up
            orderAngryPoint += disappointPoint;

            //ȯ������� �ϳ�

        }

        //�ٽ� ����
        UI_order.SetOrderText(order);


    }


    IEnumerator Exit()
    {
        //���� ȿ��
        Sprite reaction;
        if(AngryPoint >= 100)
            reaction = CustomerData.GetImage(2); //�Ҹ���
        else
            reaction = CustomerData.GetImage(1); //����

        // ��ȭ �˾� ���ֱ�
        UI_order.gameObject.SetActive(false);

        customer.GetComponent<SpriteRenderer>().sprite = reaction;


        yield return new WaitForSeconds(reactionTime);

        customer.gameObject.SetActive(false);
        order.Clear();
        didAppeared = false;

        //CoInstantiateCustomer();
        yield break;

    }


    public void CoInstantiateCustomer()
    {
        StartCoroutine(InstatiateCustomer());
    }
    IEnumerator InstatiateCustomer()
    {
        InitCustomer();

        //���� ��� �ð� ���� ����
        float spawnDalayMin = 2f;
        float spawnDalayMax = 8f;

        float spawnDelayTime = UnityEngine.Random.Range(spawnDalayMin, spawnDalayMax);
        spawnDelayTime /= Managers.Game.gameSpeed; //�ð� �ӵ�
        Debug.Log($"{spawnDelayTime}�� �� ����");

        yield return new WaitForSeconds(spawnDelayTime);

        customer.gameObject.SetActive(true);
        Order();
        Debug.Log($"{NumOfFishBun}�� �ֹ�");

        didAppeared = true;
        Debug.Log($"����");

        yield break;
    }

}
