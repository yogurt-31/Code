using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadScene("Epilogue");
    }
}
