using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Store : UI_Base
{
    #region 화면 요소
    enum Btns
    {
        FillingButton,
        SkillButton,
        ToolButton,
        MarketingButton,
    }

    enum Txts
    {

    }

    Button NextDayButton;
    
    #endregion
    protected override void Init()
    {

        Bind<Button>(typeof(Btns));
        BindChilds<Button, TextMeshProUGUI> (typeof(Btns), "Text (TMP)");
        NextDayButton = Util.Find<Button>(gameObject, "NextDayButton");

        for(int i = 0; i < dic[typeof(TextMeshProUGUI)].Length; ++i)
        {
            GetTMP(i).text = Define.UI_StoreText[i];
        }
        AddEvent(NextDayButton.gameObject, Managers.Game.StartNextDay);

    }

    void Fuc()
    {
        
    }
}
