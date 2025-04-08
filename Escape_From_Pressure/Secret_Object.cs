using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Secret_Object : MonoBehaviour
{
    [SerializeField] private GameObject _secretObj;
    [SerializeField] private PlayerCheck _playerCheck;

    private void Update()
    {
        if(_playerCheck.fakeisRun)
        {
            _playerCheck.fakeisRun = false;
            _secretObj.SetActive(true);
        }
    }
}
