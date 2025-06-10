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
        //���õ� ���� ������ �־�����
        if (selectedThing != null)
            pickDown(); //���� pickup ���� ���� ����

        pickUp();

    }

    //��ü ���� �ø��� �޼���
    void pickUp()
    {
        transform.position += moveDir; //���� �ø���
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.Euler(0, 0, originZRotation+zRotation); // �񽺵��� ȸ��
        transform.localScale = originScale;

        GetComponent<SpriteRenderer>().sortingOrder = maxSortingOrder;
        selectedThing = this;

    }

    //��ü �������� �޼���
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
