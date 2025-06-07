using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Game : UI_Base
{
    #region ������
    enum TMP {
        dayText,
        timeText
    }

    enum Btns
    {
        toggleViewButton
    }
    #endregion

    int minute
    {
        //�ð��� 10�д����� ǥ��
        get { return Managers.Game.minute / 10; }
    }


    protected override void Init()
    {
        //���ε�
        Bind<TextMeshProUGUI>(typeof(TMP));
        Bind<Button>(typeof(Btns));

        //������
        GetTMP((int)TMP.dayText).text = $"Day {Managers.Game.CurData.day}";
        GetButton((int)Btns.toggleViewButton).gameObject.AddEvent(CameraController.toggleCameraAction);
    
    
    }

    private void Update()
    {
        //���� 10�� �����θ� �ٲ�
        GetTMP((int)TMP.timeText).text = ($"{Managers.Game.hour} : {minute}0");


    }

}
