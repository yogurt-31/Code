using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTeleport : MonoBehaviour
{
    [SerializeField] private Transform teleport;
    

    public IEnumerator Teleport()
    {
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<Player_MOVE>().transform.position = teleport.position;
    }
}
