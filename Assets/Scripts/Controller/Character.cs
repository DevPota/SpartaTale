using System;
using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int   Hp           { get; set; } = 1;
    public int   MaxHp        { get; set; } = 92;
    public float Speed        { get; set; } = 5f;
    public bool  MoveWithMask { get; set; } = false;
    public bool  BlueHeart    { get; set; } = false;
    public bool  Ending       { get; set; } = false;

    GameObject Mask;

    bool hasBeenAttack = false;
    bool hasBeenChangeState = false;
    bool jumpAble = true;

    Vector3 maskPosition;

    float maskHalfWidth;
    float maskHalfHeight;
    float playerHalfWidth;
    float playerHalfHeight;

    Rigidbody2D rigid;
    Animator    anim;
    public Collider2D Coll { get; private set; }

    /* UI */
    int currentPositionIndex = 0;

    public Vector3[] ButtonPositions { get; private set; } = new Vector3[]
    {
        new Vector3(-5.2f, -4.5f, 0f),  // 기본 위치
        new Vector3(-2.2f, -4.5f, 0f),   // 왼쪽 위치
        new Vector3(0.75f, -4.5f, 0f),   // 오른쪽 위치
        new Vector3(3.78f, -4.5f, 0f)    // 추가 위치
    };

    Action<int, int>   hpListener       = null;
    Action<float>      hpSliderListener = null;

    public void Init(Action<int, int> hpAction, Action<float> hpSliderAction)
    {
        GetLength();
        GetMask();
        rigid = GetComponent<Rigidbody2D>();
        Coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        rigid.gravityScale = 0.0f;
        Coll.isTrigger = true;

        hpListener       = hpAction;
        hpSliderListener = hpSliderAction;

        hpListener(Hp, MaxHp);
        hpSliderListener((float)Hp / MaxHp);
    }

    void Update()
    {
        if (Hp > 0) 
        {
            if (Ending) Speed = 1.0f;
            if (Input.GetKeyDown(KeyCode.Z) == true)
            {
                /* 버튼 사용 */
            }


            if (GameManager.I.IsPlayerTurn == true)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) == true || Input.GetKeyDown(KeyCode.A) == true)
                {
                    currentPositionIndex = (currentPositionIndex - 1 + ButtonPositions.Length) % ButtonPositions.Length;
                    transform.position = ButtonPositions[currentPositionIndex];
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) == true)
                {
                    currentPositionIndex = (currentPositionIndex + 1) % ButtonPositions.Length;
                    transform.position = ButtonPositions[currentPositionIndex];
                }
            }
            else
            {
                if (!MoveWithMask)
                {
                    if (!BlueHeart) MoveIdle();
                    else MoveJump();
                }
                else MoveMask();
            }
        }
    }

    void GetLength()
    {
        playerHalfWidth = transform.localScale.x / 2;
        playerHalfHeight = transform.localScale.y / 2;
    }
    public void GetMask()
    {
        Mask           = GameManager.I.Mask;
        maskPosition   = GameManager.I.maskPosition;
        maskHalfWidth  = GameManager.I.maskHalfWidth;
        maskHalfHeight = GameManager.I.maskHalfHeight;
    }

    public void MoveUI()
    {

    }

    void MoveIdle()
    {
        if (hasBeenChangeState) MoveJumpEscape();

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(x, y) * Speed * Time.deltaTime;
        if (transform.position.x > maskPosition.x + maskHalfWidth - playerHalfWidth || transform.position.x < maskPosition.x - maskHalfWidth + playerHalfWidth)
        {
            transform.position -= new Vector3(x, y) * Speed * Time.deltaTime;
        }
        if (transform.position.y > maskPosition.y + maskHalfHeight - playerHalfHeight || transform.position.y < maskPosition.y - maskHalfHeight + playerHalfHeight)
        {
            transform.position -= new Vector3(x, y) * Speed * Time.deltaTime;
        }
    }
    void MoveMask()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(x, y) * Speed * Time.deltaTime;

        //화면 밖으로 못 나가도록
        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)); 
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        if (transform.position.x > topRight.x - playerHalfWidth || transform.position.x < bottomLeft.x + playerHalfWidth)
        {
            transform.position -= new Vector3(x, y) * Speed * Time.deltaTime;
        }
        if (transform.position.y > topRight.y - playerHalfHeight || transform.position.y < bottomLeft.y + playerHalfHeight)
        {
            transform.position -= new Vector3(x, y) * Speed * Time.deltaTime;
        }
        //마스크도 같이 움직이도록
        if (transform.position.x > maskPosition.x + maskHalfWidth - playerHalfWidth || transform.position.x < maskPosition.x - maskHalfWidth + playerHalfWidth || transform.position.y > maskPosition.y + maskHalfHeight - playerHalfHeight || transform.position.y < maskPosition.y - maskHalfHeight + playerHalfHeight)
        {
            Mask.transform.position += new Vector3(x, y) * Speed * Time.deltaTime;

            GameManager.I.GetComponent<GameManager>().GetMask();
            GetMask();
        }
    }

    void MoveJump()
    {
        if (!hasBeenChangeState) MoveJumpEnter();
        float x = Input.GetAxisRaw("Horizontal");

        transform.position += new Vector3(x, 0) * Speed * Time.deltaTime;

        if (transform.position.x > maskPosition.x + maskHalfWidth - playerHalfWidth || transform.position.x < maskPosition.x - maskHalfWidth + playerHalfWidth)
        {
            transform.position -= new Vector3(x, 0) * Speed * Time.deltaTime;
        }
        if (transform.position.y > maskPosition.y + maskHalfHeight - playerHalfHeight || transform.position.y < maskPosition.y - maskHalfHeight + playerHalfHeight)
        {
            transform.position -= new Vector3(x, 0) * Speed * Time.deltaTime;
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    StartCoroutine(Jump());
        //}
        if (Input.GetKeyDown(KeyCode.Space)&&jumpAble)
        {
            jumpAble = false;
            rigid.AddForce(Vector2.up * 5.5f, ForceMode2D.Impulse);
            StartCoroutine(JumpDelay());
        }
    }
    //private IEnumerator Jump()
    //{
    //    _rigidbody.AddForce(Vector2.up * 5.0f, ForceMode2D.Impulse);
    //    Debug.Log("jump");

    //    yield return new WaitForSeconds(5.0f);
    //}
    private IEnumerator JumpDelay()
    {
        yield return new WaitForSeconds(0.7f);
        jumpAble = true;
    }

    void MoveJumpEnter()
    {
        GetComponent<SpriteRenderer>().color = new Color(0,0,1);
        hasBeenChangeState = true;
        rigid.gravityScale = 1.05f;
        Coll.isTrigger = false;
    }
    void MoveJumpEscape()
    {
        GetComponent<SpriteRenderer>().color = new Color(1,1,1);
        hasBeenChangeState = false;
        rigid.gravityScale = 0.0f;
        rigid.velocity = Vector3.zero;
        Coll.isTrigger = true;
    }

    void  InitializeAttack()
    {
        hasBeenAttack = false;
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "enemy" && hasBeenAttack == false)
        {
            Hp--;
            hasBeenAttack = true;
            GameManager.I.PlaySFX("SFX_PlayerHit");
            anim.SetTrigger("OnHit");
            Invoke("InitializeAttack", 0.05f);

            hpListener(Hp, MaxHp);
            hpSliderListener((float)Hp / MaxHp);
        }
    }
}
