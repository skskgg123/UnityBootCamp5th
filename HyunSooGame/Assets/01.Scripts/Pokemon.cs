using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    public Rigidbody2D rb;
    CircleCollider2D pokeCollider;
    public int id;
    public bool _isDrag;
    public bool _isDrop;

    [SerializeField] public LayerMask _ground;
    [SerializeField] public LayerMask _otherPokemon;

    [SerializeField] public float leftBorder; 
    [SerializeField] public float rightBorder;

    //public int Id => _id;
    //[SerializeField] private int _id;

    private List<Pokemon> touched = new List<Pokemon>(); // 충돌한 물체를 담는 리스트

    public bool isMerge;

    public bool isStopped
    {
        get => _isStopped;
        set
        {
            _isStopped = value;
            if (_isStopped)
                onStopped?.Invoke();
        }
    }
    private bool _isStopped;
    public Action onStopped;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.gravityScale = 0f;
    }

    public void Drag()
    {
        _isDrag = true;
        rb.gravityScale = 0.0f;
    }

    public void Drop()
    {
        _isDrag = false;
        rb.gravityScale = 1f;
        rb.velocity += new Vector2(0, -30);
    }

    private void Update()
    {
        // 벨로시티의 y값이 0이 되면 isStopped를 true로 설정하여 새로운 프리팹 생성
        if (rb.velocity.y <= 0f)
        {
            isStopped = true;
        }
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

            // 다음 ID의 Pokemon 생성 
            //Pokemon nextPokemon = Instantiate(PokemonAssets.Instance.GetPokemonById(id), this.transform.position, Quaternion.identity);
            //nextPokemon.rb.gravityScale = 1f;
            CreateNewPokemon(transform.position);


        }

        
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }

    private Vector3 FindTangentPoint(Vector3 position)
    {
        Vector3 tangentPoint = Vector3.zero;
        float maxRadiusSum = 0;

        foreach (var otherPokemon in touched)
        {
            if (otherPokemon == null || otherPokemon.pokeCollider == null)
                continue;

            float radiusSum = otherPokemon.pokeCollider.radius + pokeCollider.radius;

            if (radiusSum > maxRadiusSum)
            {
                maxRadiusSum = radiusSum;

                // 두 원의 중점을 구합니다.
                Vector3 center1 = otherPokemon.transform.position;
                Vector3 center2 = position;

                // 내접점을 찾습니다.
                float d = Vector3.Distance(center1, center2);
                float a = (radiusSum - otherPokemon.pokeCollider.radius + pokeCollider.radius) / 2;
                float h = Mathf.Sqrt(radiusSum * radiusSum - a * a);

                // 내접점의 좌표를 계산합니다.
                Vector3 direction = (center2 - center1).normalized;
                tangentPoint = center1 + direction * (a + h);

            }
        }

        return tangentPoint;
    }

    void CreateNewPokemon(Vector3 position)
    {
        // 내접 지점 찾기
        Vector3 tangentPoint = FindTangentPoint(position);

        // 새로운 포켓몬 생성
        Pokemon newPokemonObject = Instantiate(PokemonAssets.Instance.GetPokemonById(id), tangentPoint, Quaternion.identity);
        Pokemon newPokemon = newPokemonObject.GetComponent<Pokemon>();

        // 생성된 포켓몬을 touched 리스트에 추가
        touched.Add(newPokemon);
    }
}
