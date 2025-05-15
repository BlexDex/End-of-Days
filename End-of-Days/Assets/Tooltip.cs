using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;


public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler

{

    public string message;

    // private void OnMouseEnter()
    // {
    //     ToolTipManager.Instance.SetAndShowToolTip(message);
    // }

    // private void OnMouseExit()
    // {
    //     ToolTipManager.Instance.HideToolTip();
    // }


   public void OnPointerEnter(PointerEventData pointerEventData)

    {

        ToolTipManager.Instance.SetAndShowToolTip(message);

    }



    public void OnPointerExit(PointerEventData pointerEventData)

    {

        ToolTipManager.Instance.HideToolTip();

    }

}

