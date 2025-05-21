using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum Filling
{
    None,   
    redBean,
    custard,
    creamCheese,
    pizza,
    greenTea,
    nutella,
    mint,
    sweetPotato,
}


[Serializable]
public class FishBunData 
{
    Filling filling; //소
    public float bakingTime; //굽기 적정 시간 범위
    public int price; //가격

}

