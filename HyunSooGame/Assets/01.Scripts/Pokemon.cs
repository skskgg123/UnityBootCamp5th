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
        // ���ν�Ƽ�� y���� 0�� �Ǹ� isStopped�� true�� �����Ͽ� ���ο� ������ ����
        if (rb.velocity.y <= 0f)
        {
            isStopped = true;
        }
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
        Pokemon ohterPokemon = collision.gameObject.GetComponent<Pokemon>();
        if (ohterPokemon != null && id == ohterPokemon.id && !isMerge)
        {
            isMerge = true;

            rb.simulated = false;
            Destroy(ohterPokemon.gameObject);
            Destroy(gameObject);
            id++;

            // ���� ID�� Pokemon ���� 
            //Pokemon nextPokemon = Instantiate(PokemonAssets.Instance.GetPokemonById(id), this.transform.position, Quaternion.identity);
            //nextPokemon.rb.gravityScale = 1f;
            //CreateNewPokemon(this.transform.position);
            FindAndCreateTangent();

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

    private void FindAndCreateTangent()
    {
        foreach (var otherPokemon in touched)
        {
            Vector3 tangentPoint;
            float tangentRadius;

            if (CalculateTangentPoint(this, otherPokemon, out tangentPoint, out tangentRadius))
            {
                // ���� �������� ���ο� ���ϸ� ����
                CreateNewPokemon(tangentPoint, tangentRadius);
            }
        }

        // ����Ʈ�� ���� ��� ���� �ܰ��� ���ϸ� ����
        CreateNewPokemon(this.transform.position, pokeCollider.radius);
    }

    private bool CalculateTangentPoint(Pokemon pokemon1, Pokemon pokemon2, out Vector3 tangentPoint, out float tangentRadius)
    {
        tangentPoint = Vector3.zero;
        tangentRadius = 0f;

        float d = Vector3.Distance(pokemon1.transform.position, pokemon2.transform.position);
        float r1 = pokemon1.pokeCollider.radius;
        float r2 = pokemon2.pokeCollider.radius;

        // �� ���� ������ �ʴ� ���
        if (d > r1 + r2)
        {
            return false;
        }

        // �� ���� ���ɿ��� ���
        if (d < Mathf.Abs(r1 - r2))
        {
            return false;
        }

        // ���� ������ ������ ���
        float a = (r1 * r1 + r2 * r2 + d * d) / (2 * d);
        float h = Mathf.Sqrt(r1 * r1 - a * a);

        // ���� ���� ���
        Vector3 direction = (pokemon2.transform.position - pokemon1.transform.position).normalized;
        tangentPoint = pokemon1.transform.position + direction * a - direction.normalized * h;

        // ������ ������ ���
        tangentRadius = r1 + r2 - a;

        return true;
    }

    void CreateNewPokemon(Vector3 position, float tangentRadius)
    {

        // �������� ������
        Pokemon newPokemonPrefab = PokemonAssets.Instance.GetPokemonById(id);

        // �������� �����ϸ� ����
        if (newPokemonPrefab != null)
        {
            // ���ο� ���ϸ� ����
            Pokemon newPokemonObject = Instantiate(newPokemonPrefab, position, Quaternion.identity);
            Pokemon newPokemon = newPokemonObject.GetComponent<Pokemon>();

        }
        else
        {
            Debug.LogError($"Prefab with id {id} not found.");
        }

    }
}
