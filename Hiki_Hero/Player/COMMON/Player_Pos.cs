using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Pos : MonoBehaviour
{
    GameObject game;
    void Update()
    {
        game = FindObjectOfType<Player_MOVE>().gameObject;

        transform.position = new Vector3(game.transform.position.x, game.transform.position.y, 0f);
    }
}
