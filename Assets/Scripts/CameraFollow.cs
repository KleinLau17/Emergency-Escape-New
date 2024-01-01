using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MapBound
{
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;
}

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.4f;
    public MapBound mapBound;

    private float _offsetZ;
    private Vector3 _currentVelocity;

    private void Start()
    {
        if (target == null)
        {
            return;
        }

        _offsetZ = (transform.position - target.position).z;
    }

    private void FixedUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = target.position + Vector3.forward * _offsetZ;
        Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);

        newPosition.x = Mathf.Clamp(newPosition.x, mapBound.xMin, mapBound.xMax);
        newPosition.y = Mathf.Clamp(newPosition.y, mapBound.yMin, mapBound.yMax);

        transform.position = newPosition;
    }
}
