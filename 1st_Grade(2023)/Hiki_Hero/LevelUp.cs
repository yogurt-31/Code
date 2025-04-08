using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    bool isLevelup;
    private void Awake()
    {
        StartCoroutine(LevelUP());
    }

    IEnumerator LevelUP()
    {
        while(true)
        {
            if(GetComponent<Enemy_Stat>())
            {
                if (GetComponent<Enemy_Stat>().HP <= 0f && !isLevelup)
                {
                    isLevelup = true;
                    FindObjectOfType<Player_Stat>().LevelUp();
                    yield return null;
                }
            }
            else if(GetComponent<Spawner>())
            {
                if (GetComponent<Spawner>().HP <= 0f && !isLevelup)
                {
                    print("레뼬업!");
                    isLevelup = true;
                    FindObjectOfType<Player_Stat>().LevelUp();
                    yield return null;
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
