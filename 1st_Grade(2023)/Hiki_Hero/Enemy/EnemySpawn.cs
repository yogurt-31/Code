using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject enemy_Prefab;
    public float count = 5;
    List<GameObject> pools;

    private void Start()
    {
        pools = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(enemy_Prefab, transform) as GameObject;
            pools.Add(obj);
        }
    }
}
