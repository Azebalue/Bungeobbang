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
    public GameObject Instantiate(string name)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{name}");

        if (prefab == null)
        {
            Debug.Log($"{name}�� null");
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
}
