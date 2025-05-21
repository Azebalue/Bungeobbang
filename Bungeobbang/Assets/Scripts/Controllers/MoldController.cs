using UnityEngine;

public class MoldController : MonoBehaviour
{
    //public static MoldController instance { get; private set; }

    bool isFilled = false;

    void OnMouseDown()
    {

        if (IngredientController.selectedThing == null) 
            return;

        if(isFilled == false)
        {
            InstanciateFishBun();
            isFilled = true;
        }
    }

    void InstanciateFishBun()
    {
        if(IngredientController.selectedThing.CompareTag("kettle"))
        {
            GameObject fishBun = Managers.Resource.Instantiate("fishBun");
            fishBun.GetComponent<FishBun>().Init(transform.position);
        }


    }


}

