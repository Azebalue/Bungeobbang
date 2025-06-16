using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager 
{
    GameObject scene;
    Stack<GameObject> popup = new Stack<GameObject>();
    int popup_order = 10; //�˾� ���� ���� ��

    //UI������Ʈ���� �θ� �Ǵ� ���� ������ƮUIRoot
    GameObject UIRoot
    {
        get
        {
            //Ž�� �ؼ� �Ҵ�
            GameObject root = GameObject.Find("@UI");

            //������ �����ϰ� �Ҵ�
            if (root == null)
                root = new GameObject { name = "@UI" };

            //��ȯ
            return root;
        }
    }


    //UI�������� �����ϴ� �޼���
    public T ShowUI<T>(bool isScene = true, string name = null,  Transform parent = null)
        where T : UI_Base
    {
        //�̸��� null�̸� Ÿ�� �̸� ä��
        if(string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        //����
        GameObject prefab = Managers.Resource.Instantiate($"Prefabs/UI/{name}");
        //���̾��Ű ��ġ ����
        if (parent == null)
            prefab.transform.SetParent(UIRoot.transform);
        else
            prefab.transform.SetParent(parent);

        //Scene or Popup ����
        if (isScene == true)
        {
            scene = prefab;
        }
        else
        {
            //����
            popup.Push(prefab);
            prefab.GetComponent<Canvas>().sortingOrder
                = ++popup_order;

        }

        return prefab.GetOrAddComponent<T>();
    }

    public void CloseUI(bool isScene = true)
    {
        if(isScene == true)
        {
            Object.Destroy(scene);
        }
        else
        {

            Object.Destroy(popup.Peek());
            --popup_order;
            popup.Pop();

        }
    }
}
