using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Pokemon last;
    public Vector3 spawnPoint;
    public int id;

    private void Update()
    {
        if (last == null)
        {
            id = Random.Range(0, 3);
            SpawnNewPokemon();
        }
    }

    private void SpawnNewPokemon()
    {
        last = Instantiate(PokemonAssets.Instance.pokemonList[id], spawnPoint, Quaternion.identity);
        last.onStopped = () =>
        {
            last.onStopped = null;
            last = Instantiate(PokemonAssets.Instance.pokemonList[id], spawnPoint, Quaternion.identity);
        };
    }

}
