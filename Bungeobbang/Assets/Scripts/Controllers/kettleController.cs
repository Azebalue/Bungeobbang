using UnityEngine;

public class kettleController : MonoBehaviour
{
    bool isSelected = false;

    Vector3 moveDir = new Vector3(0, 2, 0);

    //Vector3 originSpinDir = new Vector3(0, 0, -30);
    Vector3 spinDir = new Vector3(0, 0, 45);

    int originSortingOrder;
    int maxSortingOrder = 10;

    private void Start()
    {
        originSortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

    }
    private void OnMouseDown()
    {
        if (isSelected == false)
        {
            selectKettle();

        }
        else if(isSelected == true)
        {
            deSelectKettle();
        }
    }

    void selectKettle()
    {
        transform.position += moveDir; //위로 올리기
        transform.eulerAngles = spinDir; //약간 회전
        GetComponent<SpriteRenderer>().sortingOrder = maxSortingOrder;
        isSelected = true;
    }

    void deSelectKettle()
    {
        transform.position -= moveDir; 
        transform.eulerAngles = new Vector3(0,0,0);
        GetComponent<SpriteRenderer>().sortingOrder = originSortingOrder;
        isSelected = false;
    }
}
