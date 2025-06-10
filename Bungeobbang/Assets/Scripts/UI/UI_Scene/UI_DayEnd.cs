using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Util;

public class UI_DayEnd : UI_Base
{
    #region ȭ�� ���
    enum Panels
    {
        fishBun,
        customer,
        money,
        stock,
        profit
    }

    TextMeshProUGUI TitleText;
    Button CheckButton;
    #endregion

    string[] result =
    {
        $"{Managers.Game.totalFishBunsSold} ��",
        $"{Managers.Game.totalCustomers} ��",
        $"{Managers.Game.todayRevenue} ��",
        $"{-Managers.Game.IngredientCost} ��",
        $"{Managers.Game.netProfit} ��"
    };

    protected override void Init()
    {

        BindParents<GameObject>(typeof(Panels), "Panel");
        BindChilds< GameObject ,TextMeshProUGUI>(typeof(Panels), "Text (TMP)");
        BindChilds< GameObject, TextMeshProUGUI>(typeof(Panels), "num");
        TitleText = Find<TextMeshProUGUI>(gameObject, "TitleText");
        CheckButton = Find<Button>(gameObject, "CheckButton");


        TitleText.text = $"{Managers.Game.CurData.day} ����";
        int size = GetEnumSize(typeof(Panels));
        for (int index = 0; index < size  * 2; ++index)
        {
            if(index < size)
                GetTMP(index).text = Define.dayEndPanelText[index];
            else
                GetTMP(index).text = result[index - size];

        }




        CheckButton.gameObject.AddEvent(CheckButtonFunc);


    }

    private void CheckButtonFunc()
    {
        Managers.UI.CloseUI();
        Managers.UI.ShowUI<UI_Store>();
    }
}
