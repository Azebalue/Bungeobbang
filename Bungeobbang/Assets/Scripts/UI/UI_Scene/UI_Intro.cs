using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Intro : UI_Base
{

    enum PButtons
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
        BindChilds<Button>(typeof(PButtons), "Btns");
        BindChild<Button, TextMeshProUGUI>(typeof(PButtons), "Text");
    }

    protected override void Get()
    {
        Debug.Log("°Ù");
        GetTMP(2).text = "settings";
    }

    private void startBtn()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void SettingBtn()
    {
        
    }

    private void CollectionBtn()
    {

    }

    private void QuitBtn()
    {

    }


}
