using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public MouseController _object;
    public GameObject[] _objectPrefab;
    //public Transform _objectTransform;
    

    private void Start()
    {
        NextObject();
    }

    MouseController GetObject(int id)
    {
        Vector3 mousePos = new Vector3(0, 12, 0);
        GameObject instant = Instantiate(_objectPrefab[id], mousePos, Quaternion.identity);
        MouseController mouseController = instant.GetComponent<MouseController>();
        return mouseController;
    }

    void NextObject()
    {
        int randomIndex = Random.Range(0, 4); // 0 이상 3 이하의 랜덤 인덱스 생성
        MouseController newObject = GetObject(randomIndex);
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
