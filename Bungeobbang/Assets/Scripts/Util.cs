using System; //Ÿ�� ����(Type)
using UnityEngine;

public static class Util
{
    //������ ũ�� ��ȯ �޼���
    public static int GetEnumSize(Type enumName)
    {
        return Enum.GetNames(enumName).Length;
    }

    //������ �޼���
    public static bool checkNull(UnityEngine.Object obj)
    {
        if (obj == null)
        {
            Debug.LogWarning($"NULL");
            return true;
        }

        return false;
    }

    //GetComponent ������ �޼���: ������Ʈ ���� ��� ������Ŵ
    public static T GetOrAddComponent<T>(GameObject go)
        where T : Component
    {
        T component = go.GetComponent<T>();

        //���ٸ� ������Ʈ �����ϰ� ��ȯ
        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }

/*    //���ڸ����� ,�� ���� ���·� ��ȯ�ϴ� �޼���
    public static string FormatMoney(int num)
    {
        //���� �˾Ƴ���
        int length = 1;
        for (int i = 1; (num / Mathf.Pow(10, i-1)) > 0; ++i)
        {
            ++length;

            if (i / 3 == 0)
                ++length;

        }
        Debug.Log($"{num}: ���� ���� {length} ");

        //�����ϱ�
        string money;
        for(int i = length; i >= 0; --i)
        {
            money[i] = num;
        }

        return num.ToString();
    }*/
    #region Find�迭�Լ�



    //TŸ���� target�� ��ȯ�ϴ� Find���� �޼���(������Ʈ ��)
    public static T Find<T>(GameObject parent, string target, bool includeInactive = false)
        where T : UnityEngine.Object
    {
        T result = null;

        //_parent�� TŸ�� �ڽ��� T�迭�� �� ����
        T[] childs = parent.GetComponentsInChildren<T>(includeInactive);
        foreach(T child in childs)
        {
            if (child.name == target)
            {
                result = child;
                break;
            }
        }

        return result;
    }

    //���� ������Ʈ�� ã�Ƽ� ��ȯ�ϴ� �޼���
    public static GameObject FindObject(GameObject parent, string target, bool includeInactive = false)
    {
        Transform result = Find<Transform>(parent, target, includeInactive);
        return result.gameObject;
    }

    #endregion


}
