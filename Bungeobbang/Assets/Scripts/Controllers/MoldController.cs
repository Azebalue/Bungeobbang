using UnityEngine;
using UnityEngine.EventSystems;

public class MoldController : MonoBehaviour
    , IPointerClickHandler
{

    bool isFilled = false;
    public bool IsFilled
    {
        get
        {
            return isFilled;
        }
        set {
/*            if (value == false)
                Debug.Log($"{gameObject.name} ���� �����");*/
            isFilled = value;
        }

    }

    string fishBun = "fishBun";

    void Awake()
    {
        Managers.Game.InitAction -= InitMold;
        Managers.Game.InitAction += InitMold;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Managers.Game.isRunning == false || ToolController.selectedTool == null)
            return;

/*        if (IsFilled == false)
        {
            
            IsFilled = true;
        }*/
        Util.ExecuteOnce(
            () => {
                Debug.Log("����");
                InstanciateFishBun(); },
            ref isFilled, false
            );
    }

    void InstanciateFishBun()
    {
        if(ToolController.selectedTool.CompareTag("kettle"))
        {
            GameObject _fishBun = Managers.Resource.Instantiate($"Prefabs/{fishBun}");
            _fishBun.GetComponent<FishBunController>().Set(transform.position, gameObject);

            //���� ���
            Managers.Game.IngredientCost += Define.BatterCost; //���� ���� 
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

