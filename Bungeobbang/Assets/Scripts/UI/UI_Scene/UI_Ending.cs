using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Ending : UI_Base
{
    Image img;
    TextMeshProUGUI TMP;

    string ImagePath;
    string[] lines; 
    int index;

    protected override void Init()
    {
        Debug.Log("UI_Ending-Init");
        img = Util.Find<Image>(gameObject, "Image");
        TMP = Util.Find<TextMeshProUGUI>(gameObject, "Text (TMP)");

        //���
        if (img == null)
            Debug.Log("��� ���̷�");

        img.sprite = Managers.Resource.LoadSprite($"Ending/{ImagePath}");

        //���
        index = 0;
        nextLine();
        gameObject.AddEvent(nextLine);
    }

    public void SetInfo(EndingType Ending)
    {
        Debug.Log($"UI_Ending-SetInfo: {Ending}");

        ImagePath = Define.EndingImagePath[(int)Ending];

        if (Ending == EndingType.Over)
        {
            lines = Define.OverEndingText;
        }
        else if(Ending == EndingType.Normal)
        {
            lines = Define.ClearEndingText;
        }
        else
        {
            lines = Define.ClearEndingText;
        }
    }

    void nextLine()
    {
        //������ ��� ó��
        if (index == (lines.Length-1))
        {
            Managers.Game.QuitGame();
            return;
        }

        TMP.text = lines[++index];
    }
}
