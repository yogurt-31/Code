using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    getObject,
    notGetObject,
    fakeObject,
    imageObject,
    offeringObject,
    eyeTrickObject,
    tvObject,
    doorObject,
}
public class PlayerCheck : MonoBehaviour
{
    public ObjectType _objType;
    [SerializeField] private string _objectName;
    [SerializeField] public string _interactionText;
    [SerializeField] private string _explainText;
    [SerializeField] private int getObjectNumber;
    [SerializeField] private GameObject trickObject;
    [SerializeField] private int[] mat;
    [SerializeField] private PictureMaterial[] _pictureMat;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _getKeyAudio;

    public bool isTyping;
    public bool fakeisRun;
    public bool fakekey;
    public bool imageisRun;
    public bool globeisBroken;
    private DialogueManager _dialogueManager;
    private Inventory _inventory;
    private MeshRenderer _meshRenderer;
    private void Awake()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();
        _inventory = FindObjectOfType<Inventory>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    public void Typing()
    {
        if(!isTyping && _dialogueManager.canTyping)
        {
            isTyping = true;
            StartCoroutine(_dialogueManager.TypingRoutine(_objectName, _explainText, this));
            if (_objectName == "라디오")
            {
                var radio = FindObjectOfType<RadioAudio>();
                if (!radio.isPlaying)
                    radio.PlayAudio();
                else
                    radio.StopAudio();
            }
            if (_objectName is "서랍" && _audioSource)
            {
                _audioSource.Play();
            }
        }
    }
    public void GetObject()
    {
        StartCoroutine(GetOBJ());
    }

    private IEnumerator GetOBJ()
    {
        _inventory.InventoryImageSetActive(getObjectNumber);
        if (_objectName == "열쇠" && _objType == ObjectType.getObject && _audioSource)
        {
            _audioSource.PlayOneShot(_getKeyAudio);
        }

        if (_objectName is "금붕어")
        {
            _audioSource.Play();
        }
        _meshRenderer.enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    public void TrickObject()
    {
        for (int i = 0; i < _pictureMat.Length; i++)
        {
            _pictureMat[i].ChangeMaterial(mat[i]);
        }
        fakeisRun = true;
        fakekey = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        StartCoroutine(Audio());
    }

    private IEnumerator Audio()
    {
        _audioSource.Play();
        yield return new WaitUntil(() => !_audioSource.isPlaying);
        _audioSource.clip = _getKeyAudio;
        _audioSource.Play();
    }

    public void ImageObject()
    {
        StartCoroutine(ChangeImage());
    }

    IEnumerator ChangeImage()
    {
        if(!imageisRun)
        {
            _pictureMat[2].ChangeMaterial(4);
            yield return new WaitForSeconds(3f);
            _pictureMat[2].ChangeMaterial(10);
            for (int i = 0; i < 2; i++)
                _pictureMat[i].GetComponent<Rigidbody>().isKinematic = false;
            _audioSource.Play();
            imageisRun = true;
        }
        yield return null;
    }

    public void OfferingBoxObject()
    {
        if(!globeisBroken)
        {
            globeisBroken = true;
            _explainText = "";
            FindObjectOfType<Globe>().ChangeGlobe();
        }
        else if(_inventory.hasObject[1] && globeisBroken)
        {
            FindObjectOfType<OfferingBox>().MoveDown();
        }
    }

    public void IrisChangeObject(bool active)
    {
        if (trickObject)
        {
            trickObject.SetActive(active);
            if (trickObject.GetComponent<AudioSource>())
            {
                trickObject.GetComponent<AudioSource>().Play();
            }
        }

        Destroy(gameObject);
    }
}
