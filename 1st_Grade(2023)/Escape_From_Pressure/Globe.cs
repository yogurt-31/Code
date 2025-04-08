using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globe : MonoBehaviour
{
    [SerializeField] private GameObject _globe;
    [SerializeField] private GameObject _brokenGlobe;
    public void ChangeGlobe()
    {
        _globe.SetActive(false);
        _brokenGlobe.SetActive(true);
    }
}
