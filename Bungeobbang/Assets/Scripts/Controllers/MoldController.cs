using UnityEngine;
using UnityEngine.EventSystems;

public class MoldController : MonoBehaviour
    , IPointerClickHandler
{

    bool isFilled = false;
    public bool IsFilled
    {
        set {
            if(value == false)
                Debug.Log($"{gameObject.name} ���� �����");
            isFilled = value; }
    }

    string fishBun = "fishBun";

    void Awake()
    {
        Managers.Game.InitObjAction -= InitMold;
        Managers.Game.InitObjAction += InitMold;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IngredientController.selectedThing == null || isFilled == true)
            return;

        if (isFilled == false)
        {
            InstanciateFishBun();
            isFilled = true;
        }
    }

    void InstanciateFishBun()
    {
        if(IngredientController.selectedThing.CompareTag("kettle"))
        {
            GameObject _fishBun = Managers.Resource.Instantiate($"Prefabs/{fishBun}");
            _fishBun.GetComponent<FishBunController>().Set(transform.position, gameObject);

            //���� ���
            Managers.Game.ingredientCost += Define.BatterCost; //���� ���� 
        }


    }

    void InitMold()
    {
        isFilled = false;

        //�ؾ ��ü ����
        int childCount = transform.childCount;
        if (childCount == 0)
            return;

        for (int i = 0; i < childCount; ++i)
            Destroy(transform.GetChild(i).gameObject);

    }
}

