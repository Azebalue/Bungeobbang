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

            //카메라 이동
            if (gap > standard)
            {
                Debug.Log("카메라 위로");

            }
            else if (gap < standard)
            {
                Debug.Log("카메라 아래로");


            }
        }


    }
}
