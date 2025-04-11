using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRay : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    Camera mainCamera;
    private PlayerInteraction _playerInteraction;
    public bool _canTyping;
    public bool isOpen;
    float time = 0f;
    private void Awake()
    {
        mainCamera = Camera.main;
        _playerInteraction = FindObjectOfType<PlayerInteraction>();
    }
    private void Update()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit))
        {
            Interection(hit);
        }
        
        if (Physics.Raycast(ray, out hit, 100f))
        {
            ISeeYou(hit);
        }
    }

    private void Interection(RaycastHit hit)
    {
        if(hit.collider.CompareTag("InteractionObject"))
        {
            var dis = Vector3.Distance(transform.position, hit.collider.transform.position);
            if (hit.collider != null && dis <= 3f)
            {
                _playerInteraction.SetActiveInteractionPanel(true, hit.collider.GetComponent<PlayerCheck>()._interactionText);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    hit.collider.GetComponent<PlayerCheck>().Typing();
                    if (hit.collider.GetComponent<DrawerAnimation>())
                    {
                        hit.collider.GetComponent<DrawerAnimation>().OpenDrawer();
                    }
                }
            }

            if(dis > 3f)
            {
                _playerInteraction.SetActiveInteractionPanel(false);
            }
        }
        else
        {
            _playerInteraction.SetActiveInteractionPanel(false);
        }
    }
    
    private void ISeeYou(RaycastHit hit)
    {
        if (hit.collider.CompareTag("Dad"))
        {
            time += Time.deltaTime;
            if (time >= 3)
            {
                isOpen = true;
                hit.transform.position += Vector3.down;
                hit.transform.GetComponent<AudioSource>().Play();
                var petDoor = FindObjectOfType<PetDoorAnimation>().gameObject;
                petDoor.GetComponent<Animator>().SetBool("IsOpen", true);
                petDoor.GetComponent<AudioSource>().Play();
                petDoor.transform.GetChild(1).gameObject.SetActive(true);
                
            }
        }
    }
}
