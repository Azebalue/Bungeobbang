using System; //Ÿ�� ����(Type)
using UnityEngine;


public static class Util
{
    //������ ũ�� ��ȯ �޼���
    public static int GetEnumSize(Type enumName)
    {
        return Enum.GetNames(enumName).Length;
    }

    //Ư�� �ν��Ͻ� null���� ����� �޼���
    public static void checkNull(UnityEngine.Object obj)
    {
        if (obj == null)
        {
            Debug.LogWarning($"{obj.name}: NULL");
        }
    }
}
