using System;
using Unity.VisualScripting;
using UnityEngine;

public class Managers : MonoBehaviour
{
    //�̱���
    static Managers _instance;
    static GameManagerEx gameManager = new GameManagerEx();
    static ResourceManager resourceManager = new ResourceManager();
    static UIManager uiManager = new UIManager();


    static public Managers Instance { get { Init();  return _instance; } }
    static public GameManagerEx Game { get { return gameManager;  } }
    static public ResourceManager Resource { get { return resourceManager;} }
    static public UIManager UI { get { return uiManager; } }


    #region ����
    public int _gameSpeed = 2; //���� �ӵ�
    public int reactionDelayTime = 1; //�����ϴ� �ӵ�


    #endregion


    void Start()
    {
        Game.InitGame();
    }

    void Update()
    {
        Game.runGame();
    }

    static void Init()
    {
        if(_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            _instance = go.GetOrAddComponent<Managers>();

            //GetOrAddComponent��... ?
        }

    }

}
