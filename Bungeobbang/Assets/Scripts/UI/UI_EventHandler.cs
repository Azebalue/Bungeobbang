using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler
{
    //Ŭ�� �ڵ鷯
    public Action OnClickHandler = null;

    /*�Ű� ���� eventData
     * IPointerClickHandler�� �ñ״�ó�� �־ 
     * ������� �ʴ��� �ݵ�� ����
     */
    public void OnPointerClick(PointerEventData eventData)
    {
         OnClickHandler?.Invoke(); //Ŭ�� �׼� ����
    }
}
