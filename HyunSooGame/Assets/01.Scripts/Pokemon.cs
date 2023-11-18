using System;
using System.Collections.Generic;
using UnityEngine;


public class Pokemon : MonoBehaviour
{
    public Rigidbody2D rb;
    CircleCollider2D pokeCollider;
    public int id;
    public bool _isDrag;
    public bool _isDrop;

    private float _radius;

    public ParticleSystem _effect;
    public GameObject effectPrefab;

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

        _radius = pokeCollider.radius * Mathf.Max(transform.localScale.x, transform.localScale.y);
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
        if (id == PokemonAssets.Instance.pokemonList.Count - 1)
            return;

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

                // 다음 ID의 Pokemon 생성 
                //float[] result = CreatNextPokemon(meX, meY, _radius, otherX, otherY, otherPokemon._radius);

                //float newX = result[0];
                //float newY = result[1];

                //Pokemon nextPokemon = Instantiate(PokemonAssets.Instance.GetPokemonById(id), new Vector3(newX, newY, 0), Quaternion.identity);

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

    /*private float[] CreatNextPokemon(float x1, float y1, float radius1, float x2, float y2, float radius2)
    {
        float R = 1;
        float[] result = new float[2];

        foreach (var otherPokemon in touched)
        {
            float scale1 = Mathf.Max(transform.localScale.x, transform.localScale.y);
            float scale2 = Mathf.Max(otherPokemon.transform.localScale.x, otherPokemon.transform.localScale.y);

            float root = MathNet.Numerics.RootFinding.NewtonRaphson(x =>
            {
                float eq1 = (x - x1) * (x - x1) + (y1 - 0) * (y1 - 0) - (radius1 * scale1 + R) * (radius1 * scale1 + R);
                float eq2 = (x - x2) * (x - x2) + (y2 - 0) * (y2 - 0) - (radius2 * scale2 + R) * (radius2 * scale2 + R);

                return eq1 - eq2;
            }, 2.0f, 10.0f, 0.0001f);

            float newX = root;
            float newY = Mathf.Sqrt(Mathf.Pow(radius1 * scale1 + R, 2) - Mathf.Pow(newX - x1, 2));

            result[0] = newX;
            result[1] = newY;
        }

        return result;
    }*/
}


