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
            Debug.LogWarning($"NULL");
        }
    }

    /*    public static T GetOrAddComponent<T>()
            where T : UnityEngine.Object
        {
            //UI_EventHandler evt = go.GetComponent<UI_EventHandler>();
            //return GameObject go;
        }*/

    #region Find계열함수

    //게임 오브젝트를 찾아서 반환하는 메서드(탐색할 부모 지정 가능)
    public static GameObject FindObject(ref string target, UnityEngine.Object parent = null)
    {
        //반환할 값
        GameObject go = null;

        //부모 없이 경우, 하이어라키 전체에서 검색
        if (parent == null)
        {
            //하이어라키에서 이름으로 target 찾기
            go = GameObject.Find($"{target}");
        }
        //부모 지정한 경우, 부모 산하에서 검색
        else
        {
            GameObject _parent = parent as GameObject;

            //null 체크
            Util.checkNull(_parent);

            //지정한 부모 산하에서 순차 탐색
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

    //게임 오브젝트의 컴포넌트를 반환하는 메서드(탐색할 부모 지정 가능)
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

            //null 체크
            Util.checkNull(_parent);

            //지정한 부모 산하에서 순차 탐색
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
