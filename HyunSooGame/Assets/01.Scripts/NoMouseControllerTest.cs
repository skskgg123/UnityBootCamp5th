/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseControllerTest : MonoBehaviour
{
    public bool _isPress;
    Rigidbody2D rb;
    CircleCollider2D circleCollider;

    

    public MouseControllerTest mousePrefab; // 미리 만들어 둔 MouseController 프리팹
    //private List<MouseController> allObject = new List<MouseController>(); // 모든 프리팹을 저장할 리스트

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
                    touched.Remove(otherMouse); // 충돌이 끝난 물체를 리스트에서 제거
                }
            }
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        MouseControllerTest otherMouse = collision.gameObject.GetComponent<MouseControllerTest>();

        if (otherMouse != null)
        {
            touched.Remove(otherMouse); // 충돌이 끝난 물체를 리스트에서 제거
        }
    }

    private Vector3 CircleTangentPoint(Vector3 position)
    {
        // 내접 지점을 초기화
        Vector3 tangentPoint = Vector3.zero;

        // 가장 큰 반지름 합을 초기화
        float maxRadiusSum = 0;

        foreach (var sub in touched)
        {
            // 두 원의 반지름 합 계산
            float radiusSum = sub.circleCollider.radius + mousePrefab.circleCollider.radius;

            // 기존에 찾은 원들 중에서 가장 큰 반지름 합을 갱신하고
            // 해당 원들의 중심을 평균 내접 지점으로 설정
            if (radiusSum > maxRadiusSum)
            {
                maxRadiusSum = radiusSum;
                tangentPoint = (sub.transform.position + position) / 2f;
            }
        }
j
        // 가장 큰 반지름 합을 가진 원들의 내접 지점을 반환
        return tangentPoint;


    }


    void CreateNewMouse(Vector3 position)
    {
        // 내접 지점 계산
        Vector3 tangentPoint = CircleTangentPoint(position);

        // 내접 지점에 새로운 마우스를 생성
        MouseControllerTest newMouseObject = Instantiate(mousePrefab, tangentPoint, Quaternion.identity);
        MouseControllerTest newMouse = newMouseObject.GetComponent<MouseControllerTest>();

        // 생성된 마우스를 리스트에 추가
        touched.Add(newMouse);

    }*/

