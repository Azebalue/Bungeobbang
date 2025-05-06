using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Intro : UI_Base
{

    enum Buttons
    {
        Start,
        Option,
        Collection,
        Quit,
    }

    void Update()
    {
        
    }

    protected override void Bind()
    {
        BindChilds<Button>(typeof(Buttons), "Btns");
        BindChild<Button, TextMeshProUGUI>(typeof(Buttons), "Text");
    }

    protected override void Get()
    {
        string[] BtnNames = Enum.GetNames(typeof(Buttons));
        for(int i = 0; i < BtnNames.Length; i++)
        {
            GetTMP(i).text = BtnNames[i];
            Debug.Log($"{i}:{GetTMP(i).text}:{BtnNames[i]}");

        }

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
