using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MathNet.Numerics;

public class Pokemon : MonoBehaviour
{
    public Rigidbody2D rb;
    CircleCollider2D pokeCollider;
    public int id;
    public bool _isDrag;
    public bool _isDrop;

    private float _radius;

    public int _myScore;

    public ParticleSystem _effect;
    public GameObject effectPrefab;

    [SerializeField] public float leftBorder;
    [SerializeField] public float rightBorder;

    public float deadTime;

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
        MathNet.Numerics.FindRoots.Cubic(1, 1, 1, 1);
        _radius = pokeCollider.radius * Mathf.Max(transform.localScale.x, transform.localScale.y);
    }

    public void Drag()
    {
        _isDrag = true;
        rb.gravityScale = 0.0f;
        pokeCollider.enabled = false;
    }

    public void Drop()
    {
        pokeCollider.enabled = true;
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

        PokemonAssets.Instance.UpdateScoreText();

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
        if (id == PokemonAssets.Instance.pokemonList.Count - 1)
            return;

        Pokemon ohterPokemon = collision.gameObject.GetComponent<Pokemon>();
        if (ohterPokemon != null && id == ohterPokemon.id && !isMerge)
        {
            float meX = transform.position.x;
            float meY = transform.position.y;
            float otherX = ohterPokemon.transform.position.x;
            float otherY = ohterPokemon.transform.position.y;


            // 다음 ID의 Pokemon 생성 
            //Pokemon nextPokemon = Instantiate(PokemonAssets.Instance.GetPokemonById(id), this.transform.position, Quaternion.identity);

            //Vector2 newPosition = CreateNextPokemon(transform.position, _radius, ohterPokemon.transform.position, ohterPokemon._radius);
            //Pokemon nextPokemon = Instantiate(PokemonAssets.Instance.GetPokemonById(id), newPosition, Quaternion.identity);



            //Vector2 newPosition = CreateNextPokemon(transform.position, _radius, ohterPokemon.transform.position, ohterPokemon._radius);
            //Pokemon nextPokemon = Instantiate(PokemonAssets.Instance.GetPokemonById(id), newPosition, Quaternion.identity);

            if (meY < otherY || (meY == otherY && meX < otherX))
            { 

                isMerge = true;

                rb.simulated = false;
                Destroy(ohterPokemon.gameObject);
                Destroy(gameObject);
                id++;
                PokemonAssets.Instance.score += _myScore;


                if (effectPrefab != null)
                {
                    //이펙트 생성
                    GameObject effectObject = Instantiate(effectPrefab, transform.position, Quaternion.identity);
                    ParticleSystem effect = effectObject.GetComponent<ParticleSystem>();
                    effect.Play();

                    Destroy(effectObject, 1f);
                }

                // 다음 ID의 Pokemon 생성 
                Pokemon nextPokemon = Instantiate(PokemonAssets.Instance.GetPokemonById(id), this.transform.position, Quaternion.identity);
                //Vector2 newPosition = CreatNextPokemon(transform.position, _radius, ohterPokemon.transform.position, ohterPokemon._radius);
                //Pokemon nextPokemon = Instantiate(PokemonAssets.Instance.GetPokemonById(id), newPosition, Quaternion.identity);

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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Finish")
        {
            deadTime += Time.deltaTime;

            if(deadTime > 2)
            {
                Debug.Log("경고 경고");
            }
            if(deadTime > 4)
            {
                PokemonAssets.Instance.GameOver();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Finish")
        {
            deadTime = 0;
        }
    }

    /*private Vector2 CreateNextPokemon(Vector2 center1, float radius1, Vector2 center2, float radius2)
    {
        
        
        // touched 리스트에서 반지름이 큰 순서대로 정렬하여 두 개의 원을 선택합니다.
        var sortedTouched = touched.OrderByDescending(pokemon => pokemon._radius * Mathf.Max(pokemon.transform.localScale.x, pokemon.transform.localScale.y));
        var largestPokemons = sortedTouched.Take(2).ToList();

        // A와 B는 선택된 두 개의 원에 대한 반지름을 갖습니다.
        float A = largestPokemons[0]._radius * Mathf.Max(largestPokemons[0].transform.localScale.x, largestPokemons[0].transform.localScale.y);
        float B = largestPokemons[1]._radius * Mathf.Max(largestPokemons[1].transform.localScale.x, largestPokemons[1].transform.localScale.y);

        // R은 새로 생성될 원의 반지름으로 설정합니다.
        float R = PokemonAssets.Instance.GetPokemonById(id)._radius;

        // A와 B의 차이가 R보다 작으면 외접하지 않는 경우이므로 처리합니다.
        if (Mathf.Abs(A - B) < R)
        {
            // 외접하지 않는 경우에 대한 처리 (예: 다른 위치에 생성하거나 예외 처리 등)
            // ...

            // 처리 후 return이나 다른 조치를 취할 수 있습니다.
            return Vector2.zero;
        }

        // 외접한 원이 없거나 하나뿐인 경우에 대한 예외 처리
        if (largestPokemons.Count() < 2)
        {
            // 처리하지 못하는 상황에 대한 예외 처리 (예: 다른 위치에 생성하거나 예외 처리 등)
            // ...

            // 처리 후 return이나 다른 조치를 취할 수 있습니다.
            return Vector2.zero;
        }

        // 두 원의 외접 지점을 찾습니다.
        float x1 = center1.x;
        float y1 = center1.y;
        float r1 = A + R;

        float x2 = center2.x;
        float y2 = center2.y;
        float r2 = B + R;

        // 외접 지점 계산
        float d = Vector2.Distance(center1, center2);
        float a = (r1 * r1 - r2 * r2 + d * d) / (2 * d);
        float h = Mathf.Sqrt(r1 * r1 - a * a);
        Vector2 p2 = new Vector2(x1 + a * (x2 - x1) / d, y1 + a * (y2 - y1) / d);
        Vector2 intersect1 = new Vector2(p2.x + h * (y2 - y1) / d, p2.y - h * (x2 - x1) / d);
        Vector2 intersect2 = new Vector2(p2.x - h * (y2 - y1) / d, p2.y + h * (x2 - x1) / d);

        // 두 지점 중 하나를 반환합니다. 필요에 따라 intersect2를 사용할 수 있습니다.
        return intersect1;

    }*/
}