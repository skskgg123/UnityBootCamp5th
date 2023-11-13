using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PokemonAssets : MonoBehaviour 
{
    private static PokemonAssets _instance;

    public static PokemonAssets Instance => _instance;


    // Dictionary를 사용하여 ID로 Pokemon 객체를 찾음
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
                Debug.LogError("리지드바디가 없습니다.");
            }
        }
        else
        {
            Debug.LogError("해당 ID의 포켓몬이 없습니다.");
        }
    }

    //딕션어리 사옹해서 id값으로 vlaue값찾기 키-값 
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

            // 각 프리팹에 ID 컴포넌트를 추가하고 값을 할당
            Pokemon pokemon = pokemonPrefab.GetComponent<Pokemon>();


            if (pokemon != null)
            {
                pokemon.id = i;

                // 딕셔너리에 ID와 Pokemon 객체를 추가
                pokemonID.Add(i, pokemon);
                
            }
            else
            {
                Debug.LogError(" 포켓몬 도감에 없어 " + pokemonPrefab.name);
            }
        }
    }

    // ID를 사용하여 Pokemon 객체를 반환하는 메서드
    public Pokemon GetPokemonById(int id)
    {
        if (pokemonID.TryGetValue(id, out Pokemon pokemon))
        {
            return pokemon;
        }

        // 해당 ID에 맞는 Pokemon이 없을 경우 null 반환
        return null;
    }
}
