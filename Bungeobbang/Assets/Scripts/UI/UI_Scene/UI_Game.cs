using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine;
using JetBrains.Annotations;

public class UI_Game : UI_Base
{
    #region 게임 요소
    enum TMP {
        dayText,
        timeText,
        moneyText,
    }

    enum Btns {
        settingsButton,
        toggleViewButton,
    }

    GameObject ordersPanel;
    GameObject[] orders = new GameObject[Managers.Game.CurData.numOfFilling];

    #endregion

    int minute
    {
        //시간은 10분단위로 표시
        get { return Managers.Game.minute / 10; }
    }

    public static Action orderUpdateAction = null;

    protected override void Init()
    {
        //바인딩
        Bind<TextMeshProUGUI>(typeof(TMP));
        Bind<Button>(typeof(Btns));

        ordersPanel = Util.FindObject(gameObject, "ordersPanel");

        //데이터
        GetTMP((int)TMP.dayText).text = $"Day {Managers.Game.CurData.day}";
        GetTMP((int)TMP.moneyText).text = $"{Managers.Game.CurData.money.ToString("N0")} 원";
        GetButton((int)Btns.toggleViewButton).gameObject.AddEvent(CameraController.toggleCameraAction);
        GetButton((int)Btns.settingsButton).gameObject.AddEvent(settingsBtnFunc);

        //이벤트 구독
        orderUpdateAction -= orderUpdate;
        orderUpdateAction += orderUpdate;

        orderUpdateAction.Invoke();


    }

    
    private void Update()
    {
        //분은 10의 단위로만 바꿈
        GetTMP((int)TMP.timeText).text = ($"{Managers.Game.hour} : {minute}0");
        GetTMP((int)TMP.moneyText).text = ($"{Managers.Game.CurData.money.ToString("N0")} 원 ");


    }

    void settingsBtnFunc()
    {
        //Managers.Game.
        //Managers.UI.ShowUI<>();
    }

    public void orderUpdate()
    {
        int i = 0;
        GameObject panel;
        //주믄된 양 표시
        foreach (var order in Managers.Game.order)
        {
            panel = ordersPanel.transform.GetChild(i).gameObject;
            
            panel.SetActive(true);
            panel.transform.GetChild(0).GetComponent<Image>().sprite =
                Managers.Resource.LoadSprite("fillingChunks", (int) order.Key);
            panel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                order.Value.ToString();

            ++i;

        }

        //나머지 비활성화
        for(int j = i;  j < ordersPanel.transform.childCount; ++j)
        {
            panel = ordersPanel.transform.GetChild(i).gameObject;
            panel.SetActive(false);
        }
    }
}
