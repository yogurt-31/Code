using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    new SpriteRenderer renderer;
    Rigidbody2D rigid;
    Animator animator;
    Timer timer;
    BtnManager btnManager;

    new public AudioSource audio;
    public float speed;
    public float jumpPower;
    public GameObject DiedPanel;
    public GameObject ClearPanel;

    public int isJump = 2;
    public bool isWall = false;
    public bool isDie = false;
    public bool isClear = false;
    void Start()
    {
        audio = FindObjectOfType<AudioSource>();
        btnManager = FindObjectOfType<BtnManager>();
        timer = FindObjectOfType<Timer>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(!isDie && !isClear && !btnManager.isMenu)
        {
            float h = Input.GetAxisRaw("Horizontal");
            Flip(h);
            rigid.velocity = new Vector2(h * speed, rigid.velocity.y);

            if (Input.GetKeyDown(KeyCode.Space) && isJump != 0)
            {
                Jump();
            }

            animator.SetInteger("isRun", (int)h);
            if (isWall)
                animator.SetInteger("isRun", 1);
        }
        if(timer.limitTime <= 0)
        {
            isDie = true;
            animator.SetBool("isDie", isDie);
        }
        if (isClear)
            audio.Stop();
        if (isDie)
            timer.StopTimer();
    }
    void Flip(float h)
    {
        if (h > 0)
            renderer.flipX = false;
        else if (h < 0)
            renderer.flipX = true;
    }

    void Jump()
    {
        if(isJump != 0)
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
        }
        isJump--;
    }
    public void StopTime()
    {
        audio.Pause();
    }
    public void YouDied()
    {
        DiedPanel.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            isJump = 2;
        }
        else if(collision.gameObject.CompareTag("Wall"))
        {
            isJump = 1;
            isWall = true;
        }
        if(collision.gameObject.CompareTag("Death"))
        {
            isDie = true;
            animator.SetBool("isDie", isDie);
        }
        if(collision.gameObject.CompareTag("Clear"))
        {
            isClear = true;
            ClearPanel.SetActive(true);
            timer.StopTimer();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isWall = false;
        }
        
    }
}
