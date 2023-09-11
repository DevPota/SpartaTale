using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class Character : MonoBehaviour
{
    private GameObject Mask;
    public Sprite[] sprites;
    float speed = 5f;
    public int hp = 10;
    bool hasBeenAttack = false;
    public bool moveWithMask = false;
    public bool blueHeart = false;
    bool hasBeenChangeState = false;
    bool jumpAble = true;

    Vector3 maskPosition;
    float maskHalfWidth;
    float maskHalfHeight;

    float playerHalfWidth;
    float playerHalfHeight;

    Rigidbody2D _rigid;
    Collider2D _coll;

    /* UI */
    private int currentPositionIndex = 0;
    public GameObject yellowBtn;

    public Vector3[] ButtonPositions { get; private set; } = new Vector3[]
    {
        new Vector3(-5.2f, -4.5f, 0f),  // 기본 위치
        new Vector3(-2.2f, -4.5f, 0f),   // 왼쪽 위치
        new Vector3(0.75f, -4.5f, 0f),   // 오른쪽 위치
        new Vector3(3.78f, -4.5f, 0f)    // 추가 위치
    };

    void Start()
    {
        GetLength();
        GetMask();
        _rigid = GetComponent<Rigidbody2D>();
        _rigid.gravityScale = 0.0f;
        _coll = GetComponent<Collider2D>();
        _coll.isTrigger = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) == true)
        {
            /* 버튼 사용 */
        }

        
        if(GameManager.I.IsPlayerTurn == true)
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
            if (!moveWithMask)
            {
                if (!blueHeart) MoveIdle();
                else MoveJump();
            }
            else MoveMask();
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

        transform.position += new Vector3(x, y) * speed * Time.deltaTime;
        if (transform.position.x > maskPosition.x + maskHalfWidth - playerHalfWidth || transform.position.x < maskPosition.x - maskHalfWidth + playerHalfWidth)
        {
            transform.position -= new Vector3(x, y) * speed * Time.deltaTime;
        }
        if (transform.position.y > maskPosition.y + maskHalfHeight - playerHalfHeight || transform.position.y < maskPosition.y - maskHalfHeight + playerHalfHeight)
        {
            transform.position -= new Vector3(x, y) * speed * Time.deltaTime;
        }
    }
    void MoveMask()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(x, y) * speed * Time.deltaTime;

        //화면 밖으로 못 나가도록
        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)); 
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        if (transform.position.x > topRight.x - playerHalfWidth || transform.position.x < bottomLeft.x + playerHalfWidth)
        {
            transform.position -= new Vector3(x, y) * speed * Time.deltaTime;
        }
        if (transform.position.y > topRight.y - playerHalfHeight || transform.position.y < bottomLeft.y + playerHalfHeight)
        {
            transform.position -= new Vector3(x, y) * speed * Time.deltaTime;
        }
        //마스크도 같이 움직이도록
        if (transform.position.x > maskPosition.x + maskHalfWidth - playerHalfWidth || transform.position.x < maskPosition.x - maskHalfWidth + playerHalfWidth || transform.position.y > maskPosition.y + maskHalfHeight - playerHalfHeight || transform.position.y < maskPosition.y - maskHalfHeight + playerHalfHeight)
        {
            Mask.transform.position += new Vector3(x, y) * speed * Time.deltaTime;

            GameManager.I.GetComponent<GameManager>().GetMask();
            GetMask();
        }
    }

    void MoveJump()
    {
        if (!hasBeenChangeState) MoveJumpEnter();
        float x = Input.GetAxisRaw("Horizontal");

        transform.position += new Vector3(x, 0) * speed * Time.deltaTime;

        if (transform.position.x > maskPosition.x + maskHalfWidth - playerHalfWidth || transform.position.x < maskPosition.x - maskHalfWidth + playerHalfWidth)
        {
            transform.position -= new Vector3(x, 0) * speed * Time.deltaTime;
        }
        if (transform.position.y > maskPosition.y + maskHalfHeight - playerHalfHeight || transform.position.y < maskPosition.y - maskHalfHeight + playerHalfHeight)
        {
            transform.position -= new Vector3(x, 0) * speed * Time.deltaTime;
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    StartCoroutine(Jump());
        //}
        if (Input.GetKeyDown(KeyCode.Space)&&jumpAble)
        {
            jumpAble = false;
            _rigid.AddForce(Vector2.up * 5.5f, ForceMode2D.Impulse);
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
        yield return new WaitForSeconds(0.2f);
        jumpAble = true;
    }

    void MoveJumpEnter()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[1];
        hasBeenChangeState = true;
        _rigid.gravityScale = 1.05f;
        _coll.isTrigger = false;
    }
    void MoveJumpEscape()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[0];
        hasBeenChangeState = false;
        _rigid.gravityScale = 0.0f;
        _coll.isTrigger = true;
    }

    void  InitializeAttack()
    {
        hasBeenAttack = false;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "enemy" && !hasBeenAttack) //한 프레임에 여러 대 맞는 걸 방지하기 위해 bool타입 변수 사용
        {
            hp--;
            Debug.Log("맞음");
            Debug.Log(hp);
            hasBeenAttack = true;
            Invoke("InitializeAttack", 0.2f); //0.2초 간격으로만 맞을 수 있도록
        }
    }
}
