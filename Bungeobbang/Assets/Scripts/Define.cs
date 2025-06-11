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

    public static int BatterCost = 100;
    public static float FillingCostRate = 0.2f;

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
    public static string[] FillingText =
    {
        "�� �ؾ",
        "��ũ�� �ؾ",
        "���� �ؾ",
        "ũ��ġ�� �ؾ",
        "���� �ؾ",
        "��Ʈ �ؾ",
        "���� �ؾ",
        "���� �ؾ",
    };

    public static string[] UI_DayEndText =
    {
        "�Ϸ� �Ǹ� �ؾ",
        "�Ϸ� ���� �մ� ��",
        "���� ����",
        "��� ���",
        "��������",
    };
    public static string[] UI_StoreText =
    {
/*        "����",
        "��",*/

        "���",
        "��ų",
        "����",
        "ȫ��",
    };
}
