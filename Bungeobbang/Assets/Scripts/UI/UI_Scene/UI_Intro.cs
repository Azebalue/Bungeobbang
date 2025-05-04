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
    enum Childs
    {
        Text,
    }
    
    void Start()
    {

        BindChilds<Button>(typeof(PButtons), "Btns");

    }

    void Update()
    {
        
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
