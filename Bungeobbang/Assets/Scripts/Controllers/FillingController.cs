using UnityEngine;

public class FillingController : MonoBehaviour
{
    public GameObject filling;
    public static FillingController selectedFilling;

    Vector3 dir = new Vector3(0, 1, 0);
    Vector3 originSpinDir = new Vector3(0, 0, -30);
    Vector3 spinDir = new Vector3(0, 0, -45);
    bool isSelected = false;
    int originSortingOrder;

    private void OnMouseDown()
    {
        if (selectedFilling == null && isSelected == false)
        {
            select();

        }
        else if(isSelected == true)
        {

            deSelect();
        }
    }

    void select()
    {
        transform.position += dir; //���� �ø���
        transform.eulerAngles = spinDir; //�ణ ȸ��
        originSortingOrder = GetComponent<SpriteRenderer>().sortingOrder;
        GetComponent<SpriteRenderer>().sortingOrder = 55;
        selectedFilling = this; //���õ� �ʸ�����
        isSelected = true;
    }

    void deSelect()
    {
        transform.position -= dir; 
        transform.eulerAngles = originSpinDir;
        GetComponent<SpriteRenderer>().sortingOrder = originSortingOrder;
        selectedFilling = null;
        isSelected = false;
    }
}
