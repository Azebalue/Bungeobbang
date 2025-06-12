using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Intro : UI_Base
{

    protected override void Init()
    {
/*        BindChilds<Button>(typeof(Buttons), "Btns");
        BindChild<Button, TextMeshProUGUI>(typeof(Buttons), "Text");*/

        //string[] BtnNames = Enum.GetNames(typeof(Buttons));

    }


    #region 버튼 별 메서드

    public void StartBtn()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void SettingBtn()
    {
        
    }

    public void CollectionBtn()
    {

    }


    #endregion
}
