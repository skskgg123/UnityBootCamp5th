using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseController : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    //private Rigidbody2D rb;
    private bool _isDrag;


    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDrag = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDrag = false;
    }
}
