using UnityEngine;

public class Managers : MonoBehaviour
{
    //½Ì±ÛÅæ
    static Managers _instance;
    static public Managers Instance { get { return _instance; } }

    static GameManagerEx _gameManager = new GameManagerEx();
    static public GameManagerEx Game { get { return _gameManager;  } }

    static TimeManager timeManager = new TimeManager();
    static public TimeManager Time { get {  return timeManager; } }
    
    static ResourceManager _resourceManager = new ResourceManager();
    static public ResourceManager Resource { get { return _resourceManager;} }
   
    void Start()
    {
        Game.InitGame();
        Time.InitTime();
    }

    void Update()
    {
        Time.SimulateTime();

    }
}
