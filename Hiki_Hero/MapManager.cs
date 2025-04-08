using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] GameObject playersHome;
    [SerializeField] GameObject ArwelsHome;
    private void Update()
    {
        if(FindObjectOfType<Fade>().fadeOut && FindObjectOfType<Fade>().isOutDoor)
        {
            playersHome.SetActive(false);
            ArwelsHome.SetActive(true);
        }
        else if(FindObjectOfType<Fade>().fadeOut && !FindObjectOfType<Fade>().isOutDoor)
        {
            ArwelsHome.SetActive(false);
            playersHome.SetActive(true);
        }
    }
}
