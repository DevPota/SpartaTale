using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Runtime.InteropServices;
using Unity.VisualScripting;

public class StraightBone : MonoBehaviour
{
    Vector3 maskPosition = Vector3.zero;
    float maskHalfWidth = 0;
    float maskHalfHeight = 0;
    public int BoneCount = 19;

    GameObject bone;

    void Start()
    {
        maskPosition = GameManager.I.maskPosition;
        maskHalfWidth = GameManager.I.maskHalfWidth;
        maskHalfHeight = GameManager.I.maskHalfHeight;
        bone = transform.Find("bone").gameObject;

        if (transform.position.x < maskPosition.x - maskHalfWidth) 
        {
            HorizontalRight();
            Invoke("HorizontalLeft", 0.5f);
        }
        else if (transform.position.x > maskPosition.x + maskHalfWidth)
        {
            HorizontalLeft();
            Invoke("HorizontalRight", 0.5f);
        }
        else
        {
            if (transform.position.y > maskPosition.y + maskHalfHeight)
            {
                VerticalDown();
                Invoke("VerticalUp", 0.5f);
            }
            else if (transform.position.y < maskPosition.y - maskHalfHeight)
            {
                VerticalUp();
                Invoke("VerticalDown", 0.5f);
            }
        }

        Destroy(gameObject, 1.0f);

    }

    void HorizontalRight()
    {
        transform.DOMove(transform.position + new Vector3(maskHalfWidth, 0, 0), 0.4f);
    }
    void HorizontalLeft()
    {
        transform.DOMove(transform.position - new Vector3(maskHalfWidth, 0, 0), 0.4f);
    }
    void VerticalUp()
    {
        transform.DOMove(transform.position + new Vector3(0, maskHalfHeight,0), 0.4f);
    }
    void VerticalDown()
    {
        transform.DOMove(transform.position - new Vector3(0, maskHalfHeight, 0), 0.4f);
    }
}
