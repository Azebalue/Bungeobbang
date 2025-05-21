using System;
using UnityEngine;

[Serializable]
public class FishBun : MonoBehaviour
{
    private static int counts = 0;
    public GameObject filling;
    public PolygonCollider2D _collider;

    public CookingState state = CookingState.bottomBatter;
    public AmountStatus batterStatus;
    public AmountStatus fillingStatus;
    public AmountStatus warmStatus;

    public int price;
    public Sprite sprite;
    public bool isDraggable = false;

    private void OnMouseDown()
    {
        cooking();
    }

    void cooking()
    {
        switch (state)
        {

            case CookingState.bottomBatter:
                addFilling();
                break;

            case CookingState.filled:
                addBatter();
                break;

            case CookingState.topBatter:
                
                break;
        }

        Debug.Log($"{gameObject.name} ����: {state}");
    }

    public void Init(Vector3 spawnPos)
    {
        gameObject.name = $"{++counts}";
        //��ġ ����
        transform.position = spawnPos;
        transform.localScale = Vector3.one * 0.35f;

        //���� ������Ʈ ����
        filling = Util.FindObject(ref Define.FillingString, gameObject);

        filling.SetActive(false);

        //�̹���
        GetComponent<SpriteRenderer>().sprite = 
            Managers.Resource.LoadSprite("FishBunState_proto", (int)CookingState.bottomBatter);

        //����
        state = CookingState.bottomBatter;
    }

    void addFilling()
    {
        if (IngredientController.selectedThing.CompareTag("filling"))
        {
            //Debug.Log($"{IngredientController.selectedThing.filling} ����� ����");
            filling.SetActive(true);
            filling.GetComponent<SpriteRenderer>().sprite
                = Managers.Resource.LoadSprite("fillingChunks", (int)IngredientController.selectedThing.filling);
            ++state;

        }

    }

    void addBatter()
    {
        if (IngredientController.selectedThing.CompareTag("kettle"))
        {
            Debug.Log("������ ����");
            
            GetComponent<SpriteRenderer>().sprite =
                Managers.Resource.LoadSprite("FishBunState_proto", (int)CookingState.topBatter-1);

            //layer �켱���� ó��
            //�̱���

            //�ؾ ���빰 ���� ó��
/*            SpriteRenderer sr = filling.GetComponent<SpriteRenderer>();
            Color color = sr.color; 
            color.a = 0.5f;
            sr.color = color;*/

            ++state;
        }
    }
}
