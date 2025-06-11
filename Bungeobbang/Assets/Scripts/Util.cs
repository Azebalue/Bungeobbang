using System; //타입 정보(Type)
using UnityEngine;

public static class Util
{
    //열거형 크기 반환 메서드
    public static int GetEnumSize(Type enumName)
    {
        return Enum.GetNames(enumName).Length;
    }

    //디버깅용 메서드
    public static bool checkNull(UnityEngine.Object obj)
    {
        if (obj == null)
        {
            Debug.LogWarning($"NULL");
            return true;
        }

        return false;
    }

    //GetComponent 랩핑한 메서드: 컴포넌트 없는 경우 부착시킴
    public static T GetOrAddComponent<T>(GameObject go)
        where T : Component
    {
        T component = go.GetComponent<T>();

        //없다면 컴포넌트 부착하고 반환
        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }

/*    //세자리마다 ,를 넣은 형태로 변환하는 메서드
    public static string FormatMoney(int num)
    {
        //길이 알아내기
        int length = 1;
        for (int i = 1; (num / Mathf.Pow(10, i-1)) > 0; ++i)
        {
            ++length;

            if (i / 3 == 0)
                ++length;

        }
        Debug.Log($"{num}: 변형 길이 {length} ");

        //변형하기
        string money;
        for(int i = length; i >= 0; --i)
        {
            money[i] = num;
        }

        return num.ToString();
    }*/
    #region Find계열함수



    //T타입의 target를 반환하는 Find랩핑 메서드(컴포넌트 용)
    public static T Find<T>(GameObject parent, string target, bool includeInactive = false)
        where T : UnityEngine.Object
    {
        T result = null;

        //_parent의 T타입 자식을 T배열에 다 저장
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

    //게임 오브젝트를 찾아서 반환하는 메서드
    public static GameObject FindObject(GameObject parent, string target, bool includeInactive = false)
    {
        Transform result = Find<Transform>(parent, target, includeInactive);
        return result.gameObject;
    }

    #endregion


}
