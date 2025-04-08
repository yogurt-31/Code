using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Story_Spawn
{
    Yes,
    No
}
public class Spawner : MonoBehaviour
{
    [SerializeField] Image HP_bar;
    [SerializeField] private GameObject dialogue_Trigger;
    [SerializeField] Transform target;

    public Story_Spawn storySpawn;

    public int enemyCount;
    public int Space_Level;
    public int HP;

    int currentHP = 1000;

    int fullHP;

    public bool isCrash;
    public bool isDeath;
    public bool canCrash;
    public bool ISeeYou;

    private void Awake()
    {
        HP = currentHP * Space_Level;
        fullHP = HP;
    }

    private void Update()
    {
        if (isDeath) return;

        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= 15f)
        {
            ISeeYou = true;
            GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.5f, 0.9f, 1);
        }
        else
        {
            ISeeYou = false;
            GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.5f, 0.9f, 0);
        }

        if (FindObjectOfType<Player_MOVE>().isAtk && isCrash && !canCrash)
        {
            canCrash = true;
            ChangeHP();
        }
        if (!FindObjectOfType<Player_MOVE>().isAtk)
            canCrash = false;

        if (HP <= 0)
        {
            if (Story_Spawn.Yes == storySpawn) dialogue_Trigger.SetActive(true);
            StartCoroutine(Destroy());
        }
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.1f);
        isDeath = true;
        Destroy(gameObject);
    }
    void ChangeHP()
    {
        HP -= FindObjectOfType<Player_Stat>().ATK;
        HP_bar.fillAmount = (float)HP / fullHP;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        print(GetComponent<EnemySpawn>().count);
        if (collision.CompareTag("Player_ATK") && enemyCount >= GetComponent<EnemySpawn>().count)
        {
            isCrash = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isCrash = false;
    }
}
