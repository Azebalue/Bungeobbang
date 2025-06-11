using UnityEngine.UI;
using TMPro;

public class UI_Game : UI_Base
{
    #region 열거형
    enum TMP {
        dayText,
        timeText,
        moneyText,
    }

    enum Btns {
        settingsButton,
        toggleViewButton,
    }

    enum GameObjects
    {

    }
    #endregion

    int minute
    {
        //시간은 10분단위로 표시
        get { return Managers.Game.minute / 10; }
    }

    protected override void Init()
    {
        //바인딩
        Bind<TextMeshProUGUI>(typeof(TMP));
        Bind<Button>(typeof(Btns));

        //데이터
        GetTMP((int)TMP.dayText).text = $"Day {Managers.Game.CurData.day}";
        GetTMP((int)TMP.moneyText).text = $"{Managers.Game.CurData.money.ToString("N0")} 원";
        GetButton((int)Btns.toggleViewButton).gameObject.AddEvent(CameraController.toggleCameraAction);
        GetButton((int)Btns.settingsButton).gameObject.AddEvent(settingsBtnFunc);
    
    
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


}
