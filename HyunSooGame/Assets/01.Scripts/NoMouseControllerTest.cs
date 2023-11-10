/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseControllerTest : MonoBehaviour
{
    public bool _isPress;
    Rigidbody2D rb;
    CircleCollider2D circleCollider;

    

    public MouseControllerTest mousePrefab; // �̸� ����� �� MouseController ������
    //private List<MouseController> allObject = new List<MouseController>(); // ��� �������� ������ ����Ʈ

    //public bool _isMerge;
    public int _id;

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ( _isPress)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //CreateNewMouse(mousePos);
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
        
    }

    /*private void OnCollisionStay2D(Collision2D collision)
    {
        for (int i = 0; i < touched.Count; i++)
        {
            var otherMouse = touched[i];

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
                    touched.Remove(otherMouse); // �浹�� ���� ��ü�� ����Ʈ���� ����
                }
            }
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        MouseControllerTest otherMouse = collision.gameObject.GetComponent<MouseControllerTest>();

        if (otherMouse != null)
        {
            touched.Remove(otherMouse); // �浹�� ���� ��ü�� ����Ʈ���� ����
        }
    }

    private Vector3 CircleTangentPoint(Vector3 position)
    {
        // ���� ������ �ʱ�ȭ
        Vector3 tangentPoint = Vector3.zero;

        // ���� ū ������ ���� �ʱ�ȭ
        float maxRadiusSum = 0;

        foreach (var sub in touched)
        {
            // �� ���� ������ �� ���
            float radiusSum = sub.circleCollider.radius + mousePrefab.circleCollider.radius;

            // ������ ã�� ���� �߿��� ���� ū ������ ���� �����ϰ�
            // �ش� ������ �߽��� ��� ���� �������� ����
            if (radiusSum > maxRadiusSum)
            {
                maxRadiusSum = radiusSum;
                tangentPoint = (sub.transform.position + position) / 2f;
            }
        }
j
        // ���� ū ������ ���� ���� ������ ���� ������ ��ȯ
        return tangentPoint;


    }


    void CreateNewMouse(Vector3 position)
    {
        // ���� ���� ���
        Vector3 tangentPoint = CircleTangentPoint(position);

        // ���� ������ ���ο� ���콺�� ����
        MouseControllerTest newMouseObject = Instantiate(mousePrefab, tangentPoint, Quaternion.identity);
        MouseControllerTest newMouse = newMouseObject.GetComponent<MouseControllerTest>();

        // ������ ���콺�� ����Ʈ�� �߰�
        touched.Add(newMouse);

    }*/

