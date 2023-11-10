using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public bool isDrag;

    MouseController GetObject(int id)
    {
        Vector3 mousePos = new Vector3(0, 12, 0);
        GameObject instant = Instantiate(PokemonAssets.Instance.pokemonList[id], mousePos, Quaternion.identity);
        // 반환 타입이 MouseController이므로 해당 GameObject의 MouseController 컴포넌트를 반환
        return instant.GetComponent<MouseController>();
    }

    private void Start()
    {
        NextObject();
    }

    void Update()
    {
        if (isDrag)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //CreateNewMouse(mousePos);
            float leftBorder = -8.7f + transform.localScale.x / 2f;
            float rightBorder = 7.3f + transform.localScale.x / 2f;

            if (mousePos.x < leftBorder)
            {
                mousePos.x = leftBorder;
            }
            else if (mousePos.x > rightBorder)
            {
                mousePos.x = rightBorder;
            }

            mousePos.x = Mathf.Clamp(mousePos.x, leftBorder, rightBorder);
        }
    }
    public void Press()
    {
        if (PokemonAssets.Instance.pokemonList == null)
            return;
                   
        isDrag = true;
        
    }

    public void Put()
    {
        if (PokemonAssets.Instance.pokemonList == null)
            return;
        isDrag = false;

    }

    void NextObject()
    {
        int randomIndex = Random.Range(0, 4); // 0 이상 3 이하의 랜덤 인덱스 생성
        MouseController newObject = GetObject(randomIndex);

        StartCoroutine(WaitNext());
    }

    IEnumerator WaitNext()
    {
        while (PokemonAssets.Instance.pokemonList != null)
        {
            yield return null;
        }

        yield return new WaitForSeconds(1.2f);

        NextObject();
    }

}
