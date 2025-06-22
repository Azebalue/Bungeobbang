public enum DayState
{
    Waiting,
    Opening,
    Running,
    Closing,
    //Finalized,
}

public enum CookingState
{
    None,
    bottomBatter, //�ع��� ����
    filled, //����� ����
    topBatter, //������ ����
    cooking, //���� 1�ܰ�
    cooked, //���� 2�ܰ�(�ϼ���)
}

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

public enum QualityStatus
{
    //���� ������ ���� ���� ���� ����
    None,
    insufficient, //����
    perfect, //�Ϻ�
    excessive, //����
}

public enum CustomerType
{
    //�մ� ����
    JeongHyun,
    HaYoung,
    MiJu,
}

public enum EndingType
{
    //���� ����
    Over, 
    Normal,
    Clear,
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
        "���� �Ǹ��� �ؾ",
        "���� ���� �մ� ��",
        "������ ����",
        "�Ҹ�� ��� ���",
        "������ ��������",
    };
    public static string[] UI_StoreText =
    {
        "���",
        "��ų",
        "����",
        "ȫ��",
    };

    public static string[] UI_Settings =
    {
        "������",
        "���ư���",

    };

    public static string[] EndingImagePath =
    {
        "OverEndingScene",
        "NormalEndingScene",
        "ClearEndingScene",
    };

    public static string[] OverEndingText =
    {
        "�� �� Ʋ�� ���� ����.",
        "��ᵵ, �մԵ�, ����� �������.",
        "�Ϸ��Ϸ� ��Ƽ�� ������ �ᱹ �����.",
        "�̴�� ���� ���ۿ� ������.",
        "�ܿ��� ��ӵ�����, �� ���Դ� �����.",
        "Game Over: [�Ļ� ����]"
    };

    public static string[] NormalEndingText =
    {
        "���� ��, �ͼ��� �Ÿ��� ���� �ݴ´�.",
        "������ �Ȱ�, ������ ����, ������ ������.",
        "ũ�� ����������, ũ�� ���������� �ʾҴ�.",
        "��� ���缭, ���� �ܿ��� ��ٷ�����.",
        "���� �׷� �Ϸ���� ��.",
        "Ending: [����� ������]"
    };

    public static string[] ClearEndingText =
    {
        "�ż��� �ܿ�ٶ� �ӿ����� ������� ���� ����.",
        "����� �ؾ�� ���������� �Ϸ��� ���ο���.",
        "���ϰ��� ����, ���� �ְ�ġ ���.",
        "���� ���Դ� �� �Ÿ��� ��� �� �� ���� �Ǿ���.",
        "�� �ܿ�, ���� �߰ſ��� ����.",
        "Congratulations! [�α� ���� ����]"
    };
}
