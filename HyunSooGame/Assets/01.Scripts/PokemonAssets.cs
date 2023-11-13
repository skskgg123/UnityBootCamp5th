using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PokemonAssets : MonoBehaviour 
{
    private static PokemonAssets _instance;

    public static PokemonAssets Instance => _instance;


    // Dictionary�� ����Ͽ� ID�� Pokemon ��ü�� ã��
    private Dictionary<int, Pokemon> pokemonID = new Dictionary<int, Pokemon>();

    public List<Pokemon> pokemonList = new List<Pokemon>();

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        ToPokemonList();
    }

    public void PokemonRigidbody(int id)
    {
        Pokemon pokemon = GetPokemonById(id);

        if (pokemon != null)
        {
            Rigidbody2D pokemonRigidbody = pokemon.GetComponent<Rigidbody2D>();
            if (pokemonRigidbody != null)
            {
                pokemonRigidbody.simulated = true;
            }
            else
            {
                Debug.LogError("������ٵ� �����ϴ�.");
            }
        }
        else
        {
            Debug.LogError("�ش� ID�� ���ϸ��� �����ϴ�.");
        }
    }

    //��Ǿ ����ؼ� id������ vlaue��ã�� Ű-�� 
    public void ToPokemonList()
    {
        if (pokemonList == null)
        {
            Debug.LogError("pokemonList is null");
            return;
        }

        for (int i = 0; i < pokemonList.Count; i++)
        {
            Pokemon pokemonPrefab = pokemonList[i];

            // �� �����տ� ID ������Ʈ�� �߰��ϰ� ���� �Ҵ�
            Pokemon pokemon = pokemonPrefab.GetComponent<Pokemon>();


            if (pokemon != null)
            {
                pokemon.id = i;

                // ��ųʸ��� ID�� Pokemon ��ü�� �߰�
                pokemonID.Add(i, pokemon);
                
            }
            else
            {
                Debug.LogError(" ���ϸ� ������ ���� " + pokemonPrefab.name);
            }
        }
    }

    // ID�� ����Ͽ� Pokemon ��ü�� ��ȯ�ϴ� �޼���
    public Pokemon GetPokemonById(int id)
    {
        if (pokemonID.TryGetValue(id, out Pokemon pokemon))
        {
            return pokemon;
        }

        // �ش� ID�� �´� Pokemon�� ���� ��� null ��ȯ
        return null;
    }
}
