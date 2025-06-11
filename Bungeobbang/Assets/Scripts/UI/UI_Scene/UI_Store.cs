using UnityEngine.UI;
using TMPro;

public class UI_Store : UI_Base
{
    #region ȭ�� ���
    enum Btns
    {
        FillingButton,
        SkillButton,
        ToolButton,
        MarketingButton,
    }

    Button NextDayButton;
    TextMeshProUGUI MoneyNum;
    TextMeshProUGUI MoneyText;
    TextMeshProUGUI TitleText;

    #endregion

    protected override void Init()
    {

        Bind<Button>(typeof(Btns));
        BindChilds<Button, TextMeshProUGUI> (typeof(Btns), "Text (TMP)");

        NextDayButton = Util.Find<Button>(gameObject, "NextDayButton");
        MoneyNum = Util.Find<TextMeshProUGUI>(gameObject, "MoneyNum");
        TitleText = Util.Find<TextMeshProUGUI>(gameObject, "TitleText");
        MoneyText = Util.Find<TextMeshProUGUI>(gameObject, "MoneyText");

        
        for(int i = 0; i < Define.UI_StoreText.Length; ++i)
            GetTMP(i).text = Define.UI_StoreText[i];


        MoneyText.text = "��";
        TitleText.text = "����";
        MoneyNum.text = Managers.Game.CurData.money.ToString("D0")+" ��";

        AddEvent(NextDayButton.gameObject, Managers.Game.StartNextDay);

    }

    void Fuc()
    {
        
    }
}
