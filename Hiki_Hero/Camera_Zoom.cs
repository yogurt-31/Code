using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera_Zoom : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera main_Camera;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            FindObjectOfType<Mawang>().isPlayer = true;
            StartCoroutine(SmoothZoomout());
        }
    }

    IEnumerator SmoothZoomout()
    {
        print(main_Camera.m_Lens.OrthographicSize);
        while(true)
        {
            main_Camera.m_Lens.OrthographicSize += 0.1f;
            if (main_Camera.m_Lens.OrthographicSize >= 8f) break;
            yield return new WaitForSeconds(0.01f);
        }
        GetComponent<BoxCollider2D>().isTrigger = false;
        yield return null;
    }
}
