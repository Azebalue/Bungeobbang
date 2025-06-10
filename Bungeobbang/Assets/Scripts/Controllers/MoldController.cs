using UnityEngine;
using UnityEngine.EventSystems;

public class MoldController : MonoBehaviour
    , IPointerClickHandler
{

    bool isFilled = false;
    public bool IsFilled
    {
        set { isFilled = value; }
    }

    string fishBun = "fishBun";

    void Awake()
    {
        Managers.Game.InitObjAction -= InitMold;
        Managers.Game.InitObjAction += InitMold;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IngredientController.selectedThing == null)
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
        }


    }

    void InitMold()
    {
        isFilled = false;

        if(transform.childCount > 0) 
            Destroy(transform.GetChild(0).gameObject);

    }
}

