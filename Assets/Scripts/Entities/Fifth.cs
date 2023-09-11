using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fifth : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        float theta = Random.Range(0.0f, 180.0f) * Mathf.Deg2Rad;
        _rigidbody.velocity += new Vector2(Mathf.Cos(theta), 0);

        PhysicsMaterial2D newMaterial = new PhysicsMaterial2D();
        newMaterial.friction = 5.0f; // 마찰 설정
        newMaterial.bounciness = Random.Range(0.95f, 0.99f); //bounciness를 똑같이 하면 동시에 튀었다가 떨어짐
        GetComponent<Collider2D>().sharedMaterial = newMaterial;

        Destroy(gameObject, 8.5f);
    }
}
