using System.Collections;
using UnityEngine;
using DG.Tweening;
public class OfferingBox : MonoBehaviour
{
    [SerializeField] private GameObject numberEnvelope;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    void Awake() => StartCoroutine(CoolTimeMove());
    IEnumerator CoolTimeMove()
    {
        yield return new WaitForSeconds(20f);
        transform.DOMoveY(5.7f, 2f);
        _audioSource.PlayOneShot(_audioClip);
    }

    public void MoveDown()
    {
        FindObjectOfType<Inventory>().InventoryImageDestroy(1);
        numberEnvelope.SetActive(true);
        transform.DOMoveY(8f- 5.7f, 2f);
        _audioSource.PlayOneShot(_audioClip);
    }
}