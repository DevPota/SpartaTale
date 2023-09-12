using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_BrokenHeart : MonoBehaviour
{
    [SerializeField] Rigidbody2D[] rigs;

    private void OnEnable()
    {
        foreach(Rigidbody2D rig in rigs)
        {
            rig.AddForce(new Vector2(Random.Range(-2, 3), 5), ForceMode2D.Impulse);
        }
    }
}
