using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    CustomerData CustomerData; //SO ������
    static int level = 1; //�մ� ����
    static int Ex; //���� �մ� ������

    UI_Order ui_order;
    UI_Order UI_order
    {
        get { 
            if(ui_order == null)
                ui_order = Util.FindObject(gameObject, "UI_Order", true).GetComponent<UI_Order>();

            return ui_order;
        }
    }

    #region �ֹ� ���� ����
    Dictionary<FillingType, int> order = new Dictionary<FillingType, int>(); //�ؾ ����, ����
    int numsOfFishBun; //�ֹ��ϴ� �ػ� ����
    int numsOfOrderType; //�ֹ��ϴ� �� ���� ����

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

    public event Action customerInitAction;

    void Awake()
    {
        //UI_order ������ ��ġ ����
        // UI_order.gameObject.transform.SetParent(gameObject.transform);
        
        customerInitAction -= InitCustomer;
        customerInitAction += InitCustomer;

        customerInitAction -= Order;
        customerInitAction += Order;

    }

    void Update()
    {
        UI_order.slider.value = AngryPoint * 0.01f;

        //�г� �������� 100�� �Ǹ� ȭ��
        if (AngryPoint == 100)
            StartCoroutine(Exit());
    }

    public void InitCustomer()
    {

        UI_order.gameObject.SetActive(false);

        //1. ������ ���� ���� ���� ����
        startTime = Managers.Game.delta;
        orderAngryPoint = 0;

        //1. �մ� ���� ���� ����
        int customerType = UnityEngine.Random.Range(0, Util.GetEnumSize(typeof(CustomerType)));
        CustomerData = Managers.Resource.LoadCustomerSO((CustomerType)customerType);

        //2. �մ� ��������Ʈ
        GetComponent<SpriteRenderer>().sprite = CustomerData.GetImage();
        //�ݶ��̴� reset
        Destroy(gameObject.GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();

    }

    public void Order()
    {

        //�� �ߺ� ������ ���� ���� ����Ʈ
        List<int> orderableFillingType = new List<int>();

        //�ֹ� ������ �� ����(orderableFillingType)�� ���� �ʱ�ȭ
        for (int i = 0; i < Managers.Game.CurData.numOfFilling; ++i)
            orderableFillingType.Add(i);

        //1. �ֹ��� �ؾ ����
        //Debug.Log($"���� {minFishBun}, �ְ� {maxFishBun+1}");
        numsOfFishBun = UnityEngine.Random.Range(minFishBun, maxFishBun+1);
        //Debug.Log($"�ֹ��� �ؾ {numsOfFishBun}��");

        //�ؾ ���� ����*���� 
        while(numsOfFishBun != 0)
        {
            //���� ����
            int randomIndex = UnityEngine.Random.Range(0, orderableFillingType.Count);
            FillingType fillingType = (FillingType)orderableFillingType[randomIndex];
            orderableFillingType.RemoveAt(randomIndex); //�� �� ����


            //���� ����
            int _numsOfFishBun ; // fillingType������ ��ų �ػ� ����
            //���� �ؾ ���� 1�� �̻��� ������ ����
            if (numsOfFishBun > 1) 
                _numsOfFishBun = UnityEngine.Random.Range(1, numsOfFishBun - 1);
            else 
                _numsOfFishBun = 1;

            Debug.Log($"{fillingType}�� {_numsOfFishBun}��");

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

/*    void Exit()
    {
        int angryPoint = AngryPoint;

        //if ( angryPoint )
        
        // ��ȭ �˾� ���ֱ�
        UI_order.gameObject.SetActive(false);

        //1�� ���� �����
        GetComponent<SpriteRenderer>().sprite = CustomerData.GetImage(1); //����
        gameObject.SetActive(false);
        //StartCoroutine( () => { }, 2);
        //
    }*/

    IEnumerator Exit()
    {
        int angryPoint = AngryPoint;

        // ��ȭ �˾� ���ֱ�
        UI_order.gameObject.SetActive(false);

        //���� ȿ��
        Sprite reaction;
        if(AngryPoint >= 100)
            reaction = CustomerData.GetImage(2); //�Ҹ���
        else
            reaction = CustomerData.GetImage(1); //����


        GetComponent<SpriteRenderer>().sprite = reaction;


        yield return new WaitForSeconds(reactionTime);
        gameObject.SetActive(false);

        yield break;

    }

}
