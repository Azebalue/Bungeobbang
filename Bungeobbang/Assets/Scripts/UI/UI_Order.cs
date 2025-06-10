using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class UI_Order : UI_Base
{
    TextMeshProUGUI orderText;
    public Slider slider;

    protected override void Init()
    {
        // 1. ĵ���� �����̽� ������ ���� �ʱ�ȭ
        SetWorldUI();

        // 2. ���� ����
        orderText = Util.Find<TextMeshProUGUI>(gameObject, "orderText");
        slider = Util.Find<Slider>(gameObject, "slider");

        //slider.gameObject.AddEvent(SetOrder);

        // 3. �մ� �г� ������ �ʱ�ȭ
        slider.value = 0f; 

    }



    public void SetOrderText(Dictionary<FillingType, int> orders)
    {
        //���� �ؽ�Ʈ ���ֱ�
        orderText.text = null;

        foreach (var order in orders)
            orderText.text += $"{Define.FillingText[(int)order.Key]} * {order.Value}�� \n";

    }




}
