using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PokemonAssets : MonoBehaviour 
{
    private static PokemonAssets _instance;

    public static PokemonAssets Instance => _instance;

    // UI Text ������Ʈ
    public TextMeshProUGUI scoreText;

    //���� ����
    public int score = 0;

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

    public void UpdateScoreText()
    {
        // UI Text�� ���� ���� ǥ��
        if (scoreText != null)
        {
            scoreText.text =  score.ToString();
        }
    }

    public bool isOver = false;

    public void GameOver()
    {
        if (isOver)
            return;

        isOver = true;

        StartCoroutine("GameOverRoutine");
    }

    IEnumerator GameOverRoutine()
    {
        Pokemon[] currentPokemon = FindObjectsOfType<Pokemon>();

        for (int i = 0; i < currentPokemon.Length; i++)
        {
            currentPokemon[i].rb.simulated = false;
        }

        for (int i = 0; i < currentPokemon.Length; i++)
        {
            Destroy(currentPokemon[i].gameObject);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void ReStart()
    {
        score = 0;
        isOver = false;
    }

}
