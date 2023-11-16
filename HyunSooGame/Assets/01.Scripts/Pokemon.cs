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
            float meX = transform.position.x;
            float meY = transform.position.y;
            float otherX = ohterPokemon.transform.position.x;
            float otherY = ohterPokemon.transform.position.y;



            if (meY < otherY || (meY == otherY && meX < otherX))
            {

                isMerge = true;

                rb.simulated = false;
                Destroy(ohterPokemon.gameObject);
                Destroy(gameObject);
                id++;



                // 다음 ID의 Pokemon 생성 
                Pokemon nextPokemon = Instantiate(PokemonAssets.Instance.GetPokemonById(id), this.transform.position, Quaternion.identity);
                //nextPokemon.rb.gravityScale = 1f;
                //CreateNewPokemon(this.transform.position);
                //FindAndCreateTangent();
            }
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
            List<CircleCollider2D> tangentCircles = CalculateExternalTangentCirclesCollider(pokeCollider, otherPokemon.pokeCollider);

            // 외접한 중심원 생성
            CreateCenterPokemonWithExternalTangent(id, otherPokemon.transform.position, tangentCircles[0]);

            // 외접하는 원들 생성
            for (int i = 1; i < tangentCircles.Count; i++)
            {
                CreateNewPokemon(this.transform.position, tangentCircles[i].radius);
            }
        }

        // 리스트에 없는 경우 다음 단계의 포켓몬 생성
        CreateNewPokemon(this.transform.position, pokeCollider.radius);
    }


    private List<CircleCollider2D> CalculateExternalTangentCirclesCollider(CircleCollider2D collider1, CircleCollider2D collider2)
    {
        // 두 원이 외접할 때 외접하는 중심원과 외접하는 원들의 콜라이더를 계산
        List<CircleCollider2D> tangentCircles = new List<CircleCollider2D>();

        float d = Vector2.Distance(collider1.transform.position, collider2.transform.position);

        // 외접하는 중심원의 반지름
        float centerCircleRadius = (collider1.radius * collider2.radius) / (collider1.radius + collider2.radius - d);

        // 외접하는 원들의 반지름
        float tangentCircleRadius1 = (collider1.radius * centerCircleRadius) / (collider1.radius - centerCircleRadius);
        float tangentCircleRadius2 = (collider2.radius * centerCircleRadius) / (collider2.radius - centerCircleRadius);

        tangentCircles.Add(CreateCircleCollider(collider1.transform.position, centerCircleRadius));
        tangentCircles.Add(CreateCircleCollider(collider1.transform.position, tangentCircleRadius1));
        tangentCircles.Add(CreateCircleCollider(collider2.transform.position, tangentCircleRadius2));

        return tangentCircles;
    }

    private void CreateCenterPokemonWithExternalTangent(int newId, Vector2 externalTangentPoint, CircleCollider2D tangentCollider)
    {
        // 새로운 원의 중심을 계산
        Vector2 newPokemonCenter = CalculateNewPokemonCenter(externalTangentPoint, tangentCollider.radius);

        // R의 ID에 해당하는 포켓몬 생성
        Pokemon centerPokemon = Instantiate(PokemonAssets.Instance.GetPokemonById(newId), newPokemonCenter, Quaternion.identity);

        // 중심 포켓몬의 콜라이더를 설정
        //centerPokemon.pokeCollider.radius = tangentCollider.radius;

        // 중심 포켓몬에게 Drag 호출 (원하는 경우 Drag 호출 여부를 조절)
        centerPokemon.Drag();
    }

    private Vector2 CalculateNewPokemonCenter(Vector2 externalTangentPoint, float newRadius)
    {
        // 여기에 원의 중심을 계산하는 방정식 추가
        // 외접한 두 원의 외접점에서 일정 거리만큼 떨어진 위치에 새로운 원을 생성하도록 하였습니다.
        // 원하는 방식에 따라 수정이 필요합니다.
        Vector2 direction = (externalTangentPoint - (Vector2)transform.position).normalized;
        return externalTangentPoint + direction * newRadius;
    }

    private void CreateNewPokemon(Vector2 position, float tangentRadius)
    {
        // 프리팹을 가져옴
        Pokemon newPokemonPrefab = PokemonAssets.Instance.GetPokemonById(id);

        // 프리팹이 존재하면 생성
        if (newPokemonPrefab != null)
        {
            // 새로운 포켓몬 생성
            Pokemon newPokemonObject = Instantiate(newPokemonPrefab, position, Quaternion.identity);
            Pokemon newPokemon = newPokemonObject.GetComponent<Pokemon>();

            // 설정된 반지름으로 Collider 조정
            //newPokemon.pokeCollider.radius = tangentRadius;
        }
        else
        {
            Debug.LogError($"Prefab with id {id} not found.");
        }
    }

    private CircleCollider2D CreateCircleCollider(Vector2 position, float radius)
    {
        // 새로운 콜라이더 생성
        GameObject colliderObject = new GameObject("TangentCollider");
        CircleCollider2D circleCollider = colliderObject.AddComponent<CircleCollider2D>();

        // 위치와 반지름 설정
        circleCollider.transform.position = position;
        circleCollider.radius = radius;

        return circleCollider;
    }
}
