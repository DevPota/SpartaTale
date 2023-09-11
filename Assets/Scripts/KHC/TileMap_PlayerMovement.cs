using UnityEngine;

public class TileMap_PlayerMovement : MonoBehaviour
{
    public  bool        IsActive  { get; set; }         = true;
    public  Animator    Anim      { get; private set; } = default;

            Rigidbody2D rigid                           = default;
            Vector2     direciton                       = new Vector2(0, -1);

            float       speed                           = 2f;
            string      isMoveStr                       = "IsMove";
            string      horizontalStr                   = "Horizontal";
            string      verticalStr                     = "Vertical";

    public void Start()
    {
        Anim  = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(IsActive == false)
        {
            return;
        }

        float horizontal = Input.GetAxisRaw(horizontalStr);
        float vertical   = Input.GetAxisRaw(verticalStr);

        if(Mathf.Approximately(horizontal, 0) == true && Mathf.Approximately(vertical, 0) == true)
        {
            Anim.SetBool(isMoveStr, false);
        }
        else
        {
            Anim.SetBool(isMoveStr, true);
            direciton = new Vector2(horizontal, vertical);
        }

        Anim.SetFloat(horizontalStr, direciton.x);
        Anim.SetFloat(verticalStr, direciton.y);
        rigid.velocity = new Vector2(horizontal, vertical) * speed;
    }
}
