using System; //타입 정보(Type)
using UnityEngine;

public static class Util
{
    //열거자 크기 반환 메서드
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
/*            //지정한 부모 산하에서 순차 탐색
        for (int i = 0; i < parent.transform.childCount; ++i)
        {
            Transform child = _parent.transform.GetChild(i);
            if (child.name == target)
            {
                result = child.gameObject.GetComponent<T>();
                break;
            }
        }*/

        return result;
    }

    //게임 오브젝트를 찾아서 반환하는 메서드
    public static GameObject FindObject(GameObject parent, string target, bool includeInactive = false)
    {
        /*        //반환할 값
                GameObject result = null;
                *//*
                        //부모 없이 경우, 하이어라키 전체에서 검색
                        if (parent == null)
                        {
                            if(includeInActive == true)
                            {

                            }
                            else
                            {
                                //하이어라키에서 이름으로 target 찾기
                                go = GameObject.Find($"{target}");
                            }

                        }
                        //부모 지정한 경우, 부모 산하에서 검색
                        else
                        {*/
        /*

                GameObject[] childs = parent.GetComponentsInChildren<GameObject>(includeInactive);
                //지정한 부모 산하에서 순차 탐색
                for (int i = 0; i < parent.transform.childCount; ++i)
                {
                    Transform child = parent.transform.GetChild(i);
                    if (child.name == target)
                    {
                        result = child.gameObject;
                        break;
                    }
                }

                return result;*/
        Transform result = Find<Transform>(parent, target, includeInactive);
        return result.gameObject;
    }

    #endregion
}
