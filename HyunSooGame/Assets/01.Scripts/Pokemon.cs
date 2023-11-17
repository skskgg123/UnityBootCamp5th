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

    private float _radius;

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



                // ���� ID�� Pokemon ���� 
                Pokemon nextPokemon = Instantiate(PokemonAssets.Instance.GetPokemonById(id), this.transform.position, Quaternion.identity);
                //nextPokemon.rb.gravityScale = 1f;
                //CreateNewPokemon(this.transform.position);
                //FindAndCreateTangent();
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


}
