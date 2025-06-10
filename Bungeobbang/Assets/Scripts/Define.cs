using System;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;

[Serializable]
public enum CookingState
{
    None,
    bottomBatter, //밑반죽 부음
    filled, //속재료 넣음
    topBatter, //윗반죽 부음
    cooking, //굽기 1단계
    cooked, //굽기 2단계(완성됨)
}

[Serializable]
public enum FillingType
{
    //붕어빵 맛 종류
    redBean,
    custard,
    nutella,
    creamCheese,
    pizza,
    mint,
    sweetPotato,
    greenTea,
}

[Serializable]
public enum QualityStatus
{
    //조리 정도에 대한 판정 기준 종류
    None,
    insufficient, //부족
    perfect, //완벽
    excessive, //과함
}

[Serializable]
public enum CustomerType
{
    //손님 종류
    JeongHyun,
    HaYoung,
    MiJu,
}

public class Define 
{


    public static string BatterString = "Batter";
    public static string FillingString = "Filling";

    public static int[] FillingPrice=
    {
        500,
        500,
        700,
        800,
        900,
        1000,
        1100,
        1200,


    };

}
