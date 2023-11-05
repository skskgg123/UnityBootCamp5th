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
            // �ٸ� MyCollider2D �ν��Ͻ��� ����
            MyCollider2D otherCollider = GetOtherCollider(); // �� �Լ��� ����Ͽ� �ٸ� �ݶ��̴��� �����;� ��

            // �ٸ� �ݶ��̴��� �浹 ����
            isColliding = CheckCollision(otherCollider);
        }

        public bool CheckCollision(MyCollider2D otherCollider)
        {
            // �� ���� �߽� �� �Ÿ��� ���
            float distance = Vector2.Distance(offset, otherCollider.offset);
            // �� ���� �������� ���� ��
            float totalRadius = radius + otherCollider.radius;
            // �� ���� �߽� �� �Ÿ��� ������ �պ��� �۰ų� ������ �浹
            bool isColliding = distance <= totalRadius;

            if (isColliding)
            {
                // �� �ڵ忡�� Ʈ���� ������ ���� (��: �̺�Ʈ �߻�)
                // ���� ���, �ٸ� �ݶ��̴��� �浹 �̺�Ʈ�� ȣ���� �� �ֽ��ϴ�.
                // �̺�Ʈ ȣ��, ���� ���� �Ǵ� �ٸ� �ൿ�� ����
                TriggerCollision(otherCollider);
            }

            return isColliding;
        }

        private void TriggerCollision(MyCollider2D otherCollider)
        {
            // �̰����� Ʈ���� ������ ����
            // �ʿ��� �۾��� ����
            // ��: �̺�Ʈ ȣ��, ���� ����, �ٸ� ���� ����
        }

        private MyCollider2D GetOtherCollider()
        {
            // �ٸ� ���� ������Ʈ�� ã�Ƽ� MyCollider2D ������Ʈ�� �����ɴϴ�.
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
