using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplateController : MonoBehaviour
{
    #region ������ ��ǥ
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

    static int[] numsOfFishBun = new int[Util.GetEnumSize(typeof(FillingType))];
    const int maxAllowedFishBun = 5;

    private void Awake()
    {
        Managers.Game.InitObjAction -= Init;
        Managers.Game.InitObjAction += Init;
    }

    static public void Set(GameObject fishBun)
    {
        FishBunController fbc;
        if (fishBun.TryGetComponent(out fbc) == false)
            return;

        //pos����
        int index = (int)fbc.fillingType;
        Vector3 pos = fillingPos[index];
        pos.y -= 0.2f * numsOfFishBun[index];
        fbc.spawnPos = pos;
        Debug.Log($"{fishBun.name}�� ��ġ: {pos.x}, {pos.y}");

        //sprite_order ����
        fishBun.GetComponent<SpriteRenderer>().sortingOrder = numsOfFishBun[index];
        Debug.Log($"{fishBun.name}�� order: {fishBun.GetComponent<SpriteRenderer>().sortingOrder}");

        ++numsOfFishBun[index];

    }

    void Init()
    {
        for(int i = 0; numsOfFishBun.Length > i; i++)
            numsOfFishBun[i] = 0;
    }

}
