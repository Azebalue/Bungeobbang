using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceManager 
{
    public T Load<T>(string name)
        where T : UnityEngine.Object
    {
        return Resources.Load<T>(name);
    }

    //������ ����&��ȯ �޼���
    public GameObject Instantiate(string path)
    {
        GameObject prefab = Load<GameObject>($"{path}");

        if (prefab == null)
        {
            Debug.Log($"{path}�� null");
            prefab = Load<GameObject>($"nullPrefab");
        }

        //���� ���� Instantiate�� ObjectŬ���� ���� �޼���� ����޼���� ���� ��� ����
        return Object.Instantiate(prefab); 
    }

    //Sprite ��ȯ �޼���
    public Sprite LoadSprite(string path, int? index = null)
    {
        //���� ��������Ʈ�� ���ϴ� ���
        if(index == null)
        {
            Sprite sprite = Load<Sprite>($"Sprites/{path}");
            return sprite;
        }
        //slice�� ��������Ʈ ��Ʈ���� �������� ���
        else
        {
            Sprite[] sprites = Resources.LoadAll<Sprite>($"Sprites/{path}");

            if (sprites == null || sprites.Length == 0)
                Debug.Log($"��������Ʈ��Ʈ X");

            return sprites[(int)index];
        }
        
    }

    public CustomerData LoadCustomerSO(CustomerType customer)
    {
        return Resources.Load<CustomerData>($"Data/So/{customer}");

    }
}
