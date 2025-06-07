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
            GameObject fishBun = Managers.Resource.Instantiate("Prefabs/fishBun");
            fishBun.GetComponent<FishBunController>().Set(transform.position, gameObject);
        }


    }


}

