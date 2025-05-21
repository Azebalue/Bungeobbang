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
            Debug.LogWarning($"NULL");
        }
    }

    /*    public static T GetOrAddComponent<T>()
            where T : UnityEngine.Object
        {
            //UI_EventHandler evt = go.GetComponent<UI_EventHandler>();
            //return GameObject go;
        }*/

    #region Find�迭�Լ�

    //���� ������Ʈ�� ã�Ƽ� ��ȯ�ϴ� �޼���(Ž���� �θ� ���� ����)
    public static GameObject FindObject(ref string target, UnityEngine.Object parent = null)
    {
        //��ȯ�� ��
        GameObject go = null;

        //�θ� ���� ���, ���̾��Ű ��ü���� �˻�
        if (parent == null)
        {
            //���̾��Ű���� �̸����� target ã��
            go = GameObject.Find($"{target}");
        }
        //�θ� ������ ���, �θ� ���Ͽ��� �˻�
        else
        {
            GameObject _parent = parent as GameObject;

            //null üũ
            Util.checkNull(_parent);

            //������ �θ� ���Ͽ��� ���� Ž��
            for (int i = 0; i < _parent.transform.childCount; ++i)
            {
                Transform child = _parent.transform.GetChild(i);
                if (child.name == target)
                {
                    go = child.gameObject;
                    break;
                }
            }

        }

        Util.checkNull(go);
        return go;

    }

    //���� ������Ʈ�� ������Ʈ�� ��ȯ�ϴ� �޼���(Ž���� �θ� ���� ����)
    public static T FindComponent<T>(ref string target, UnityEngine.Object parent = null)
        where T : UnityEngine.Object
    {
        T result = null;
        if (parent == null)
        {
            result =UnityEngine.Object.FindAnyObjectByType<T>();

        }
        else
        {

            GameObject _parent = parent as GameObject;

            //null üũ
            Util.checkNull(_parent);

            //������ �θ� ���Ͽ��� ���� Ž��
            for (int i = 0; i < _parent.transform.childCount; ++i)
            {
                Transform child = _parent.transform.GetChild(i);
                if (child.name == target)
                {
                    result = child.gameObject.GetComponent<T>();
                    break;
                }
            }


        }

        Util.checkNull(result);
        return result;
    }

    #endregion
}
