using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler
{
    //Ŭ�� �ڵ鷯
    public Action<PointerEventData> OnClickHandler = null;

    public void OnPointerClick(PointerEventData eventData)
    {
         OnClickHandler?.Invoke(eventData); //Ŭ�� �׼� ����
    }
}
