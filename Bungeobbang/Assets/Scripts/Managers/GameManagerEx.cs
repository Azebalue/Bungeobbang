using System;
using UnityEngine;

[Serializable] 
enum  Collections
{
    
}

[Serializable]
public class GameData
{
    public int day;

    public int money;
    public int gold;
    public int spritPiece;

    //����
    //��� �ر� ����
}

public class GameManagerEx 
{
    GameData gameData = new GameData();
    public GameData curData
    {
        get { return gameData; }
        set { gameData = value; }
    }

    public void InitGame()
    {
        curData.day = 0;
    }


}
