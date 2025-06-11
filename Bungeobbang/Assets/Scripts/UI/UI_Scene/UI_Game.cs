using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine;

public class UI_Game : UI_Base
{
    #region ���� ���
    enum TMP {
        dayText,
        timeText,
        moneyText,
    }

    enum Btns {
        settingsButton,
        toggleViewButton,
    }

    static GameObject ordersPanel;

    #endregion

    int minute
    {
        //�ð��� 10�д����� ǥ��
        get { return Managers.Game.minute / 10; }
    }

    public static Action orderUpdateAction = null;

    protected override void Init()
    {
        //���ε�
        Bind<TextMeshProUGUI>(typeof(TMP));
        Bind<Button>(typeof(Btns));

        //������
        GetTMP((int)TMP.dayText).text = $"Day {Managers.Game.CurData.day}";
        GetTMP((int)TMP.moneyText).text = $"{Managers.Game.CurData.money.ToString("N0")} ��";
        GetButton((int)Btns.toggleViewButton).gameObject.AddEvent(CameraController.toggleCameraAction);
        GetButton((int)Btns.settingsButton).gameObject.AddEvent(settingsBtnFunc);

        //�̺�Ʈ ����

        orderUpdateAction -= orderUpdate;
        orderUpdateAction += orderUpdate;

        ordersPanel = Util.FindObject(gameObject, "ordersPanel");

    }


    void Update()
    {
        //���� 10�� �����θ� �ٲ�
        GetTMP((int)TMP.timeText).text = ($"{Managers.Game.hour} : {minute}0");
        GetTMP((int)TMP.moneyText).text = ($"{Managers.Game.CurData.money.ToString("N0")} �� ");


    }

    void settingsBtnFunc()
    {
        //Managers.Game.
        //Managers.UI.ShowUI<>();
    }

    static void orderUpdate()
    {
        int numOfPanel = 0;
        GameObject panel; //ordersPanel������ panel

        //�ֹ� ����&����UI ǥ��
        foreach (var order in Managers.Game.Order)
        {
            panel = ordersPanel.transform.GetChild(numOfPanel).gameObject;
            
            panel.SetActive(true);
            panel.transform.GetChild(0).GetComponent<Image>().sprite =
                Managers.Resource.LoadSprite("fillingChunks", (int) order.Key);
            panel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                order.Value.ToString();

            ++numOfPanel;

        }

        //������ ��Ȱ��ȭ
        Util.checkNull(ordersPanel);
        for(int j = numOfPanel;  j < ordersPanel.transform.childCount; ++j)
        {
            panel = ordersPanel.transform.GetChild(j).gameObject;
            panel.SetActive(false);
        }
    }
}
