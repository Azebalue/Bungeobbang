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

        Debug.Log($"{gameObject.name} 상태: {state}");
    }

    public void Init(Vector3 spawnPos)
    {
        gameObject.name = $"{++counts}";
        //위치 조정
        transform.position = spawnPos;
        transform.localScale = Vector3.one * 0.35f;

        //산하 오브젝트 정리
        filling = Util.FindObject(ref Define.FillingString, gameObject);

        filling.SetActive(false);

        //이미지
        GetComponent<SpriteRenderer>().sprite = 
            Managers.Resource.LoadSprite("FishBunState_proto", (int)CookingState.bottomBatter);

        //상태
        state = CookingState.bottomBatter;
    }

    void addFilling()
    {
        if (IngredientController.selectedThing.CompareTag("filling"))
        {
            //Debug.Log($"{IngredientController.selectedThing.filling} 속재료 넣음");
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
            Debug.Log("윗반죽 넣음");
            
            GetComponent<SpriteRenderer>().sprite =
                Managers.Resource.LoadSprite("FishBunState_proto", (int)CookingState.topBatter-1);

            //layer 우선순위 처리
            //미구현

            //붕어빵 내용물 투명도 처리
/*            SpriteRenderer sr = filling.GetComponent<SpriteRenderer>();
            Color color = sr.color; 
            color.a = 0.5f;
            sr.color = color;*/

            ++state;
        }
    }
}
