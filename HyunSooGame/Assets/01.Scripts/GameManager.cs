using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int level;
    public MouseController _object;
    public GameObject _objectPrefab;
    public Transform _objectTransform;

    private void Start()
    {
        NextObject();
    }

    MouseController GetObject()
    {
        GameObject instant = Instantiate(_objectPrefab, _objectTransform);
        MouseController mouseController = instant.GetComponent<MouseController>();
        return mouseController;
    }

    void NextObject()
    {
        MouseController newObject = GetObject();
        _object = newObject;

        StartCoroutine(WaitNext());
    }

    IEnumerator WaitNext()
    {
        while(_object != null)
        {
            yield return null;
        }

        yield return new WaitForSeconds(1.8f);

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
}
