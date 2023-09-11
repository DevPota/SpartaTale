using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eighth : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;

    private float x;
    private float y;
    private float value;
    private Vector3 vec;
    private Vector2[] colliderPoints;

    private void Start()
    {
        value = Random.Range(1f, 5f);
        lineRenderer=GetComponent<LineRenderer>();

        edgeCollider=GetComponent<EdgeCollider2D>();
        colliderPoints = edgeCollider.points;

        StartCoroutine(AddPoint());
        Destroy(gameObject, 3.0f);
    }
    private IEnumerator AddPoint()
    {
        while (true)
        {
            x = Random.Range(-1.0f, 1.0f);
            y = Random.Range(-1.0f, 1.0f);

            vec = value * new Vector3(x, y, 0).normalized;

            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.position + vec); //점이 하나 더 새로 그려짐

            Vector2[] newPoints = new Vector2[colliderPoints.Length + 1]; //기존의 collider point들에 점 하나 더 추가할 예정
            colliderPoints.CopyTo(newPoints, 0);
            newPoints[newPoints.Length - 1] = transform.position + vec;

            colliderPoints = newPoints;
            edgeCollider.points = colliderPoints;

            if (lineRenderer.positionCount == 40) yield break;

            yield return new WaitForSeconds(0.05f);
        }
    }
}
