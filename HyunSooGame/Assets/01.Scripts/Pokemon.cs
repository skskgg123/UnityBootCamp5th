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
        pokeCollider = GetComponent<CircleCollider2D>();
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
            //CreateNewPokemon(this.transform.position);
            FindAndCreateTangent();

        }

        
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 다른 포켓몬 오브젝트 받기
        Pokemon otherPokemon = collision.gameObject.GetComponent<Pokemon>();

        // 콜라이더가 닿지 않은 경우 리스트에서 제거
        if (otherPokemon != null)
        {
            touched.Remove(otherPokemon);
        }
    }

    private void FindAndCreateTangent()
    {
        foreach (var otherPokemon in touched)
        {
            Vector3 tangentPoint;
            float tangentRadius;

            if (CalculateTangentPoint(this, otherPokemon, out tangentPoint, out tangentRadius))
            {
                // 내접 지점에서 새로운 포켓몬 생성
                CreateNewPokemon(tangentPoint, tangentRadius);
            }
        }

        // 리스트에 없는 경우 다음 단계의 포켓몬 생성
        CreateNewPokemon(this.transform.position, pokeCollider.radius);
    }

    private bool CalculateTangentPoint(Pokemon pokemon1, Pokemon pokemon2, out Vector3 tangentPoint, out float tangentRadius)
    {
        tangentPoint = Vector3.zero;
        tangentRadius = 0f;

        float d = Vector3.Distance(pokemon1.transform.position, pokemon2.transform.position);
        float r1 = pokemon1.pokeCollider.radius;
        float r2 = pokemon2.pokeCollider.radius;

        // 두 원이 만나지 않는 경우
        if (d > r1 + r2)
        {
            return false;
        }

        // 두 원이 동심원인 경우
        if (d < Mathf.Abs(r1 - r2))
        {
            return false;
        }

        // 외접 지점과 반지름 계산
        float a = (r1 * r1 + r2 * r2 + d * d) / (2 * d);
        float h = Mathf.Sqrt(r1 * r1 - a * a);

        // 외접 지점 계산
        Vector3 direction = (pokemon2.transform.position - pokemon1.transform.position).normalized;
        tangentPoint = pokemon1.transform.position + direction * a - direction.normalized * h;

        // 외접한 반지름 계산
        tangentRadius = r1 + r2 - a;

        return true;
    }

    void CreateNewPokemon(Vector3 position, float tangentRadius)
    {

        // 프리팹을 가져옴
        Pokemon newPokemonPrefab = PokemonAssets.Instance.GetPokemonById(id);

        // 프리팹이 존재하면 생성
        if (newPokemonPrefab != null)
        {
            // 새로운 포켓몬 생성
            Pokemon newPokemonObject = Instantiate(newPokemonPrefab, position, Quaternion.identity);
            Pokemon newPokemon = newPokemonObject.GetComponent<Pokemon>();

        }
        else
        {
            Debug.LogError($"Prefab with id {id} not found.");
        }

    }
}
