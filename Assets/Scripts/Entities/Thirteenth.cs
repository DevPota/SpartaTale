using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Thirteenth : MonoBehaviour
{
    Vector3 maskPosition = Vector3.zero;
    float maskHalfWidth = 0;
    bool right = true;

    void Start()
    {
        maskPosition = GameManager.I.maskPosition;
        maskHalfWidth = GameManager.I.maskHalfWidth;

        if(transform.position.x > maskPosition.x)
        {
            right = false;
            transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }

        Destroy(gameObject, 1.5f);
    }

    void Update()
    {
        if(right)
        {
            transform.position += new Vector3(4.0f, 0, 0) * Time.deltaTime;
        }
        else
        {
            transform.position -= new Vector3(4.0f, 0, 0) * Time.deltaTime;
        }
    }
}
