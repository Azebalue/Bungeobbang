using System; //타입 정보(Type)
using UnityEngine;


public static class Util
{
    //열거자 크기 반환 메서드
    public static int GetEnumSize(Type enumName)
    {
        return Enum.GetNames(enumName).Length;
    }

    //특정 인스턴스 null여부 디버깅 메서드
    public static void checkNull(UnityEngine.Object obj)
    {
        if (obj == null)
        {
            Debug.LogWarning($"{obj.name}: NULL");
        }
    }
}
