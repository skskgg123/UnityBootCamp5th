using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    Rigidbody rb;
    CircleCollider2D pokeCollider;

    private List<MouseControllerTest> touched = new List<MouseControllerTest>(); // �浹�� ��ü�� ��� ����Ʈ

    public bool isMerge;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �ٸ� ���ϸ� ������Ʈ �ޱ�
        Pokemon pokemon = collision.gameObject.GetComponent<Pokemon>();


    }

}
