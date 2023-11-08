using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public bool _isPress;
    Rigidbody2D rb;
    CircleCollider2D circleCollider;

    private List<MouseController> touchedObjects = new List<MouseController>(); // 충돌한 물체를 담는 리스트

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
            touchedObjects.Add(otherMouse); // 충돌한 물체를 리스트에 추가
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
                    touchedObjects.Remove(otherMouse); // 충돌이 끝난 물체를 리스트에서 제거
                }
            }
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        MouseController otherMouse = collision.gameObject.GetComponent<MouseController>();

        if (otherMouse != null)
        {
            touchedObjects.Remove(otherMouse); // 충돌이 끝난 물체를 리스트에서 제거
        }
    }

    private void CircleTangentPoint()
    {
        Vector3 CalculateTangentPoint(List<MouseController> collidedObjects)
        {
            // 내접 지점을 초기화합니다.
            Vector3 tangentPoint = Vector3.zero;

            // 가장 큰 반지름 합을 초기화합니다.
            float maxRadiusSum = 0;

            // 충돌한 원들 중에서 내접할 수 있는 원을 찾아서 반지름 합이 가장 큰 원을 선택합니다.
            foreach (var obj in collidedObjects)
            {
                // obj가 현재 원(this)과 내접 가능하며, 머지되지 않은 상태일 때
                if (obj._id == _id && !obj._isMerge && !_isMerge)
                {
                    // 두 원의 반지름 합을 계산합니다.
                    float radiusSum = obj.circleCollider.radius + circleCollider.radius;

                    // 기존에 찾은 원들 중에서 가장 큰 반지름 합을 갱신하고
                    // 해당 원들의 중심을 평균 내접 지점으로 설정합니다.
                    if (radiusSum > maxRadiusSum)
                    {
                        maxRadiusSum = radiusSum;
                        tangentPoint = (obj.transform.position + transform.position) / 2f;
                    }
                }
            }

            // 가장 큰 반지름 합을 가진 원들의 내접 지점을 반환합니다.
            return tangentPoint;



        }
    }


    void CreateNewMouse()
    {
        void CreateNewMouse(Vector3 position)
        {
            // 내접 지점에 새로운 원을 생성
            GameObject newMouseObject = Instantiate(gameObject, position, Quaternion.identity);
            MouseController newMouse = newMouseObject.GetComponent<MouseController>();
            newMouse._isMerge = false; // 새 원은 머지되지 않음
            newMouse._id = _id; // 같은 ID를 사용

            // 레벨 매니저나 게임 매니저 등에서 새 원을 관리하는 로직을 추가할 수 있음
        }
    }
}
