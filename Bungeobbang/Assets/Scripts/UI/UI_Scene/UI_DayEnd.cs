using UnityEngine;
using UnityEngine.UI;

public class UI_DayEnd : UI_Base
{
    enum Btns{
        checkButton,
    }

    protected override void Init()
    {
        Bind<Button>(typeof(Btns));
        GetButton((int)Btns.checkButton).gameObject.AddEvent(checkButtonFunc);

    }

    private void checkButtonFunc()
    {
        Managers.UI.CloseUI();
        Managers.UI.ShowUI<UI_Store>();
    }
}
