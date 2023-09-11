using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwelfthLong : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5.0f);
    }

    void Update()
    {
        transform.position += new Vector3(1.0f, 0, 0) * 5.0f * Time.deltaTime;
    }
}
