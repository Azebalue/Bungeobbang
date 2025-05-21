using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler
{
    //클릭 핸들러
    public Action<PointerEventData> OnClickHandler = null;

    public void OnPointerClick(PointerEventData eventData)
    {
         OnClickHandler?.Invoke(eventData); //클릭 액션 실행
    }
}
