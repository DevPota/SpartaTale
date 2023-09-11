using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwelfthShort : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5.0f);
    }

    void Update()
    {
        transform.position -= new Vector3(1, 0, 0) * 5.0f * Time.deltaTime;
    }
}
