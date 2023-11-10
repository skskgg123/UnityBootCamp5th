using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    Rigidbody2D rb;
    CircleCollider2D pokeCollider;
    public int id;

    //public int Id => _id;
    //[SerializeField] private int _id;

    private List<Pokemon> touched = new List<Pokemon>(); // 충돌한 물체를 담는 리스트

    public bool isMerge;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 다른 포켓몬 오브젝트 받기
        Pokemon ohterPokemon = collision.gameObject.GetComponent<Pokemon>();

        if (ohterPokemon != null && !isMerge && !ohterPokemon.isMerge && id < 11)
        {
            touched.Add(ohterPokemon);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Pokemon ohterPokemon = collision.gameObject.GetComponent<Pokemon>();
        if (ohterPokemon != null && id == ohterPokemon.id && !isMerge)
        {
            isMerge = true;

            rb.simulated = false;
            Destroy(ohterPokemon.gameObject);
            Destroy(gameObject);
            id++;
        }
    }

}
