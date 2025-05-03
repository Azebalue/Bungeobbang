using UnityEngine;

public class CameraController : MonoBehaviour
{
    float startPos;
    float endPos;
    float gap
    {
        get { return endPos - startPos; }
    }

    [SerializeField] float standard = 100;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition.y;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition.y;
            Debug.Log(gap);

            //ī�޶� �̵�
            if (gap > standard)
            {
                Debug.Log("ī�޶� ����");

            }
            else if (gap < standard)
            {
                Debug.Log("ī�޶� �Ʒ���");


            }
        }


    }
}
