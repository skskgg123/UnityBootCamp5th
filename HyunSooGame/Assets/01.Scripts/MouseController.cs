using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //private Rigidbody2D rb;
    private bool _isDrag;
    private GameObject _dragPrefab;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDrag = true;
        transform.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isDrag)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;

            // ���콺�� Ŭ���� ��ġ�� �������� �̵�
            if (_dragPrefab != null)
            {
                _dragPrefab.transform.position = mousePos;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDrag = false;
    }

    // ���콺�� Ŭ���� �������� ����
    public void SetDraggedPrefab(GameObject prefab)
    {
        _dragPrefab = prefab;
    }

    // �巡�� ���� �������� ����
    public void ResetDraggedPrefab()
    {
        _dragPrefab = null;
    }
}
