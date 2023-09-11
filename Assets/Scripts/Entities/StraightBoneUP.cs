using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StraightBoneUp : MonoBehaviour
{
    Sequence sequence;
    public int BoneCount = 19;

    GameObject bone;

    void Start()
    {
        bone = transform.Find("bone").gameObject;

        //sequence = DOTween.Sequence();
        //sequence.Append();
        //sequence.Join(transform.DOShakePosition(1.0f, 0.1f, 10));
        transform.DOMove(transform.position + new Vector3(0, bone.GetComponent<SpriteRenderer>().size.y, 0), 0.4f);

        Destroy(gameObject, 1.5f);

    }
}
