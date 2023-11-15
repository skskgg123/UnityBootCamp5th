using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Pokemon last;
    public Vector3 spawnPoint;
    public int id;
    

    private Vector3 mousePos;

    private void Update()
    {
        if (last == null)
        { 
            SpawnNewPokemon();
        }
        
        if (last._isDrag)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            mousePos.y = 12;
            

            if (mousePos.x < last.leftBorder)
            {
                mousePos.x = last.leftBorder;
            }
            else if (mousePos.x > last.rightBorder)
            {
                mousePos.x = last.rightBorder;
            }
            last.transform.position = mousePos;
        }
    }

    private void SpawnNewPokemon()
    {
        id = Random.Range(0, 3);
        last = Instantiate(PokemonAssets.Instance.pokemonList[id], spawnPoint, Quaternion.identity);
        /*last.onStopped = () =>
        {
            last.onStopped = null;

            last = Instantiate(PokemonAssets.Instance.pokemonList[id], spawnPoint, Quaternion.identity);
        };    */
    }

    public void MouseDrag()
    {
        if (last == null)
            return;

        last.Drag();
    }

    public void MouseUp()
    {
        if (last == null)
            return;
        last.Drop();


        last = null;

    }
    
}
