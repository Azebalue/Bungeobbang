using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Store : UI_Base
{
    public Button nextDayButton;

    protected override void Init()
    {
        AddEvent(nextDayButton.gameObject, Managers.Game.InitNextDay);
    }


}
