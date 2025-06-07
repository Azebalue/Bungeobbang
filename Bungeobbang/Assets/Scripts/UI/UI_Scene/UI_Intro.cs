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


    #region MainMenu_ButtonMethods

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

    public void QuitBtn()
    {
#if UNITY_EDITOR
        //에디터에서 실행할 때
        UnityEditor.EditorApplication.isPlaying = false; //에디터 실행 중단
#else
        Application.Quit();
#endif
    }

    #endregion
}
