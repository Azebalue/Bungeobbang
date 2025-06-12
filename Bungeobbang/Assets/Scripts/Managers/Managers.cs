using System;
using UnityEngine;
using UnityEngine.UIElements;

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
    public float _gameSpeed = 1.8f; //���� �ӵ�
    public int reactionDelayTime = 1; //�����ϴ� �ӵ�


    #endregion


    void Start()
    {
        Game.InitGame();
    }

    void Update()
    {
        Game.OnUpdate();
    }

    static void Init()
    {
        if(_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            _instance = Util.GetOrAddComponent<Managers>(go);

        }

    }

}
