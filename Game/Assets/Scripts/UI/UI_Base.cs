using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Base : MonoBehaviour
{
    // GameObject를 Type별로 관리
    protected Dictionary<Type, GameObject[]> dic = new Dictionary<Type, GameObject[]>();
    
    #region Bind계열함수
    //특정 오브젝트 산하의 같은 타입의 obj들을 바인딩
    protected void BindChilds<T>(Type eType, string parentName)
    {
        //하이어라키에서 부모를 이름으로 찾기
        GameObject parent = Find(ref parentName);

        //찾을 자식 객체 이름을 string배열로 저장
        string[] names = Enum.GetNames(eType);

        //자식 객체 저장용 배열 생성
        GameObject[] obj = new GameObject[names.Length]; 

        //자식 객체를 부모 산하에서 탐색해서 배열에 저장
        for(int i = 0; i < names.Length; i++) 
        {
            obj[i] = FindChild(parent, names[i]); 
        }

        //딕셔너리에 추가 [키: 자식 타입, 값: 자식 배열]
        dic.Add(typeof(T), obj); 

    }

    protected void BindChild<T>(string parentName, string childName)
    {

    }
    #endregion

    #region Find계열함수
    //parent 오브젝트 산하에서 childname을 찾아서 반환하는 함수
    protected GameObject FindChild(GameObject parent, string childname)
    {
        //부모 산하의 
        for(int curChild = 0; curChild < parent.transform.childCount; curChild++)
        {
            Transform child = parent.transform.GetChild(curChild);

            if (child.name == childname)
            {
                //Debug.Log($"{childname}을 찾아서 반환.");
                return child.gameObject;
            }
        }

        Debug.LogWarning($"{childname}을 못찾아서 null반환.");
        return null;
    }

    protected GameObject Find(ref string target)
    {
        //하이어라키에서 이름으로 target 찾기
        GameObject go = GameObject.Find($"{target}");

        //못찾은 경우의 예외처리
        if (go == null)
        {
            Debug.LogWarning($"찾으려는 {target}가 null");
            return null;
        }
        else
            return go;

    }
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
        return dic[typeof(T)].GetValue(index) as T;
        //주의: as 연산자는 참조타입에만 사용가능함
        //=> T를 참조가능 타입인 UnityEngine.Object으로 제약 걸어줘야 함
        
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
        return Get<TextMeshProUGUI>(index);
    }
    #endregion

    protected void AddEvent(GameObject go, EventHandler eventHandler)
    {

    }
}
