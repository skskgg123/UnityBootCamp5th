using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public bool _isPress;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
}
