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
