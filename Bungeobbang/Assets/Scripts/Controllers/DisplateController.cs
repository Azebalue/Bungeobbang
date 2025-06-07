using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplateController : MonoBehaviour,
    IEndDragHandler
{
    #region ¸Àº°·Î ÁÂÇ¥
    static float upperY = 1.8f;
    static float lowerY = 0.5f;

    static public Vector3[] fillingPos = new Vector3[]
    {
        new Vector3 (-7.7f,upperY, 0),
        new Vector3 (-6.5f,upperY, 0),
        new Vector3 (-5.3f,upperY, 0),
        new Vector3 (-4.1f,upperY, 0),

        new Vector3 (-7.7f,lowerY, 0),
        new Vector3 (-6.5f,lowerY, 0),
        new Vector3 (-5.3f,lowerY, 0),
        new Vector3 (-4.1f,lowerY, 0),
    };


    #endregion

    int[] numsOfFishBun;
    const int maxAllowedFishBun = 5;
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("µå·¡±× ³õÀ½");
    }

    //static public Vector3 SetPos(FillingType filling, out Vector3 parent)
    static public Vector3 SetPos(FillingType filling)
    {
        //parent = gameObject.transform.position;
        return fillingPos[(int)filling];
    }


}
