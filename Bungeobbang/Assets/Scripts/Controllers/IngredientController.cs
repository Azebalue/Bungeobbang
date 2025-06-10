using UnityEngine;

public class IngredientController : MonoBehaviour
{
    public static IngredientController selectedThing;
    [SerializeField] public FillingType filling;

    Vector3 moveDir = new Vector3(0, 1, 0);

    float originZRotation;
    float zRotation = -20;

    Vector3 originScale;

    int originSortingOrder;
    int maxSortingOrder = 10;

    void Awake()
    {
        Managers.Game.InitObjAction -= InitIngredient;
        Managers.Game.InitObjAction += InitIngredient;
    }

    void Start()
    {
        originSortingOrder = GetComponent<SpriteRenderer>().sortingOrder;
        originZRotation = transform.rotation.eulerAngles.z;
        originScale = transform.localScale;
    }

    void OnMouseDown()
    {
        //선택된 이전 물건이 있었으면
        if (selectedThing != null)
            pickDown(); //이전 pickup 물건 내려 놓기

        pickUp();

    }

    //객체 집어 올리는 메서드
    void pickUp()
    {
        transform.position += moveDir; //위로 올리기
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.Euler(0, 0, originZRotation+zRotation); // 비스듬히 회전
        transform.localScale = originScale;

        GetComponent<SpriteRenderer>().sortingOrder = maxSortingOrder;
        selectedThing = this;

    }

    //객체 내려놓는 메서드
    void pickDown()
    {
        selectedThing.transform.position -= moveDir;
        transform.localScale = Vector3.one;
        selectedThing.transform.rotation = Quaternion.Euler(0, 0, selectedThing.originZRotation);
        transform.localScale = originScale;
        selectedThing.GetComponent<SpriteRenderer>().sortingOrder = selectedThing.originSortingOrder;
        selectedThing = null;
    }

    void InitIngredient()
    {
        if(selectedThing != null)
            selectedThing.pickDown();

    }
}
