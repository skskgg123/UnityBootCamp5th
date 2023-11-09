using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    Rigidbody rb;
    CircleCollider2D pokeCollider;

    private List<MouseControllerTest> touched = new List<MouseControllerTest>(); // 충돌한 물체를 담는 리스트

    public bool isMerge;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 다른 포켓몬 오브젝트 받기
        Pokemon pokemon = collision.gameObject.GetComponent<Pokemon>();


    }

}
