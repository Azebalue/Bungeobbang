using System;

[Serializable]
public enum CookingState
{
    None,
    bottomBatter, //�ع��� ����
    filled, //����� ����
    topBatter, //������ ����
    cooking, //���� 1�ܰ�
    cooked, //���� 2�ܰ�(�ϼ���)
}

[Serializable]
public enum FillingType
{
    //�ؾ �� ����
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
    //���� ������ ���� ���� ���� ����
    None,
    insufficient, //����
    perfect, //�Ϻ�
    excessive, //����
}

[Serializable]
public enum CustomerType
{
    //�մ� ����
    JeongHyun,
    HaYoung,
    MiJu,
}

public class Define 
{


    public static string BatterString = "Batter";
    public static string FillingString = "Filling";

}
