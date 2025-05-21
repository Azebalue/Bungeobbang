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
    Filling filling; //��
    public float bakingTime; //���� ���� �ð� ����
    public int price; //����

}

