using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerTest : MonoBehaviour
{
    
    public MouseControllerTest _object;
    public GameObject[] _objectPrefab;
    //public Transform _objectTransform;
    public List<GameObject> _objectPrefabList;

    private void Start()
    {
        NextObject();
    }

    MouseControllerTest GetObject(int id)
    {
        Vector3 mousePos = new Vector3(0, 12, 0);
        GameObject instant = Instantiate(_objectPrefab[id], mousePos, Quaternion.identity);
        MouseControllerTest mouseController = instant.GetComponent<MouseControllerTest>();
        return mouseController;
    }

    void NextObject()
    {
        int randomIndex = Random.Range(0, 4); // 0 이상 3 이하의 랜덤 인덱스 생성
        MouseControllerTest newObject = GetObject(randomIndex);
        _object = newObject;

        StartCoroutine(WaitNext());
    }

    IEnumerator WaitNext()
    {
        while(_object != null)
        {
            yield return null;
        }

        yield return new WaitForSeconds(1.2f);

        NextObject();
    }

    public void TouchDown()
    {
        if (_object == null)
            return;

        _object.Press();
    }

    public void TouchUp()
    {
        if (_object == null)
            return;

        _object.Put();
        _object = null;
    }

    public void NextMerge(int id)
    {
        if (_object._isMerge)
        {
            Instantiate(_objectPrefab[id]);
        }
    }
}
