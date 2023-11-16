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

    private void FindAndCreateTangent()
    {
        foreach (var otherPokemon in touched)
        {
            List<CircleCollider2D> tangentCircles = CalculateExternalTangentCirclesCollider(pokeCollider, otherPokemon.pokeCollider);

            // ������ �߽ɿ� ����
            CreateCenterPokemonWithExternalTangent(id, otherPokemon.transform.position, tangentCircles[0]);

            // �����ϴ� ���� ����
            for (int i = 1; i < tangentCircles.Count; i++)
            {
                CreateNewPokemon(this.transform.position, tangentCircles[i].radius);
            }
        }

        // ����Ʈ�� ���� ��� ���� �ܰ��� ���ϸ� ����
        CreateNewPokemon(this.transform.position, pokeCollider.radius);
    }


    private List<CircleCollider2D> CalculateExternalTangentCirclesCollider(CircleCollider2D collider1, CircleCollider2D collider2)
    {
        // �� ���� ������ �� �����ϴ� �߽ɿ��� �����ϴ� ������ �ݶ��̴��� ���
        List<CircleCollider2D> tangentCircles = new List<CircleCollider2D>();

        float d = Vector2.Distance(collider1.transform.position, collider2.transform.position);

        // �����ϴ� �߽ɿ��� ������
        float centerCircleRadius = (collider1.radius * collider2.radius) / (collider1.radius + collider2.radius - d);

        // �����ϴ� ������ ������
        float tangentCircleRadius1 = (collider1.radius * centerCircleRadius) / (collider1.radius - centerCircleRadius);
        float tangentCircleRadius2 = (collider2.radius * centerCircleRadius) / (collider2.radius - centerCircleRadius);

        tangentCircles.Add(CreateCircleCollider(collider1.transform.position, centerCircleRadius));
        tangentCircles.Add(CreateCircleCollider(collider1.transform.position, tangentCircleRadius1));
        tangentCircles.Add(CreateCircleCollider(collider2.transform.position, tangentCircleRadius2));

        return tangentCircles;
    }

    private void CreateCenterPokemonWithExternalTangent(int newId, Vector2 externalTangentPoint, CircleCollider2D tangentCollider)
    {
        // ���ο� ���� �߽��� ���
        Vector2 newPokemonCenter = CalculateNewPokemonCenter(externalTangentPoint, tangentCollider.radius);

        // R�� ID�� �ش��ϴ� ���ϸ� ����
        Pokemon centerPokemon = Instantiate(PokemonAssets.Instance.GetPokemonById(newId), newPokemonCenter, Quaternion.identity);

        // �߽� ���ϸ��� �ݶ��̴��� ����
        //centerPokemon.pokeCollider.radius = tangentCollider.radius;

        // �߽� ���ϸ󿡰� Drag ȣ�� (���ϴ� ��� Drag ȣ�� ���θ� ����)
        centerPokemon.Drag();
    }

    private Vector2 CalculateNewPokemonCenter(Vector2 externalTangentPoint, float newRadius)
    {
        // ���⿡ ���� �߽��� ����ϴ� ������ �߰�
        // ������ �� ���� ���������� ���� �Ÿ���ŭ ������ ��ġ�� ���ο� ���� �����ϵ��� �Ͽ����ϴ�.
        // ���ϴ� ��Ŀ� ���� ������ �ʿ��մϴ�.
        Vector2 direction = (externalTangentPoint - (Vector2)transform.position).normalized;
        return externalTangentPoint + direction * newRadius;
    }

    private void CreateNewPokemon(Vector2 position, float tangentRadius)
    {
        // �������� ������
        Pokemon newPokemonPrefab = PokemonAssets.Instance.GetPokemonById(id);

        // �������� �����ϸ� ����
        if (newPokemonPrefab != null)
        {
            // ���ο� ���ϸ� ����
            Pokemon newPokemonObject = Instantiate(newPokemonPrefab, position, Quaternion.identity);
            Pokemon newPokemon = newPokemonObject.GetComponent<Pokemon>();

            // ������ ���������� Collider ����
            //newPokemon.pokeCollider.radius = tangentRadius;
        }
        else
        {
            Debug.LogError($"Prefab with id {id} not found.");
        }
    }

    private CircleCollider2D CreateCircleCollider(Vector2 position, float radius)
    {
        // ���ο� �ݶ��̴� ����
        GameObject colliderObject = new GameObject("TangentCollider");
        CircleCollider2D circleCollider = colliderObject.AddComponent<CircleCollider2D>();

        // ��ġ�� ������ ����
        circleCollider.transform.position = position;
        circleCollider.radius = radius;

        return circleCollider;
    }
}
