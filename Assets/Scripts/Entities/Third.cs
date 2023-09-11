using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Third : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMove(transform.position - new Vector3(GameManager.I.maskHalfWidth * 6, 0, 0), 4.5f);
        Destroy(gameObject, 5.0f);
    }

}
