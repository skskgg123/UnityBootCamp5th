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

    public List<GameObject> pokemonList = new List<GameObject>();

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

    //��Ǿ ����ؼ� id������ vlaue��ã�� Ű-�� 
    private void ToPokemonList()
    {
        for (int i = 0; i < pokemonList.Count; i++)
        {
            GameObject pokemonPrefab = pokemonList[i];

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
