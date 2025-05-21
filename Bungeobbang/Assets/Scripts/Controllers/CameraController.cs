using UnityEngine;

public class CameraController : MonoBehaviour
{
    bool isUpper = true;
    Vector3 cameraUpPos = new Vector3(0, 4.5f, -10);
    Vector3 cameraDownPos = new Vector3(0, -4.3f, -10);

    private void Start()
    {
        transform.position = cameraUpPos;
    }

    public void toggleCamera()
    {
        if (isUpper == true)
        {
            transform.position = cameraUpPos;
            isUpper = false;
        }
        else
        {
            transform.position = cameraDownPos;
            isUpper = true;
        }
    }
}
