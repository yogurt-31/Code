using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player_MOVE : MonoBehaviour
{
    bool isDash;
    float x;
    float y;

    public Action playerMove;
    public bool isAtk;
    public bool isMoving;
    public bool neverMove; // 움직이면 안디~
    public float speed;
    public float defaultSpeed;
    public float dashSpeed;

    public int isFlip = 1;

    [SerializeField] Player_UI player_UI;

    Rigidbody2D rigid;
    TrailRenderer trail;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        trail = GetComponentInChildren<TrailRenderer>();
        player_UI = FindObjectOfType<Player_UI>();
        trail.emitting = false;
        playerMove += Move;

    }
    private void FixedUpdate() => playerMove.Invoke();

    private void OnEnable()
    {
        transform.position = FindObjectOfType<Teleport>().transform.position;
    }

    private void OnDestroy()
    {
        playerMove -= Move;
    }

    void Update()
    {
        playerMove += Animator;

        if (neverMove)
        {
            GetComponentInChildren<Animator>().SetInteger("isRun", 0);
            speed = 0;
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDash && isMoving)
        {
            StartCoroutine(Dash());
        }

        if (Input.GetMouseButtonDown(0) && !isAtk)
        {
            Atk();
        }

    }

    public void SpeedDefault()
    {
        speed = defaultSpeed;
    }

    void Move() // �����¿� ������
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        rigid.velocity = new Vector2(x, y).normalized * speed;

        if (!neverMove)
            Flip();
    }
    public IEnumerator Dash(float dashtTime = 0.1f) // �뽬
    {
        speed = dashSpeed;
        trail.emitting = true;
        yield return new WaitForSeconds(dashtTime);
        trail.emitting = false;
        StartCoroutine(player_UI.DashCoolTime());
        isDash = true;
        speed = defaultSpeed;
        yield return new WaitForSeconds(1f);
        isDash = false;
    }
    void Flip() // ĳ���� �ٶ󺸴� ����
    {
        if (x > 0)
            transform.localScale = new Vector2(1, 1);
        else if (x < 0)
            transform.localScale = new Vector2(-1, 1);

        isFlip = (int)transform.localScale.x;
    }
    void Animator() // �ִϸ�����~~
    {
        GetComponentInChildren<Animator>().SetInteger("isRun", x == 0 ? (int)y : (int)x);
        if (x != 0 || y != 0)
        {
            isMoving = true;
        }
        else if (x == 0 && y == 0)
        {
            isMoving = false;
        }
    }

    void Atk() // ����.
    {
        speed = 3;
        isAtk = true;
        GetComponentInChildren<Animator>().SetBool("isAtk", isAtk);
    }
}
