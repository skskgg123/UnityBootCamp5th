using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public bool _isPress;
    Rigidbody2D rb;
    CircleCollider2D circleCollider;

    private List<MouseController> touchedObjects = new List<MouseController>(); // �浹�� ��ü�� ��� ����Ʈ

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
            touchedObjects.Add(otherMouse); // �浹�� ��ü�� ����Ʈ�� �߰�
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        for (int i = 0; i < touchedObjects.Count; i++)
        {
            var otherMouse = touchedObjects[i];

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
                    touchedObjects.Remove(otherMouse); // �浹�� ���� ��ü�� ����Ʈ���� ����
                }
            }
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        MouseController otherMouse = collision.gameObject.GetComponent<MouseController>();

        if (otherMouse != null)
        {
            touchedObjects.Remove(otherMouse); // �浹�� ���� ��ü�� ����Ʈ���� ����
        }
    }

    private void CircleTangentPoint()
    {
        Vector3 CalculateTangentPoint(List<MouseController> collidedObjects)
        {
            // ���� ������ �ʱ�ȭ�մϴ�.
            Vector3 tangentPoint = Vector3.zero;

            // ���� ū ������ ���� �ʱ�ȭ�մϴ�.
            float maxRadiusSum = 0;

            // �浹�� ���� �߿��� ������ �� �ִ� ���� ã�Ƽ� ������ ���� ���� ū ���� �����մϴ�.
            foreach (var obj in collidedObjects)
            {
                // obj�� ���� ��(this)�� ���� �����ϸ�, �������� ���� ������ ��
                if (obj._id == _id && !obj._isMerge && !_isMerge)
                {
                    // �� ���� ������ ���� ����մϴ�.
                    float radiusSum = obj.circleCollider.radius + circleCollider.radius;

                    // ������ ã�� ���� �߿��� ���� ū ������ ���� �����ϰ�
                    // �ش� ������ �߽��� ��� ���� �������� �����մϴ�.
                    if (radiusSum > maxRadiusSum)
                    {
                        maxRadiusSum = radiusSum;
                        tangentPoint = (obj.transform.position + transform.position) / 2f;
                    }
                }
            }

            // ���� ū ������ ���� ���� ������ ���� ������ ��ȯ�մϴ�.
            return tangentPoint;



        }
    }


    void CreateNewMouse()
    {
        void CreateNewMouse(Vector3 position)
        {
            // ���� ������ ���ο� ���� ����
            GameObject newMouseObject = Instantiate(gameObject, position, Quaternion.identity);
            MouseController newMouse = newMouseObject.GetComponent<MouseController>();
            newMouse._isMerge = false; // �� ���� �������� ����
            newMouse._id = _id; // ���� ID�� ���

            // ���� �Ŵ����� ���� �Ŵ��� ��� �� ���� �����ϴ� ������ �߰��� �� ����
        }
    }
}
