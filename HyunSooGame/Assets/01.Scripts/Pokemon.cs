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

    private List<Pokemon> touched = new List<Pokemon>(); // �浹�� ��ü�� ��� ����Ʈ

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
        // ���ν�Ƽ�� y���� 0�� �Ǹ� isStopped�� true�� �����Ͽ� ���ο� ������ ����
        if (rb.velocity.y <= 0f)
        {
            isStopped = true;
        }

        PokemonAssets.Instance.UpdateScoreText();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �ٸ� ���ϸ� ������Ʈ �ޱ�
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


            // ���� ID�� Pokemon ���� 
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
                    //����Ʈ ����
                    GameObject effectObject = Instantiate(effectPrefab, transform.position, Quaternion.identity);
                    ParticleSystem effect = effectObject.GetComponent<ParticleSystem>();
                    effect.Play();

                    Destroy(effectObject, 1f);
                }

                // ���� ID�� Pokemon ���� 
                Pokemon nextPokemon = Instantiate(PokemonAssets.Instance.GetPokemonById(id), this.transform.position, Quaternion.identity);
                //Vector2 newPosition = CreatNextPokemon(transform.position, _radius, ohterPokemon.transform.position, ohterPokemon._radius);
                //Pokemon nextPokemon = Instantiate(PokemonAssets.Instance.GetPokemonById(id), newPosition, Quaternion.identity);

            }

        }



    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // �ٸ� ���ϸ� ������Ʈ �ޱ�
        Pokemon otherPokemon = collision.gameObject.GetComponent<Pokemon>();

        // �ݶ��̴��� ���� ���� ��� ����Ʈ���� ����
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
                Debug.Log("��� ���");
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
        
        
        // touched ����Ʈ���� �������� ū ������� �����Ͽ� �� ���� ���� �����մϴ�.
        var sortedTouched = touched.OrderByDescending(pokemon => pokemon._radius * Mathf.Max(pokemon.transform.localScale.x, pokemon.transform.localScale.y));
        var largestPokemons = sortedTouched.Take(2).ToList();

        // A�� B�� ���õ� �� ���� ���� ���� �������� �����ϴ�.
        float A = largestPokemons[0]._radius * Mathf.Max(largestPokemons[0].transform.localScale.x, largestPokemons[0].transform.localScale.y);
        float B = largestPokemons[1]._radius * Mathf.Max(largestPokemons[1].transform.localScale.x, largestPokemons[1].transform.localScale.y);

        // R�� ���� ������ ���� ���������� �����մϴ�.
        float R = PokemonAssets.Instance.GetPokemonById(id)._radius;

        // A�� B�� ���̰� R���� ������ �������� �ʴ� ����̹Ƿ� ó���մϴ�.
        if (Mathf.Abs(A - B) < R)
        {
            // �������� �ʴ� ��쿡 ���� ó�� (��: �ٸ� ��ġ�� �����ϰų� ���� ó�� ��)
            // ...

            // ó�� �� return�̳� �ٸ� ��ġ�� ���� �� �ֽ��ϴ�.
            return Vector2.zero;
        }

        // ������ ���� ���ų� �ϳ����� ��쿡 ���� ���� ó��
        if (largestPokemons.Count() < 2)
        {
            // ó������ ���ϴ� ��Ȳ�� ���� ���� ó�� (��: �ٸ� ��ġ�� �����ϰų� ���� ó�� ��)
            // ...

            // ó�� �� return�̳� �ٸ� ��ġ�� ���� �� �ֽ��ϴ�.
            return Vector2.zero;
        }

        // �� ���� ���� ������ ã���ϴ�.
        float x1 = center1.x;
        float y1 = center1.y;
        float r1 = A + R;

        float x2 = center2.x;
        float y2 = center2.y;
        float r2 = B + R;

        // ���� ���� ���
        float d = Vector2.Distance(center1, center2);
        float a = (r1 * r1 - r2 * r2 + d * d) / (2 * d);
        float h = Mathf.Sqrt(r1 * r1 - a * a);
        Vector2 p2 = new Vector2(x1 + a * (x2 - x1) / d, y1 + a * (y2 - y1) / d);
        Vector2 intersect1 = new Vector2(p2.x + h * (y2 - y1) / d, p2.y - h * (x2 - x1) / d);
        Vector2 intersect2 = new Vector2(p2.x - h * (y2 - y1) / d, p2.y + h * (x2 - x1) / d);

        // �� ���� �� �ϳ��� ��ȯ�մϴ�. �ʿ信 ���� intersect2�� ����� �� �ֽ��ϴ�.
        return intersect1;

    }*/
}