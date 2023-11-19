using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public Animator deadAnim;
    public string sceneName;

    private void Start()
    {
        deadAnim = GetComponent<Animator>();
        Button button = GetComponent<Button>();
    }

    private void Update()
    {
        if (PokemonAssets.Instance.isOver)
        {
            deadAnim.SetBool("isDead", true);
        }
        else
        {
            deadAnim.SetBool("isDead", false);
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene(sceneName);
        PokemonAssets.Instance.ReStart();

    }
}
