using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerFollowCamera : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    private Transform target;

    private Vector3 bottomLeftBounds;
    private Vector3 topRightBounds;

    private float halfHeight;
    private float halfWidth;

    private void Start()
    {
        target = PlayerController.Instance.transform;

        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        bottomLeftBounds = tilemap.localBounds.min + new Vector3(halfWidth, halfHeight, 0);
        topRightBounds = tilemap.localBounds.max + new Vector3(-halfWidth, -halfHeight, 0);

        // pass player data about hte end of the map, so player can`t cross it
        PlayerController.Instance.SetBounds(tilemap.localBounds.min, tilemap.localBounds.max);
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        // to make camere be always in border of the background
        float clampedX = Mathf.Clamp(transform.position.x, bottomLeftBounds.x, topRightBounds.x);
        float clampedY = Mathf.Clamp(transform.position.y, bottomLeftBounds.y, topRightBounds.y);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
