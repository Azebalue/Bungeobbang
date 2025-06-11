using UnityEngine;

public class ToolController : MonoBehaviour
{
    public static ToolController selectedTool;
    [SerializeField] public FillingType filling;

    Vector3 moveDir = new Vector3(0, 1, 0);

    float originZRotation;
    float zRotation = -20;

    Vector3 originScale;

    int originSortingOrder;
    int maxSortingOrder = 10;

    void Awake()
    {
        Managers.Game.InitAction -= InitIngredient;
        Managers.Game.InitAction += InitIngredient;
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
        if (selectedTool != null)
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
        selectedTool = this;

    }

    //��ü �������� �޼���
    void pickDown()
    {
        selectedTool.transform.position -= moveDir;
        transform.localScale = Vector3.one;
        selectedTool.transform.rotation = Quaternion.Euler(0, 0, selectedTool.originZRotation);
        transform.localScale = originScale;
        selectedTool.GetComponent<SpriteRenderer>().sortingOrder = selectedTool.originSortingOrder;
        selectedTool = null;
    }

    void InitIngredient()
    {
        if(selectedTool != null)
            selectedTool.pickDown();

    }
}
