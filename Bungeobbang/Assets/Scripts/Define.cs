using System;

[Serializable]
public enum CookingState
{
    None,
    bottomBatter, //¹Ø¹ÝÁ× ºÎÀ½
    filled, //¼ÓÀç·á ³ÖÀ½
    topBatter, //À­¹ÝÁ× ºÎÀ½
    cooking, //±Á±â 1´Ü°è
    cooked, //±Á±â 2´Ü°è(¿Ï¼ºµÊ)
}

[Serializable]
public enum FillingType
{
    //ºØ¾î»§ ¸À Á¾·ù
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
    //Á¶¸® Á¤µµ¿¡ ´ëÇÑ ÆÇÁ¤ ±âÁØ Á¾·ù
    None,
    insufficient, //ºÎÁ·
    perfect, //¿Ïº®
    excessive, //°úÇÔ
}

[Serializable]
public enum CustomerType
{
    //¼Õ´Ô Á¾·ù
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
        "ÆÏ ºØ¾î»§",
        "½´Å©¸² ºØ¾î»§",
        "ÃÊÄÚ ºØ¾î»§",
        "Å©¸²Ä¡Áî ºØ¾î»§",
        "ÇÇÀÚ ºØ¾î»§",
        "¹ÎÆ® ºØ¾î»§",
        "°í±¸¸¶ ºØ¾î»§",
        "³ìÂ÷ ºØ¾î»§",
    };

    public static string[] UI_DayEndText =
    {
        "ÇÏ·ç ÆÇ¸Å ºØ¾î»§",
        "ÇÏ·ç ´©Àû ¼Õ´Ô ¼ö",
        "¿À´Ã ¸ÅÃâ",
        "Àç·á ºñ¿ë",
        "¿µ¾÷ÀÌÀÍ",
    };
    public static string[] UI_StoreText =
    {
/*        "»óÁ¡",
        "µ·",*/

        "Àç·á",
        "½ºÅ³",
        "µµ±¸",
        "È«º¸",
    };
}
