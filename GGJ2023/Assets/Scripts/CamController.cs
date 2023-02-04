using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField]
    [Range(0.01f, 1f)]
    private float cameraFollowSpeed;
    private static float cameraY = 100000f;
    [SerializeField] private Vector2 offset;
    private Vector3 velocity = Vector3.zero;
    private Vector3 oldPosition;
    private float minX, maxX, minY, maxY;

    private void Start()
    {
        float width = Camera.main.orthographicSize * Screen.width / Screen.height;
        minX = LayerManager.instance.worldEdgeLeft.position.x + width;
        maxX = LayerManager.instance.worldEdgeRight.position.x - width;
        minY = LayerManager.instance.worldEdgeBottom.position.y;
        maxY = LayerManager.instance.worldEdgeUpper.position.y;
        transform.position = GetWantedPosition();
        //cameraY = transform.position.y;
    }

    private Vector3 GetWantedPosition(bool followY = true)
    {
        if (player != null)
        {
            float x = Mathf.Clamp(player.position.x + offset.x, minX, maxX);
            float y = Mathf.Clamp(player.position.y + offset.y, minY, maxY);
            //float y = followY ? player.position.y + offset.y : transform.position.y;

            /*if(!followY && cameraY != -10000f)
            {
                y = cameraY;
            }*/
            return new Vector3(x, y, -10f);
        }
        return new Vector3(0f, 0f, -10f);
    }

    private void Update()
    {
        if (player != null)
        {
            Vector3 wantedPosition = GetWantedPosition(false);

            LayerManager.instance.MoveableLayers(transform.position - oldPosition);
            oldPosition = transform.position;

            transform.position = Vector3.SmoothDamp(transform.position, wantedPosition, ref velocity, cameraFollowSpeed);
        }
    }
}
