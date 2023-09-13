using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditPlayer : MonoBehaviour
{
    float Speed { get; set; } = 5f;
    float playerHalfWidth;
    float playerHalfHeight;

    void Awake()
    {
        playerHalfWidth = transform.localScale.x;
        playerHalfHeight = transform.localScale.y;
    }

    void Update()
    {
        MoveIdle();
    }

    void MoveIdle()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(x, y) * Speed * Time.deltaTime;

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
    }
}
