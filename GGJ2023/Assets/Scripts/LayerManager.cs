using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    [SerializeField] public Transform worldEdgeLeft, worldEdgeRight, worldEdgeBottom, worldEdgeUpper;
    [SerializeField] private MoveableLayer[] layers;
    public static LayerManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void MoveableLayers(Vector3 movement)
    {
        foreach (MoveableLayer obj in layers)
        {
            float x = movement.x * obj.xSpeed - obj.autoScrollXSpeed * Time.deltaTime;
            float y = movement.y * obj.xSpeed - obj.autoScrollYSpeed * Time.deltaTime;
            obj.layer.position += new Vector3(x, y, 0f);
        }
    }
}

[System.Serializable]
public class MoveableLayer
{
    public Transform layer;
    public float xSpeed, ySpeed;
    public float autoScrollXSpeed, autoScrollYSpeed;
    [HideInInspector] public Transform[] children;
}
