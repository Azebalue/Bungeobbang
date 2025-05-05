using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
   
public class UI_Base : MonoBehaviour
{
    // GameObject를 Type별로 관리
    protected Dictionary<Type, UnityEngine.Object[]> dic = new Dictionary<Type, UnityEngine.Object[]>();

    protected void Start()
    {
        Init();
    }

    #region Init()메서드
    protected void Init()
    {
        Bind();
        Get();
    }

    protected virtual void Bind()
    {

    }
    protected virtual void Get()
    {

    }

    #endregion

    #region Bind계열함수

    //특정 부모 오브젝트 산하의 같은 타입의 자식obj들을 바인딩
    protected void BindChilds<T>(Type enumName, string parentName)
        where T : UnityEngine.Object
    {
        //하이어라키에서 부모를 이름으로 찾기
        GameObject parent = FindObject(ref parentName);

        //찾을 자식 객체 이름을 string배열로 저장
        string[] names = Enum.GetNames(enumName);

        //자식 객체 저장용 배열 생성
        UnityEngine.Object[] obj = new UnityEngine.Object[names.Length]; 

        //자식 객체를 부모 산하에서 탐색해서 배열에 저장
        for(int i = 0; i < names.Length; i++) 
        {

            obj[i] = FindComponent<T>(ref names[i], parent) ; 

            if(obj[i] == null)
            {

            }

        }

        AddDictionaryValue<T>(obj);

    }

    //첫 번째 인수 parentEnum은 이미 바인딩 되어야, 산하의 자식 바인딩 할 수 있음
    protected void BindChild<parentT, childT>(Type parentEnum, string childName)
        where childT : UnityEngine.Object
    {
        UnityEngine.Object[] value = new UnityEngine.Object[Util.GetEnumSize(parentEnum)];

        //부모 오브젝트가 바인딩되어 있지 않은 경우 예외처리
        if (dic.ContainsKey(typeof(parentT)) == false)
        {
            Debug.LogWarning($"{typeof(parentT)}타입이 바인딩 되지 않았음");
            return;
        }

        //각 부모의 자식들 탐색
        for (int i = 0; i < Util.GetEnumSize(parentEnum); ++i)
        {
            GameObject parent = dic[typeof(parentT)][i] as GameObject; //바인딩된 부모 찾기
            UnityEngine.Object child = FindComponent<childT>(ref childName, parent); //부모의 자식 찾기

            value[i] = child; //배열 채우기

        }

        AddDictionaryValue<childT>(value);
    }
    #endregion

    #region Find계열함수

    //게임 오브젝트를 찾아서 반환하는 메서드(탐색할 부모 지정 가능)
    protected GameObject FindObject(ref string target, UnityEngine.Object parent = null)
    {
        //반환할 값
        GameObject go = null;

        //부모 없이 경우, 하이어라키 전체에서 검색
        if(parent == null)
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
                if(child.name == target)
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
    protected T FindComponent<T>(ref string target, UnityEngine.Object parent = null)
        where T : UnityEngine.Object
    {
        T result = null;
        if(parent == null)
        {
            result = FindAnyObjectByType<T>().GetComponent<T>();

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

    //parent 오브젝트 산하에서 childname을 찾아서 반환하는 함수
/*    protected T FindChild<T>(GameObject parent, string childname)
        where T : UnityEngine.Object
    {
*//*        //GameObject pr = parent as GameObject;
        //부모 산하의 모든 자식들을 순차 탐색 
        for (int curChild = 0; curChild < parent.transform.childCount; curChild++)
        {
            Transform child = parent.transform.GetChild(curChild);

            //찾으면 해당 자식 객체 반환
            if (child.name == childname)
            {
                //게임 오브젝트를 찾는 경우
                if (typeof(T) == typeof(GameObject))
                {
                    return child.gameObject as T;
                    return child.gameObject as T;

                }
                else
                {
                    //게임 컴포넌트를 찾는 경우
                    return child.gameObject.GetComponent<T>();
                }
            }
        }*//*

        T result = null;    
        if(typeof(T) == typeof(GameObject))
        {
            result = FindObject(ref childname, parent) as T;
        }
        else
        {
            result = FindCom
        }

        //예외 체크
        Util.checkNull(result);

        return result;
    }*/


    /*    protected void Find(GameObject go, string path)
        {
            string[] tokens = path.Split('/'); //�������� ����

            Transform current = GameObject.Find(tokens[0]).transform;
            if (tokens.Length > 1)
            {
                for (int index = 1; index < tokens.Length; index++)
                {
                    for (int _cur = 0; _cur < current.transform.childCount; _cur++)
                    {

                    }
                }
            }
        }*/

    #endregion

    #region Get계열함수

    //딕셔너리에서 특정 타입(T)의 값인 배열 인덱스에 접근해 게임 오브젝트 반환
    protected T Get<T>(int index) 
        where T : UnityEngine.Object //Unity에서 관리되는 오브젝트만 허용
    {

        if(dic.TryGetValue(typeof(T), out UnityEngine.Object[] value))
        {
            Debug.LogWarning($"{typeof(T)}타입 {index}번째 널이 아니다.");

            if (value[index] == null)
            {
                Debug.LogWarning($"{typeof(T)}타입 value[{index}] 널.");
                return null;
            }
            return value[index]
                as T;
            //주의: as 연산자는 참조타입에만 사용가능함
            //=> T를 참조가능 타입인 UnityEngine.Object으로 제약 걸어줘야 함
        }
        else
        {
            Debug.LogWarning($"{typeof(T)}타입 {index}번째 널이다.");
            return null;
        }


    }

    //자주 사용하는 타입에 대해서... 확장? => 편의성 증진
    protected Image GetImage(int index)
    {
        return Get<Image>(index);
    }
    protected Button GetButton(int index)
    {
        return Get<Button>(index);
    }
    protected TextMeshProUGUI GetTMP(int index)
    {
        //TextMeshProUGUI TMP = Get<TextMeshProUGUI>(index);
        if(Get<TextMeshProUGUI>(index) == null)
        {
            Debug.LogWarning($"NULL : TextMeshProUGUI[{index}]");
            return null;
        }
        return Get<TextMeshProUGUI>(index).GetComponent<TextMeshProUGUI>();
        //return TMP.GetComponent<TextMeshProUGUI>();
    }
    #endregion

    //딕셔너리에 T타입 값배열 저장하는 메서드
    protected void AddDictionaryValue<T>(UnityEngine.Object[] obj)
        //where T : UnityEngine.Object
    {
        //딕셔너리에 추가 [키: 자식 타입, 값: 자식 배열]
        if (dic.ContainsKey(typeof(T)))
        {
            //키가 이미 있기에, 기존 배열 deepCopy&값 추가한 후 배열 등록
            int arrSize = obj.Length + dic[typeof(T)].Length; //새 배열 크기 계산
            UnityEngine.Object[] newArr = new UnityEngine.Object[arrSize]; //새 배열 생성

            //배열 채우기
            for(int i = 0; i < arrSize; ++i)
            {
                UnityEngine.Object element;
                if (i < dic[typeof(T)].Length)
                    element = dic[typeof(T)].GetValue(i) as UnityEngine.Object;
                else
                    element = obj[i];

                newArr[i] = element;
            }

            //딕셔너리에 업데이트한 배열 등록
            dic[typeof(T)] = newArr;

        }
        else
        {
            //키와 값 신규 등록
            dic.Add(typeof(T), obj);
        }


        //디버깅용 코드
        Debug.Log($"딕셔너리 내용");
        for (int i = 0; i < dic[typeof(T)].Length; ++i)
        {
            Debug.Log($"{typeof(T)} : {dic[typeof(T)].GetValue(i)}");
        }
    }

    //특정 UI오브젝트에 상호작용 기능 붙이는 메서드
    protected void AddEvent(GameObject go, EventHandler eventHandler)
    {

    }
}
