using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyunSoo.Component
{
    public class MyCollider2D : MonoBehaviour
    {

        public Vector2 offset;
        public float radius;
        public bool isColliding;


        private void Update()
        {
            // 다른 MyCollider2D 인스턴스를 얻어옴
            MyCollider2D otherCollider = GetOtherCollider(); // 이 함수를 사용하여 다른 콜라이더를 가져와야 함

            // 다른 콜라이더와 충돌 판정
            isColliding = CheckCollision(otherCollider);
        }

        public bool CheckCollision(MyCollider2D otherCollider)
        {
            // 두 원의 중심 간 거리를 계산
            float distance = Vector2.Distance(offset, otherCollider.offset);
            // 두 원의 반지름을 합한 값
            float totalRadius = radius + otherCollider.radius;
            // 두 원의 중심 간 거리가 반지름 합보다 작거나 같으면 충돌
            bool isColliding = distance <= totalRadius;

            if (isColliding)
            {
                // 이 코드에서 트리거 동작을 수행 (예: 이벤트 발생)
                // 예를 들어, 다른 콜라이더와 충돌 이벤트를 호출할 수 있습니다.
                // 이벤트 호출, 상태 변경 또는 다른 행동을 수행
                TriggerCollision(otherCollider);
            }

            return isColliding;
        }

        private void TriggerCollision(MyCollider2D otherCollider)
        {
            // 이곳에서 트리거 동작을 수행
            // 필요한 작업을 수행
            // 예: 이벤트 호출, 상태 변경, 다른 동작 수행
        }

        private MyCollider2D GetOtherCollider()
        {
            // 다른 게임 오브젝트를 찾아서 MyCollider2D 컴포넌트를 가져옵니다.
            GameObject otherObject = GameObject.Find("OtherObject");

            if(otherObject != null)
            {
                MyCollider2D otherCollider = otherObject.GetComponent<MyCollider2D>();
                return otherCollider;
            }
            else
                return null;
                
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(offset, radius);

        }

    }
}
