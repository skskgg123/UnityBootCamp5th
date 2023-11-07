using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public bool _isPress;
    Rigidbody2D rb;
    CircleCollider2D circleCollider;

    private List<MouseController> _objectCheck = new List<MouseController>();

    public bool _isMerge;
    public int _id;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        _isPress = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ( _isPress)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float leftBorder = -8.7f + transform.localScale.x / 2f;
            float rightBorder = 7.3f + transform.localScale.x / 2f;

            if (mousePos.x < leftBorder)
            {
                mousePos.x = leftBorder;
            }
            else if (mousePos.x > rightBorder)
            {
                mousePos.x = rightBorder;
            }

            mousePos.x = Mathf.Clamp(mousePos.x, leftBorder, rightBorder);
            mousePos.y = 12;
            mousePos.z = 0;
            transform.position = mousePos;

        }
    }

    public void Press()
    {
        _isPress = true;
    }

    public void Put()
    {
        _isPress = false;
        rb.simulated = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        MouseController otherMouse = collision.gameObject.GetComponent<MouseController>();

        if (otherMouse != null && !_isMerge && !otherMouse._isMerge && _id < 7)
        {
            _objectCheck.Add(otherMouse);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        List<MouseController> toRemove = new List<MouseController>();

        for (int i = 0; i < _objectCheck.Count; i++)
        {
            var otherMouse = _objectCheck[i];

            if (_id == otherMouse._id && !_isMerge && !otherMouse._isMerge && _id < 7)
            {
                float meX = transform.position.x;
                float meY = transform.position.y;
                float otherX = otherMouse.transform.position.x;
                float otherY = otherMouse.transform.position.y;

                if (meY < otherY || (meY == otherY && meX < otherX))
                {
                    _isMerge = true;

                    rb.simulated = false;
                    //circleCollider.enabled = false;

                    Destroy(otherMouse.gameObject);
                    Destroy(gameObject);
                    _id++;
                    toRemove.Add(otherMouse);
                }
            }
        }

        foreach (var mouse in toRemove)
        {
            _objectCheck.Remove(mouse);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        MouseController otherMouse = collision.gameObject.GetComponent<MouseController>();

        if (otherMouse != null)
        {
            _objectCheck.Remove(otherMouse);
        }
    }




}
