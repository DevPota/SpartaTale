using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Second : MonoBehaviour
{
    void Start()
    {
        transform.DOMove(transform.position - new Vector3(GameManager.I.maskHalfWidth * 3, 0, 0), 1.8f);
        Destroy(gameObject, 2.0f);
    }
}
