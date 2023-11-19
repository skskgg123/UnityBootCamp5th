using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene : MonoBehaviour
{
    public string sceneName;

    private void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(NextScene);
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene(sceneName);
        PokemonAssets.Instance.ReStart();

    }
}
