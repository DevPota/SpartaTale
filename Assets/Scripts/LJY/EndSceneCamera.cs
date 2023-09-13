using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneCamera : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    Vector3 cameraPosition;

    [SerializeField]
    Vector2 center;

    [SerializeField]
    Vector2 mapSize;

    [SerializeField]
    public float cameraMoveSpeed;

    float height;
    float width;

    private void Update()
    {
        if (transform.position.x <= 11)
        {
            LimitCameraArea();
        }

        else
        {
            CameraMove();
        }
    }

    private void LimitCameraArea()
    {
        transform.position = player.transform.position + cameraPosition;
        float lx = mapSize.x - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }

    private void CameraMove()
    {
        TileMap_PlayerMovement temp = player.GetComponent<TileMap_PlayerMovement>();
        temp.speed = 0.0f;
        temp.IsActive = false;
    }
}
