using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectBullet : MonoBehaviour
{
    [SerializeField] private Image[] bullet; // ź ����
    [SerializeField] public int[] totalBullet;

    public bool isChange;
    public bool isShooting; // ����... ����� ���ɾ����� ����...
    public float bulletSwapTime;
    public int bulletNumber;

    /*private void Awake()
    {
        totalBullet = new int[5];
    }*/

    private void Start()
    {
        ChangeBulletImage(4); // ������ �� 1�� ź ����.
        isShooting = true;
    }
    void Update()
    {
        Bullet_Select();
    }

    void Bullet_Select() // ź ��ü �Լ�.
    {
        if(!isChange) // ź ��ü�� �� �ٸ� ź���� ��ü�ϴ� ���� ����.
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChangeBulletImage(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ChangeBulletImage(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ChangeBulletImage(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ChangeBulletImage(3);
            }
        }
    }

    void ChangeBulletImage(int num) // UI �̹����� � ź�� �����Ͽ����� ���̰Բ� ��.
    {
        if (num == bulletNumber)
        {
            for (int i = 0; i < bullet.Length; i++)
            {
                bullet[i].color = new Color(1f, 1f, 1f, 1f);
            }
            isShooting= true;
            bulletNumber = 4;
            StartCoroutine(GetComponent<Gun_UI>().Reloading(bulletSwapTime, bulletNumber, false));
            return;
        }
        isChange = true;
        for (int i = 0; i < bullet.Length; i++)
        {
            bullet[i].color = new Color(1f, 1f, 1f, 1f);
        }
        bulletNumber = num;
        if (bulletNumber < bullet.Length) bullet[bulletNumber].color = new Color(0.8f, 0.8f, 0.8f, 1f);
        StartCoroutine(GetComponent<Gun_UI>().Reloading(bulletSwapTime, bulletNumber, false));
        isShooting = false;
    }
}
