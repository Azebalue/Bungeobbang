using UnityEngine;

public class CameraController : MonoBehaviour
{
    bool isUpper = true;
    float posY = 4f; //»ó´Ü

    private void Start()
    {
        transform.position = new Vector3(0, posY, -10);
    }

    public void toggleCamera()
    {
        if (isUpper == true)
        {
            transform.position = new Vector3(0, posY, -10);
            isUpper = false;
        }
        else
        {
            transform.position = new Vector3(0, -posY, -10);
            isUpper = true;
        }
    }
}
